/*
    CreatedBy: Chau
    Date: 03/22/2024
    
    @PageSize int
    @PageNumber int 
    @Email varchar(256) 
    @Status int
    @RoleId int
*/

WITH AccountListView AS (
    SELECT
        acc.email,
        acc.first_name,
        acc.last_name,
        acc.gender,
        acc.status,
        acc.enabled,
        acc.avatar,
        
        acc.created_date AS account_created_date,
        acc.updated_date AS account_updated_date,
        ROW_NUMBER() OVER (ORDER BY acc.created_date DESC) AS RowNum,
        COUNT(*) OVER () AS TotalRecords
    FROM
        account AS acc
    WHERE
       /* (@Email IS NULL OR acc.email = @Email)
        AND (@Status IS NULL OR acc.status = @Status)*/
        (@Email IS NULL OR acc.email = @Email)
        AND (@Status IS NULL OR acc.status = @Status)
       
       
)
SELECT
    CEILING(CONVERT(decimal, acc.TotalRecords) / @PageSize) AS TotalPages,
    acc.TotalRecords AS TotalItems,
    acc.email As Email,
    acc.first_name AS FirstName,
    acc.last_name As LastName,
    acc.gender AS Gender,
    acc.status As Status,
    acc.enabled As Enabled,
    acc.avatar As Avatar,
  
    acc.account_created_date AS AccountCreateDate,
    acc.account_updated_date As AccountUpdateDate
FROM
    AccountListView as acc
WHERE
    acc.RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize;
