IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_Login') DROP PROCEDURE usp_Login
GO
CREATE PROCEDURE usp_Login
(  
	@Email varchar(50),
	@Password varchar(50)
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	SELECT
		email,
		password
	FROM tbUsers 
	WHERE email=@Email and password=@Password
end
GO

exec usp_GetContacts