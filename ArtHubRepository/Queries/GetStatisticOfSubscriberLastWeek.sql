/*
    CreatedBy: Tien
    Date: 03/23/2024
       
    @@ArtistEmail string //Email of artist
*/

SELECT
	SUM(CASE WHEN DATENAME(WEEKDAY, s.created_date) = 'Monday' THEN 1 ELSE 0 END) AS Monday,
	SUM(CASE WHEN DATENAME(WEEKDAY, s.created_date) = 'Tuesday' THEN 1 ELSE 0 END) AS Tuesday,
	SUM(CASE WHEN DATENAME(WEEKDAY, s.created_date) = 'Wednesday' THEN 1 ELSE 0 END) AS Wednesday,
	SUM(CASE WHEN DATENAME(WEEKDAY, s.created_date) = 'Thursday' THEN 1 ELSE 0 END) AS Thursday,
	SUM(CASE WHEN DATENAME(WEEKDAY, s.created_date) = 'Friday' THEN 1 ELSE 0 END) AS Friday,
	SUM(CASE WHEN DATENAME(WEEKDAY, s.created_date) = 'Saturday' THEN 1 ELSE 0 END) AS Saturday,
	SUM(CASE WHEN DATENAME(WEEKDAY, s.created_date) = 'Sunday' THEN 1 ELSE 0 END) AS Sunday
FROM
	ArtHub.dbo.subscriber s
WHERE
	s.created_date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) - 2, 0)
	AND s.created_date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) - 1, 0)
	AND s.email_artist =  @ArtistEmail;