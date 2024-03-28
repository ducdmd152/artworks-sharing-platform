/*
    CreatedBy: DucDMD
    Date: 19/03/2024
    
    @AudienceEmail string
    @CreatorEmail string
    @Categories List<int>
    @PageIndex int
    @PageSize int
*/

WITH SubscribedCreators AS (
    SELECT email_artist
    FROM subscriber
    WHERE 
        @AudienceEmail IS NOT NULL
        AND email_user = @AudienceEmail
        AND status = 1 
        AND GETDATE() <= expired_date    
),
AuthenticatedPosts AS (
    SELECT post_id
    FROM post
    WHERE
        status = 2
        AND (scope = 1 OR (scope = 2 AND artist_email IN (SELECT email_artist FROM SubscribedCreators)))
)
    
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
    a.artist_name AS ArtistName
FROM post p
LEFT JOIN post_category pc ON p.post_id = pc.post_id
LEFT JOIN artist a ON p.artist_email = a.email
WHERE p.post_id IN (SELECT post_id FROM AuthenticatedPosts) AND p.artist_email != @CreatorEmail
ORDER BY 
    CASE 
        WHEN pc.category_id IN @Categories THEN 0
        ELSE 1
    END
OFFSET (@PageIndex - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;

