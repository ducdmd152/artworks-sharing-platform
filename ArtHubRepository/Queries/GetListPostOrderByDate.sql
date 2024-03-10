-- GetListPostOrderByDate.sql
/*
    CreatedBy: Thong
    Date: 03/09/2024
    
    @ArtworkTitle string
    @Date dateTime
    @ArtistName string
    @Status string
    @ArtworkID int
 */

SELECT
    post.post_id AS PostId,
    post.title AS Title,
    post.status AS Status,
    post.created_date AS Date,
	post.[scope] AS Scope,
	artist.email AS Email,
	artist.artist_name AS ArtistName
FROM
    post
    INNER JOIN artist ON post.artist_email = artist.email
WHERE
    CASE
        WHEN @ArtworkTitle IS NOT NULL THEN post.title LIKE '%' + @ArtworkTitle + '%'
        ELSE 'TRUE'
    END
    AND
        CASE
            WHEN @ArtworkID IS NOT NULL THEN post.title LIKE '%' + @ArtworkID + '%'
            ELSE 'TRUE'
        END
    AND
        CASE 
            WHEN @Date IS NOT NULL THEN post.created_date = @Date
            ELSE 'TRUE'
        END
    AND
        CASE 
            WHEN @ArtistName IS NOT NULL THEN artist_name LIKE '%' + @ArtistName + '%'
            ELSE 'TRUE'
        END
    AND
        CASE
            WHEN @Status IS NOT NULL THEN post.status = @Status
            ELSE 'TRUE'
        END;
