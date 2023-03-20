IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_DeleteCivilStatus') DROP PROCEDURE usp_DeleteCivilStatus
GO
CREATE PROCEDURE usp_DeleteCivilStatus
(  
	@CivilStatusID int
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	DELETE FROM dbo.tbCivilStatus
	WHERE civilstatusid = @CivilStatusID
end
GO