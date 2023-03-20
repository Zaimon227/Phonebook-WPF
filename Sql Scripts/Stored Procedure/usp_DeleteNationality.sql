IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_DeleteNationality') DROP PROCEDURE usp_DeleteNationality
GO
CREATE PROCEDURE usp_DeleteNationality
(  
	@NationalityID int
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	DELETE FROM dbo.tbNationality
	WHERE nationalityid = @NationalityID
end
GO