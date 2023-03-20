IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_InsertCivilStatus') DROP PROCEDURE usp_InsertCivilStatus
GO
CREATE PROCEDURE usp_InsertCivilStatus
(  
	@Name varchar(50),
	@Description varchar(max),
	@Created_by varchar(50)
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	INSERT INTO dbo.tbCivilStatus(name, description, is_active, created_by, created_datetime)
	VALUES (@Name, @Description, 0, @Created_by, GETDATE())
end
GO