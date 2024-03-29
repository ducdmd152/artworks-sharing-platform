/*
    CreatedBy: DucDMD
    Date: 25/03/2024
    
    @CreatorEmail string
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

SELECT DISTINCT
    a.email AS ArtistEmail,
    a.bio AS ArtistBio,
    a.artist_name AS ArtistName,
    a.total_subscribe AS ArtistTotalSubscribe,
    SUM(p.total_react) AS ArtistTotalReact,
    SUM(p.total_view) AS ArtistTotalView,
    acc.avatar AS ArtistAvatar,
    CASE WHEN a.email IN (SELECT email_artist FROM SubscribedCreators)
		THEN 'True' ELSE 'False'
		END
		AS IsSubscribed
FROM artist a
INNER JOIN account acc ON a.email = acc.email
LEFT JOIN post p ON a.email = p.artist_email
WHERE a.email = @CreatorEmail
GROUP BY 
    a.email,
    a.bio,
    a.artist_name,
    a.total_subscribe,
    acc.avatar;
