IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetReligions') DROP PROCEDURE usp_GetReligions
GO
CREATE PROCEDURE usp_GetReligions
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
					religionid,
					religionname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbReligion
			end
		else
			begin
				SELECT
					religionid,
					religionname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbReligion 
				WHERE religionname like @Keyword+'%'
			end
	end
else
	if (isnull(@Keyword,'') = '')
		begin
			WITH getReligionData as 
			(
				SELECT 
					ROW_NUMBER() OVER(ORDER BY religionid) as SrNo,
					religionid,
					religionname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbReligion
			)
			SELECT
				religionid,
				religionname,
				description,
				created_by,
				created_datetime,
				is_active
			FROM getReligionData
			WHERE SrNo BETWEEN @StartRow AND @EndRow
		end
	else
		begin
			WITH getReligionData as 
			(
				SELECT 
					ROW_NUMBER() OVER(ORDER BY religionid) as SrNo,
					religionid,
					religionname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbReligion WHERE religionname like @Keyword+'%'
			)
			SELECT
				religionid,
				religionname,
				description,
				created_by,
				created_datetime,
				is_active
			FROM getReligionData
			WHERE SrNo BETWEEN @StartRow AND @EndRow
		end
GO