IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_InsertReligion') DROP PROCEDURE usp_InsertReligion
GO
CREATE PROCEDURE usp_InsertReligion
(  
	@Name varchar(50),
	@Description varchar(max),
	@Created_by varchar(50)
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	INSERT INTO dbo.tbReligion (name, description, is_active, created_by, created_datetime)
	VALUES (@Name, @Description, 0, @Created_by, GETDATE())
end
GO