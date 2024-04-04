/*
    CreatedBy: DucDMD
    Date: 19/03/2024
    
    @AudienceEmail string
    @ArtistEmail string
*/


WITH SubscribedCreators AS (
    SELECT email_artist
    FROM subscriber
    WHERE 
        @AudienceEmail IS NOT NULL
        AND email_user = @AudienceEmail
        AND status = 1 
        AND GETDATE() <= expired_date       
)

SELECT post_id
FROM post
WHERE
    status = 2
    AND (scope = 1 OR (scope = 2 AND artist_email IN (SELECT email_artist FROM SubscribedCreators)))
    AND (@ArtistEmail IS NULL OR artist_email = @ArtistEmail)
