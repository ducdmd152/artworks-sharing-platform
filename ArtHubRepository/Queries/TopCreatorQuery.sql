/*
DECLARE @PageNumber INT = 2;
DECLARE @PageSize INT = 4;
DECLARE @AudienceEmail VARCHAR(100) = 'user1@gmail.com';
DECLARE @CreatorEmail VARCHAR(100) = 'creator@gmail.com';
DECLARE @Email VARCHAR(100) = '';
*/

WITH CreatorRevenue AS (
    SELECT
        artist.email AS CreatorEmail,
        artist.artist_name AS CreatorName,
        artist.total_subscribe AS TotalSubscribe,
        fee.amount AS Fee,
        SUM([transaction].amount) AS Revenue,
		CEILING(COUNT(artist.email) OVER () * 1.0 / @PageSize) AS TotalPages,
		COUNT(artist.email) OVER () AS TotalItems,
        ROW_NUMBER() OVER (ORDER BY SUM([transaction].amount) DESC) AS RowNum
    FROM 
        ArtHub.dbo.artist AS artist
    JOIN 
        ArtHub.dbo.fee AS fee ON artist.email = fee.artist_email
    JOIN 
        ArtHub.dbo.[transaction] AS [transaction] ON [transaction].fee_id = fee.fee_id
    WHERE
        (@Email IS NULL OR artist.email LIKE ('%' + @Email +'%'))
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
    INNER JOIN 
        post p ON artist.artist_name = p.artist_email
    GROUP BY
        artist.email
)

SELECT
    cr.TotalPages AS TotalPages,
	cr.TotalItems AS TotalItems,
	cr.CreatorEmail AS CreatorEmail,
    cr.CreatorName AS CreatorName,
    cr.TotalSubscribe AS TotalSubscribe,
    cr.Fee AS Fee,
    CASE 
        WHEN art.TotalLove IS NULL THEN 0
        ELSE art.TotalLove
    END AS TotalLove,
    cr.Revenue AS Revenue
FROM 
    CreatorRevenue AS cr
LEFT JOIN 
    WithArtworkLove AS art ON cr.CreatorEmail = art.email
WHERE
    cr.RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize
GROUP BY 
    cr.CreatorEmail,
    cr.CreatorName,
    cr.TotalSubscribe,
    cr.Fee,
    art.TotalLove,
    cr.Revenue,
	cr.TotalPages,
	cr.TotalItems
ORDER BY
    cr.Revenue DESC;