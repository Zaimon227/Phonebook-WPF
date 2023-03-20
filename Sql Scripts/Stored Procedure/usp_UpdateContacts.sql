IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_UpdateContacts') DROP PROCEDURE usp_UpdateContacts
GO
CREATE PROCEDURE usp_UpdateContacts
(  
	@ID int,
	@Name varchar(max),
	@Address varchar(max),
	@Email varchar(max),
	@Contact varchar(max)
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	UPDATE dbo.tbContacts
	SET name = @Name, address = @Address, email = @Email, contact = @Contact
	WHERE id = @ID
end
GO