IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetNationalities') DROP PROCEDURE usp_GetNationalities
GO
CREATE PROCEDURE usp_GetNationalities
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
					nationalityid,
					nationalityname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbNationality
			end
		else
			begin
				SELECT
					nationalityid,
					nationalityname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbNationality
				WHERE nationalityname like @Keyword+'%'
			end
	end
else
	if (isnull(@Keyword,'') = '')
		begin
			WITH getNationalityData as 
			(
				SELECT 
					ROW_NUMBER() OVER(ORDER BY nationalityid) as SrNo, 
					nationalityid,
					nationalityname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbNationality
			)
			SELECT 
				nationalityid,
				nationalityname,
				description,
				created_by,
				created_datetime,
				is_active
			FROM getNationalityData
			WHERE SrNo BETWEEN @StartRow AND @EndRow
		end
	else
		begin
			WITH getNationalityData as 
			(
				SELECT 
					ROW_NUMBER() OVER(ORDER BY nationalityid) as SrNo, 
					nationalityid,
					nationalityname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbNationality 
				WHERE nationalityname like @Keyword+'%'
			)
			SELECT
				nationalityid,
				nationalityname,
				description,
				created_by,
				created_datetime,
				is_active
			FROM getNationalityData
			WHERE SrNo BETWEEN @StartRow AND @EndRow
		end
GO