IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_UpdateReligion') DROP PROCEDURE usp_UpdateReligion
GO
CREATE PROCEDURE usp_UpdateReligion
(  
	@ReligionID int,
	@Name varchar(50),
	@Description varchar(max),
	@Updated_by varchar(50)
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	UPDATE dbo.tbReligion
	SET name = @Name, description = @Description, updated_by = @Updated_by, updated_datetime = GETDATE()
	WHERE religionid = @ReligionID
end
GO