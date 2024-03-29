/*
    CreatedBy: DucDMD
    Date: 25/03/2024
    
    @AudienceEmail string
*/
SELECT
    ArtistEmail,
    ArtistBio,
    ArtistName,
    ArtistTotalSubscribe,
    ArtistTotalReact,
    ArtistTotalView,
    ArtistAvatar,
    TotalPages,
    TotalItems,
	SubscribeExpiredDate
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
		FORMAT(sub.expired_date, 'dd/MM/yyyy HH:mm') AS SubscribeExpiredDate,
        CEILING(COUNT(a.email) OVER () * 1.0 / @PageSize) AS TotalPages,
        ROW_NUMBER() OVER (ORDER BY MAX(sub.created_date) DESC) AS RowNum
    FROM subscriber sub
    INNER JOIN artist a ON (sub.status = 1 AND GETDATE() <= sub.expired_date AND sub.email_user = @AudienceEmail AND sub.email_artist = a.email)
    INNER JOIN account acc ON a.email = acc.email
    LEFT JOIN post p ON a.email = p.artist_email
    WHERE acc.enabled = 'true'
    GROUP BY 
        a.email,
        a.bio,
        a.artist_name,
        a.total_subscribe,
        acc.avatar,
		sub.expired_date
) AS SubQuery
WHERE RowNum BETWEEN (@PageIndex - 1) * @PageSize + 1 AND @PageIndex * @PageSize;
