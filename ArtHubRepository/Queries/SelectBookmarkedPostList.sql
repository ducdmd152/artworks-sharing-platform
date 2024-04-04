/*
    CreatedBy: DucDMD
    Date: 22/03/2024
    
    @AudienceEmail string
    @PageIndex int
    @PageSize int
*/

-- Get the total number of bookmarked posts
DECLARE @TotalItems INT;
SELECT @TotalItems = COUNT(b.bookmark_id) 
FROM bookmark b
WHERE b.account_email = @AudienceEmail;

-- Calculate TotalPages
DECLARE @TotalPages INT;
SET @TotalPages = CEILING(CONVERT(FLOAT, @TotalItems) / @PageSize);

-- Retrieve the paged results
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
    a.artist_name AS ArtistName,
    acc.avatar AS ArtistAvatar,
    (
        SELECT TOP 1 image_url
        FROM [image] i
        WHERE i.post_id = p.post_id
        ORDER BY i.created_date ASC
    ) AS Image,
    @TotalPages AS TotalPages,
    @TotalItems AS TotalItems
FROM 
    bookmark b
INNER JOIN 
    post p ON p.post_id = b.post_id
INNER JOIN 
    artist a ON p.artist_email = a.email
LEFT JOIN
    account acc ON a.email = acc.email
WHERE 
    b.account_email = @AudienceEmail
ORDER BY 
    b.created_date DESC
OFFSET (@PageIndex - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;