IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_InsertContact') DROP PROCEDURE usp_InsertContact
GO
CREATE PROCEDURE usp_InsertContact
(  
	@Name varchar(max),
	@Address varchar(max),
	@Email varchar(max),
	@Contact varchar(max)
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	INSERT INTO dbo.tbContacts (name, address, email, contact)
	VALUES (@Name, @Address, @Email, @Contact)
end
GO