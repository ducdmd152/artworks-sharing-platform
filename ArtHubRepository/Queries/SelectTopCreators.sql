/*
    CreatedBy: DucDMD
    Date: 25/03/2024
    
    @AudienceEmail string
*/
WITH SubscribedCreators AS (
	SELECT email_artist
	FROM
		subscriber
	WHERE 
		email_user = @AudienceEmail
		AND status = 1
		AND GETDATE() <= expired_date
)

SELECT
    ArtistEmail,
    ArtistBio,
    ArtistName,
    ArtistTotalSubscribe,
    ArtistTotalReact,
    ArtistTotalView,
    ArtistAvatar,
    CASE WHEN ArtistEmail IN (SELECT email_artist FROM SubscribedCreators)
		THEN 'True' ELSE 'False'
		END
		AS IsSubscribed,
    TotalPages,
    TotalItems
FROM (
    SELECT DISTINCT
        a.email AS ArtistEmail,
        a.bio AS ArtistBio,
        a.artist_name AS ArtistName,
        a.total_subscribe AS ArtistTotalSubscribe,
        SUM(p.total_react) AS ArtistTotalReact,
        SUM(p.total_view) AS ArtistTotalView,
        acc.avatar AS ArtistAvatar,
        COUNT(a.email) OVER () AS TotalItems,
        CEILING(COUNT(a.email) OVER () * 1.0 / @PageSize) AS TotalPages,
        ROW_NUMBER() OVER (ORDER BY a.total_subscribe DESC, SUM(p.total_react) DESC) AS RowNum
    FROM artist a
    INNER JOIN account acc ON a.email = acc.email
    LEFT JOIN post p ON a.email = p.artist_email
    WHERE acc.enabled = 'true'
    GROUP BY 
        a.email,
        a.bio,
        a.artist_name,
        a.total_subscribe,
        acc.avatar	
) AS SubQuery
WHERE RowNum BETWEEN (@PageIndex - 1) * @PageSize + 1 AND @PageIndex * @PageSize;
