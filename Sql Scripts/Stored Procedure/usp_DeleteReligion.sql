IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_DeleteReligion') DROP PROCEDURE usp_DeleteReligion
GO
CREATE PROCEDURE usp_DeleteReligion
(  
	@ReligionID int
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	DELETE FROM dbo.tbReligion
	WHERE religionid = @ReligionID
end
GO