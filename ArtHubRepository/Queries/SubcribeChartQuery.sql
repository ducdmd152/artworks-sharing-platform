WITH RankedSubscribers AS (
    SELECT
        email_user,
        CONVERT(date, created_date) AS RegistrationDate,
        ROW_NUMBER() OVER (PARTITION BY email_user ORDER BY created_date) AS RowNum
    FROM
        subscriber
    WHERE
        CONVERT(date, created_date) BETWEEN @StartDate AND @EndDate
)
SELECT
    RegistrationDate,
    COUNT(*) AS TotalUniqueSubscribers                                                                                                                                           
FROM
    RankedSubscribers
WHERE
    RowNum = 1
GROUP BY
    RegistrationDate
ORDER BY
    RegistrationDate;