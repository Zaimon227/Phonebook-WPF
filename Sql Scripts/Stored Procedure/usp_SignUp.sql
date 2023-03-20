IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_SignUp') DROP PROCEDURE usp_SignUp
GO
CREATE PROCEDURE usp_SignUp
(  
	@Firstname varchar(50),
	@Middlename varchar(50),
	@Lastname varchar(50),
	@Gender varchar(50),
	@Birthdate date,
	@ReligionID int,
	@NationalityID int,
	@Email varchar(50),
	@Password varchar(50),
	@Created_by varchar(50)
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
begin
	INSERT INTO dbo.tbUsers 
	(
		firstname, 
		middlename, 
		lastname, 
		gender, 
		birthdate, 
		religionid,
		nationalityid,
		email, 
		password, 
		is_active, 
		created_datetime, 
		created_by
	)
	VALUES 
	(
		@Firstname, 
		@Middlename, 
		@Lastname, 
		@Gender, 
		@Birthdate, 
		@ReligionID,
		@NationalityID,
		@Email, 
		@Password, 
		0, 
		GETDATE(), 
		@Created_by
	)
end
GO