



SELECT 
    artist.email AS artist_email,
    artist.artist_name,
    artist.total_subscribe,
    fee.amount as FEE,
    SUM(post.total_react) AS Love,
    [transaction].amount as Revenue 
FROM 
    ArtHub.dbo.artist AS artist
JOIN 
    ArtHub.dbo.fee AS fee ON artist.email = fee.artist_email
JOIN 
    ArtHub.dbo.post AS post ON artist.email = post.artist_email
JOIN 
    ArtHub.dbo.[transaction] AS [transaction] ON [transaction].fee_id = fee.fee_id

WHERE [transaction].created_date between @StartDate and @EndDate

GROUP BY 
    artist.email,
    artist.artist_name,
    artist.total_subscribe,
    fee.amount,
    [transaction].amount;