/*DECLARE @PostTitle NVARCHAR(100) = '';
DECLARE @PageSize INT = 10;
DECLARE @PageNumber INT = 1;*/


WITH TopArtWork AS (
    SELECT
        PostTitle,
        CreatorName,
        LoveCount,
        SaveCount,
        ViewCount,
        ROW_NUMBER() OVER (ORDER BY LoveCount DESC) AS RowNum,
        COUNT(*) OVER () AS TotalRecords
    FROM
        (SELECT
            p.title AS PostTitle,
            a.artist_name AS CreatorName,
            p.total_react AS LoveCount,
            p.total_bookmark AS SaveCount,
            p.total_view AS ViewCount
        FROM
            ArtHub.dbo.post AS p
        JOIN
            ArtHub.dbo.artist AS a ON p.artist_email = a.email
        WHERE 
            (@PostTitle IS NULL OR p.title = @PostTitle)
        ) AS SortedArtWork
)
SELECT
    CEILING(CONVERT(decimal, TotalRecords) / @PageSize) AS TotalPages,
    TotalRecords AS TotalItems,
    PostTitle As PostTitle,
    CreatorName As CreatorName,
    LoveCount As LoveCount,
    SaveCount As SaveCount,
    ViewCount As ViewCount
FROM
    TopArtWork
WHERE
    RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize;