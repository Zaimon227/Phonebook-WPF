IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_DeleteContact') DROP PROCEDURE usp_DeleteContact
GO
CREATE PROCEDURE usp_DeleteContact
(  
	@ID int
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	DELETE FROM dbo.tbContacts
	WHERE id = @ID
end
GO