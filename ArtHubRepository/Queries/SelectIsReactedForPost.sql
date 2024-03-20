/*
    CreatedBy: DucDMD
    Date: 15/03/2024
    
    @AccountEmail string
    @PostId int
*/
SELECT 
	CASE 
	WHEN EXISTS (SELECT 1 FROM reaction WHERE account_email = @AccountEmail AND post_id = @PostId) 
	THEN 'true' ELSE 'false' END;