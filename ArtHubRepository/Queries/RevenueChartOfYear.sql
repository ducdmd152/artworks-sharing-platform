SELECT
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'January' THEN t.amount ELSE 0 END) AS January,
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'February' THEN t.amount ELSE 0 END) AS February,
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'March' THEN t.amount ELSE 0 END) AS March,
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'April' THEN t.amount ELSE 0 END) AS April,
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'May' THEN t.amount ELSE 0 END) AS May,
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'June' THEN t.amount ELSE 0 END) AS June,
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'July' THEN t.amount ELSE 0 END) AS July,
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'August' THEN t.amount ELSE 0 END) AS August,
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'September' THEN t.amount ELSE 0 END) AS September,
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'October' THEN t.amount ELSE 0 END) AS October,
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'November' THEN t.amount ELSE 0 END) AS November,
    SUM(CASE WHEN DATENAME(MONTH, t.created_date) = 'December' THEN t.amount ELSE 0 END) AS December
FROM
    ArtHub.dbo.[transaction] t
INNER JOIN
    ArtHub.dbo.fee f ON t.fee_id = f.fee_id
WHERE
	t.created_date >= DATEFROMPARTS(YEAR(GETDATE()), 1, 1)
    AND t.created_date < DATEADD(YEAR, 1, DATEFROMPARTS(YEAR(GETDATE()), 1, 1))