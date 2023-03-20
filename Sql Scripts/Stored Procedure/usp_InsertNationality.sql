IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_InsertNationality') DROP PROCEDURE usp_InsertNationality
GO
CREATE PROCEDURE usp_InsertNationality
(  
	@Name varchar(50),
	@Description varchar(max),
	@Created_by varchar(50)
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	INSERT INTO dbo.tbNationality(name, description, is_active, created_by, created_datetime)
	VALUES (@Name, @Description, 0, @Created_by, GETDATE())
end
GO