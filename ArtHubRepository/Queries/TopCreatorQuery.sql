WITH CreatorRevenue AS (
    SELECT
        artist.email AS CreatorEmail,
        artist.artist_name AS CreatorName,
        artist.total_subscribe AS TotalSubscribe,
        fee.amount AS Fee,
        SUM([transaction].amount) AS Revenue,
        ROW_NUMBER() OVER (ORDER BY artist.email) AS RowNum
    FROM 
        ArtHub.dbo.artist AS artist
    JOIN 
        ArtHub.dbo.fee AS fee ON artist.email = fee.artist_email
    JOIN 
        ArtHub.dbo.[transaction] AS [transaction] ON [transaction].fee_id = fee.fee_id
    GROUP BY 
        artist.email,
        artist.artist_name,
        artist.total_subscribe,
        fee.amount
),
WithArtworkLove AS (
SELECT 
	artist.email,
	SUM(p.total_react ) AS TotalLove
FROM 
    ArtHub.dbo.artist AS artist
	INNER JOIN post p ON artist.artist_name = p.artist_email
GROUP BY
	artist.email

)
SELECT
    CEILING(CONVERT(DECIMAL, COUNT(*)) / 10) AS TotalPages,
    COUNT(*) AS TotalItems,
    cr.CreatorEmail AS CreatorEmail ,
    cr.CreatorName AS CreatorName ,
    cr.TotalSubscribe AS TotalSubscribe,
    cr.Fee AS Fee ,
	CASE 
		WHEN  art.TotalLove IS NULL THEN 0
		ELSE art.TotalLove
	END AS TotalLove,
    cr.Revenue AS Revenue
FROM 
    CreatorRevenue AS cr
	LEFT JOIN WithArtworkLove AS art ON cr.CreatorEmail = art.email
WHERE
    cr.RowNum BETWEEN (1 - 1) * 10 + 1 AND 1 * 10
GROUP BY 
    cr.CreatorEmail,
    cr.CreatorName,
    cr.TotalSubscribe,
    cr.Fee,
    art.TotalLove,
    cr.Revenue;
