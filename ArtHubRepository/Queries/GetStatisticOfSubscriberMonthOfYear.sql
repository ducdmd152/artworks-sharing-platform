/*
    CreatedBy: Tien
    Date: 03/23/2024
       
    @@ArtistEmail string //Email of artist
*/

SELECT
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 1 THEN 1 ELSE 0 END) AS January,
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 2 THEN 1 ELSE 0 END) AS February,
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 3 THEN 1 ELSE 0 END) AS March,
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 4 THEN 1 ELSE 0 END) AS April,
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 5 THEN 1 ELSE 0 END) AS May,
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 6 THEN 1 ELSE 0 END) AS June,
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 7 THEN 1 ELSE 0 END) AS July,
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 8 THEN 1 ELSE 0 END) AS August,
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 9 THEN 1 ELSE 0 END) AS September,
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 10 THEN 1 ELSE 0 END) AS October,
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 11 THEN 1 ELSE 0 END) AS November,
    SUM(CASE WHEN DATEPART(MONTH, s.created_date) = 12 THEN 1 ELSE 0 END) AS December
FROM
    ArtHub.dbo.subscriber s
WHERE
    s.created_date >= DATEFROMPARTS(YEAR(GETDATE()), 1, 1)
    AND s.created_date < DATEADD(YEAR, 1, DATEFROMPARTS(YEAR(GETDATE()), 1, 1))   	
	AND s.email_artist =  @ArtistEmail;