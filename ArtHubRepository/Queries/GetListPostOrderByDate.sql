/*
    CreatedBy: Thong
    Date: 03/09/2024
    
    @ArtworkTitle string
    @Date dateTime
    @ArtworkName string
    @Status string
    @ArtworkID int
    @PageNumber int 
    @PageSize int
*/

WITH ArtworkList AS (
    SELECT
        post.post_id,
        post.title,
        post.status,
        post.created_date,
        post.[scope],
        artist.email,
        artist.artist_name,
        ROW_NUMBER() OVER (ORDER BY post.created_date DESC) AS RowNum,
        COUNT(*) OVER () AS TotalRecords
    FROM
        post
            INNER JOIN artist ON post.artist_email = artist.email
    WHERE
        (@ArtworkTitle IS NULL OR post.title LIKE CONCAT('%', @ArtworkTitle, '%'))
      AND
        (@ArtworkID IS NULL OR post.post_id = @ArtworkID)
      AND
        (@Date IS NULL OR post.created_date = @Date)
      AND
        (@ArtworkName IS NULL OR artist.artist_name LIKE CONCAT('%', @ArtworkName, '%'))
      AND
        (@Status IS NULL OR @Status = -1 OR post.status = @Status)
)
SELECT
    CEILING(CONVERT(decimal, art.TotalRecords) / @PageSize) AS TotalPages,
    art.TotalRecords AS TotalItems,
    art.post_id AS PostId,
    art.title AS Title,
    art.status AS Status,
    art.created_date AS Date,
    art.[scope] AS Scope,
    art.email AS Email,
    art.artist_name AS ArtistName
FROM
    ArtworkList as art
WHERE
    art.RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize;
