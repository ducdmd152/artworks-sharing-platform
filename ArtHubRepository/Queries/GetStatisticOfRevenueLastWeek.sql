/*
    CreatedBy: Tien
    Date: 03/23/2024
       
    @@ArtistEmail string //Email of artist
*/

SELECT    
    SUM(CASE WHEN DATENAME(dw, t.created_date) = 'Monday' THEN t.amount ELSE 0 END) AS Monday,
    SUM(CASE WHEN DATENAME(dw, t.created_date) = 'Tuesday' THEN t.amount ELSE 0 END) AS Tuesday,
    SUM(CASE WHEN DATENAME(dw, t.created_date) = 'Wednesday' THEN t.amount ELSE 0 END) AS Wednesday,
    SUM(CASE WHEN DATENAME(dw, t.created_date) = 'Thursday' THEN t.amount ELSE 0 END) AS Thursday,
    SUM(CASE WHEN DATENAME(dw, t.created_date) = 'Friday' THEN t.amount ELSE 0 END) AS Friday,
    SUM(CASE WHEN DATENAME(dw, t.created_date) = 'Saturday' THEN t.amount ELSE 0 END) AS Saturday,
    SUM(CASE WHEN DATENAME(dw, t.created_date) = 'Sunday' THEN t.amount ELSE 0 END) AS Sunday
FROM
    ArtHub.dbo.[transaction] t
INNER JOIN
    ArtHub.dbo.fee f ON t.fee_id = f.fee_id
WHERE
	t.created_date >= DATEADD(DAY, -DATEPART(WEEKDAY, GETDATE()) - 6, CAST(GETDATE() AS DATE))
	AND t.created_date < DATEADD(DAY, -DATEPART(WEEKDAY, GETDATE()) + 2, CAST(GETDATE() AS DATE))
	AND f.artist_email = @ArtistEmail;