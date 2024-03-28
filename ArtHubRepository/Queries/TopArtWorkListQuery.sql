/*DECLARE @PostTitle NVARCHAR(100) = '';
DECLARE @PageSize INT = 10;
DECLARE @PageNumber INT = 1;*/


WITH TopArtWork AS (
    SELECT
        p.title AS PostTitle,
        a.artist_name AS CreatorName,
        p.total_react AS LoveCount,
        p.total_bookmark AS SaveCount,
        p.total_view AS ViewCount,
        ROW_NUMBER() OVER (ORDER BY p.created_date DESC) AS RowNum,
        COUNT(*) OVER () AS TotalRecords
    FROM
        ArtHub.dbo.post AS p
    JOIN
        ArtHub.dbo.artist AS a ON p.artist_email = a.email
        where 
          (@PostTitle IS NULL OR p.title = @PostTitle)
      
        
)
SELECT
    CEILING(CONVERT(decimal, pl.TotalRecords) / @PageSize) AS TotalPages,
    pl.TotalRecords AS TotalItems,
    pl.PostTitle As PostTitle ,
    pl.CreatorName As CreatorName,
    pl.LoveCount As LoveCount,
    pl.SaveCount As SaveCount ,
    pl.ViewCount As ViewCount
FROM
    TopArtWork AS pl
WHERE
    pl.RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize;
