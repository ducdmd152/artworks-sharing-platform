/*
    CreatedBy: Tien
    Date: 03/19/2024
    
    @IsGetDataPost bool //For get data of post if audience subscribe or for moderator and admin
    @Email string //Email of artist
    @PostScope int[] //Scope of post in range
    @PostStatus int[] //Status of post in range
    @IsOrderByReact bool //Order by react otherwise default by created date
    @IsOrderByView bool //Order by view otherwise default by created date
    @IsOrderByTitle bool //Order by view otherwise default by created date
    @IsOrderAsc bool //Order by ASC otherwise by DESC
    @PageNum int //Page number of post
    @PageSize int //Page size of post
    @AccountStatus int //Account status
    @AccountIsEnable bool //Account enable
*/

WITH PostDetailsJson AS (
    SELECT
        CASE WHEN @IsGetDataPost = 1 THEN
            (
            SELECT
                p.post_id AS PostId,
                p.title AS Title,
                p.description AS Description,
                p.status AS Status,
                p.scope AS Scope,
                p.total_react AS TotalReact,
                p.total_view AS TotalView,
                p.total_bookmark AS TotalBookmark,
                p.artist_email AS ArtistEmail,
                STRING_AGG(i.image_url, ',') AS ImageUrls
            FROM
                ArtHub.dbo.post p
            JOIN 
                ArtHub.dbo.[image] i ON
                p.post_id = i.post_id
            WHERE
                artist_email = @Email
                AND scope IN (SELECT value FROM STRING_SPLIT(@PostScope, ','))
                AND status IN (SELECT value FROM STRING_SPLIT(@PostStatus, ','))
            GROUP BY
                p.post_id,
                p.title,
                p.description,
                p.status,
                p.scope,
                p.total_react,
                p.total_view,
                p.total_bookmark,
                p.artist_email,
                p.created_date
            ORDER BY
                CASE
		            WHEN @IsOrderByReact = 1 THEN
                        CASE
			            WHEN @IsOrderAsc = 1 THEN p.total_react
			            ELSE -p.total_react
		            END
		            WHEN @IsOrderByView = 1 THEN
                        CASE
			            WHEN @IsOrderAsc = 1 THEN p.total_view 
			            ELSE -p.total_view 
		            END
                    WHEN @IsOrderByTitle = 1 THEN
                        CASE
                        WHEN @IsOrderAsc = 1 THEN LEN(p.title)
                        ELSE -LEN(p.title)
		            END
		            ELSE
                        CASE
			            WHEN @IsOrderAsc = 1 THEN CAST(p.created_date AS BIGINT)
			            ELSE -CAST(p.created_date AS BIGINT)
		            END
	            END
            OFFSET (@PageNum - 1) * @PageSize ROWS
            FETCH NEXT @PageSize ROWS ONLY
            FOR JSON PATH
            )
        ELSE NULL END AS PostDetail
), TotalPostCount AS (
	SELECT 
		CASE WHEN @IsGetDataPost = 1 THEN
			(
			SELECT
				COUNT(p.post_id) AS TotalPostCount
			FROM
				ArtHub.dbo.post p
			WHERE
                artist_email = @Email
                AND scope IN (SELECT value FROM STRING_SPLIT(@PostScope, ','))
                AND status IN (SELECT value FROM STRING_SPLIT(@PostStatus, ','))
		    ) ELSE NULL END AS TotalPostCount
)
SELECT
    a.email AS Email,
    MAX(a.avatar) AS Avatar,
    MAX(art.artist_name) AS ArtistName,
    MAX(art.bio) AS Bio,
    MAX(art.total_subscribe) AS TotalSubscriber,
    SUM(total_react) AS TotalReact,
    SUM(total_view) AS TotalView,
    SUM(total_bookmark) AS TotalBookmark,
    MAX(PostDetailsJson.PostDetail) AS PostDetail,
    MAX(TotalPostCount.TotalPostCount) AS TotalPostCount
FROM 
    ArtHub.dbo.post p
INNER JOIN ArtHub.dbo.account a ON p.artist_email = a.email
INNER JOIN ArtHub.dbo.artist art ON p.artist_email = art.email
CROSS JOIN PostDetailsJson
CROSS JOIN TotalPostCount
WHERE
    p.artist_email = @Email
    AND a.status = @AccountStatus
    AND a.enabled = @AccountIsEnable
GROUP BY a.email;

