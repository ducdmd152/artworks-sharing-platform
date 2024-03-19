/*
    CreatedBy: DucDMD
    Date: 19/03/2024
    
    @AudienceEmail string
*/

WITH SubscribedCreators AS (
    SELECT email_artist
    FROM subscriber
    WHERE 
        email_user = @AudienceEmail
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
    *
FROM post p
LEFT JOIN post_category pc ON p.post_id = pc.post_id
WHERE post_id IN (SELECT post_id FROM AuthenticatedPosts) OR @AudienceEmail IS NULL
ORDER BY 
    CASE 
        WHEN pc.category_id IN (SELECT CategoryId FROM @Categories) THEN 0
        ELSE 1
    END;
