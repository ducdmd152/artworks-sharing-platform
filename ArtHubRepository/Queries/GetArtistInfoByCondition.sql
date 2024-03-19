/*
    CreatedBy: Tien
    Date: 03/19/2024
    

*/

WITH PostDetailsJson AS (
    SELECT
        CASE WHEN 1 = 1 THEN
            (
            SELECT
                p.post_id AS PostId,
                p.title AS Title,
                p.description AS Description,
                p.status AS Status,
                p.scope AS Scope,
                p.total_react AS TotalReact,
                p.total_view AS TotalView,
                p.total_bookmark AS TotalBookmark,
                p.artist_email AS ArtistEmail,
                STRING_AGG(i.image_url, ',') AS ImageUrls
            FROM
                ArtHub.dbo.post p
            JOIN 
                ArtHub.dbo.[image] i ON
                p.post_id = i.post_id
            WHERE
                artist_email = 'creator@gmail.com'
                AND scope IN (1, 2, 3)
                AND status IN (1, 2)
            GROUP BY
                p.post_id,
                p.title,
                p.description,
                p.status,
                p.scope,
                p.total_react,
                p.total_view,
                p.total_bookmark,
                p.artist_email
            ORDER BY
                total_react DESC
            OFFSET 0 ROWS
            FETCH NEXT 6 ROWS ONLY
            FOR JSON PATH
            )
        ELSE NULL END AS PostDetail
)  
SELECT
    a.email AS Email,
    MAX(a.avatar) AS Avatar,
    MAX(art.artist_name) AS ArtistName,
    MAX(art.bio) AS Bio,
    MAX(art.total_subscribe) AS TotalSubscriber,
    SUM(total_react) AS TotalReact,
    SUM(total_view) AS TotalView,
    SUM(total_bookmark) AS TotalBookmark,
    MAX(PostDetailsJson.PostDetail) AS PostDetail
FROM 
    ArtHub.dbo.post p
INNER JOIN ArtHub.dbo.account a ON p.artist_email = a.email
INNER JOIN ArtHub.dbo.artist art ON p.artist_email = art.email
CROSS JOIN PostDetailsJson
WHERE
    p.artist_email = 'creator@gmail.com'
    AND a.status = 1
    AND a.enabled = 1
GROUP BY a.email;

