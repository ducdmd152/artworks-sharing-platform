/*
    CreatedBy: Thong
    Date: 03/18/2024
    
    @Status string
    @PageNumber int 
    @PageSize int
*/

WITH ReportList AS (
    SELECT
        r.report_id,
        r.reason,
        r.reporter_email,
        a.first_name + ' ' + a.last_name AS full_name,
        r.status,
        p.post_id,
        p.artist_email,
        (
            SELECT artist_name FROM artist a2 WHERE email = p.artist_email
        ) AS artist_name,
        ROW_NUMBER() OVER (ORDER BY r.created_date DESC) AS RowNum,
            COUNT(*) OVER () AS TotalRecords
    FROM
        report r INNER JOIN account a ON r.reporter_email  = a.email
                 INNER JOIN post p ON r.post_id = p.post_id
    WHERE
            r.status = 1
)
SELECT
    CEILING(CONVERT(decimal, r.TotalRecords) / @PageSize) AS TotalPages,
    r.TotalRecords AS TotalItems,
    r.report_id AS ReportId,
    r.reason AS Reason,
    r.reporter_email AS ReporterEmail,
    r.full_name AS FullName,
    r.status AS Status,
    r.post_id AS PostId,
    r.artist_email AS ArtistEmail,
    r.artist_name AS ArtistName
FROM
    ReportList AS r
WHERE
    r.RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize;