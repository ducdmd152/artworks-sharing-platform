/*
    CreatedBy: DucDMD
    Date: 19/03/2024
    
    @PageIndex INT;
    @PageSize INT;
*/

DECLARE @PageIndex INT = 1;
DECLARE @PageSize INT = 12;

WITH TopCreators AS (
    SELECT
        a.email AS ArtistEmail,
        a.artist_name AS ArtistName,
        a.bio AS ArtistBio,
        acc.avatar AS ArtistAvatar,
        COUNT(DISTINCT sub.subscriber_id) AS TotalSubscribers,
        SUM(p.total_react) AS TotalReactions,
        ROW_NUMBER() OVER (ORDER BY COUNT(DISTINCT sub.subscriber_id) DESC, SUM(p.total_react) DESC) AS RowNum
    FROM
        artist a
    JOIN
        account acc ON a.email = acc.email
    LEFT JOIN
        subscriber sub ON a.email = sub.email_artist
    LEFT JOIN
        post p ON a.email = p.artist_email
    GROUP BY
        a.email,
        a.bio,
        a.artist_name,
        acc.avatar
)

SELECT 
    ArtistEmail,
    ArtistName,
    ArtistBio,
    ArtistAvatar,
    TotalSubscribers,
    TotalReactions
FROM
    TopCreators
WHERE
    RowNum > (@PageIndex - 1) * @PageSize
    AND RowNum <= @PageIndex * @PageSize;