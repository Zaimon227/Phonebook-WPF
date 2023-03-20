IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_UpdateNationality') DROP PROCEDURE usp_UpdateNationality
GO
CREATE PROCEDURE usp_UpdateNationality
(  
	@NationalityID int,
	@Name varchar(50),
	@Description varchar(max),
	@Updated_by varchar(50)
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	UPDATE dbo.tbNationality
	SET name = @Name, description = @Description, updated_by = @Updated_by, updated_datetime = GETDATE()
	WHERE nationalityid = @NationalityID
end
GO