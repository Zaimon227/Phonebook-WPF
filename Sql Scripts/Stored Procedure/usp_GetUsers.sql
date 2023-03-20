IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetUsers') DROP PROCEDURE usp_GetUsers
GO
CREATE PROCEDURE usp_GetUsers
(  
	@Page int = null,
	@Size int = null,
	@Keyword nvarchar(100) = null
)
AS 
SET NOCOUNT OFF 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	DECLARE
		@StartRow int,
		@EndRow int

	SET @StartRow = ((@Page-1)*@Size)+1;
	SET @EndRow = @Page*@Size;

if (isnull(@Page,'') = '' AND isnull(@Size,'') = '')
	begin
		if (isnull(@Keyword, '') = '')
			begin
				SELECT 
					CONCAT(lastname, ', ', firstname, ' ', middlename) as [completename],
					userid, 
					gender,
					birthdate,
					tbReligion.religionname,
					tbNationality.nationalityname,
					tbCivilStatus.civilstatusname,
					email
				FROM tbUsers
				INNER JOIN tbReligion ON tbReligion.religionid = tbUsers.religionid
				INNER JOIN tbNationality ON tbNationality.nationalityid = tbUsers.nationalityid
				INNER JOIN tbCivilStatus ON tbCivilStatus.civilstatusid = tbUsers.civilstatusid
			end
		else
			begin
				SELECT 
					CONCAT(lastname, ', ', firstname, ' ', middlename) as [completename],
					userid, 
					gender,
					birthdate,
					tbReligion.religionname,
					tbNationality.nationalityname,
					tbCivilStatus.civilstatusname,
					email
				FROM tbUsers
				INNER JOIN tbReligion ON tbReligion.religionid = tbUsers.religionid
				INNER JOIN tbNationality ON tbNationality.nationalityid = tbUsers.nationalityid
				INNER JOIN tbCivilStatus ON tbCivilStatus.civilstatusid = tbUsers.civilstatusid
				WHERE firstname like @Keyword+'%'
			end
	end
else
	if (isnull(@Keyword,'') = '')
		begin
			WITH getUserData as 
			(
				SELECT 
					ROW_NUMBER() OVER(ORDER BY userid) as SrNo,
					CONCAT(lastname, ', ', firstname, ' ', middlename) as [completename],
					userid, 
					gender,
					birthdate,
					tbReligion.religionname,
					tbNationality.nationalityname,
					tbCivilStatus.civilstatusname,
					email
				FROM tbUsers
				INNER JOIN tbReligion ON tbReligion.religionid = tbUsers.religionid
				INNER JOIN tbNationality ON tbNationality.nationalityid = tbUsers.nationalityid
				INNER JOIN tbCivilStatus ON tbCivilStatus.civilstatusid = tbUsers.civilstatusid
			)
			SELECT
				completename,
				userid, 
				gender,
				birthdate,
				religionname,
				nationalityname,
				civilstatusname,
				email
			FROM getUserData
			WHERE SrNo BETWEEN @StartRow AND @EndRow
		end
	else
		begin
			WITH getUserData as 
			(
				SELECT 
					ROW_NUMBER() OVER(ORDER BY userid) as SrNo, 
					CONCAT(lastname, ', ', firstname, ' ', middlename) as [completename],
					userid, 
					gender,
					birthdate,
					tbReligion.religionname,
					tbNationality.nationalityname,
					tbCivilStatus.civilstatusname,
					email
				FROM tbUsers
				INNER JOIN tbReligion ON tbReligion.religionid = tbUsers.religionid
				INNER JOIN tbNationality ON tbNationality.nationalityid = tbUsers.nationalityid
				INNER JOIN tbCivilStatus ON tbCivilStatus.civilstatusid = tbUsers.civilstatusid
				WHERE firstname like @Keyword+'%'
			)
			SELECT
				completename,
				userid, 
				gender,
				birthdate,
				religionname,
				nationalityname,
				civilstatusname,
				email
			FROM getUserData
			WHERE SrNo BETWEEN @StartRow AND @EndRow
		end
GO