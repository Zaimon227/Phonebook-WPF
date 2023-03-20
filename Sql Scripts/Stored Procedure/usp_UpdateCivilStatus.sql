IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_UpdateCivilStatus') DROP PROCEDURE usp_UpdateCivilStatus
GO
CREATE PROCEDURE usp_UpdateCivilStatus
(  
	@CivilStatusID int,
	@Name varchar(50),
	@Description varchar(max),
	@Updated_by varchar(50)
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	UPDATE dbo.tbCivilStatus
	SET name = @Name, description = @Description, updated_by = @Updated_by, updated_datetime = GETDATE()
	WHERE civilstatusid = @CivilStatusID
end
GO