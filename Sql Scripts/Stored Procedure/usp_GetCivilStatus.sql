IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetCivilStatus') DROP PROCEDURE usp_GetCivilStatus
GO
CREATE PROCEDURE usp_GetCivilStatus
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
					civilstatusid,
					civilstatusname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbCivilStatus
			end
		else
			begin
				SELECT
					civilstatusid,
					civilstatusname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbCivilStatus
				WHERE civilstatusname like @Keyword+'%'
			end
	end
else
	if (isnull(@Keyword,'') = '')
		begin
			WITH getCivilStatusData as 
			(
				SELECT 
					ROW_NUMBER() OVER(ORDER BY civilstatusid) as SrNo, 
					civilstatusid,
					civilstatusname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbCivilStatus
			)
			SELECT
				civilstatusid,
				civilstatusname,
				description,
				created_by,
				created_datetime,
				is_active
			FROM getCivilStatusData
			WHERE SrNo BETWEEN @StartRow AND @EndRow
		end
	else
		begin
			WITH getCivilStatusData as 
			(
				SELECT 
					ROW_NUMBER() OVER(ORDER BY civilstatusid) as SrNo,
					civilstatusid,
					civilstatusname,
					description,
					created_by,
					created_datetime,
					is_active
				FROM tbCivilStatus 
				WHERE civilstatusname like @Keyword+'%'
			)
			SELECT
				civilstatusid,
				civilstatusname,
				description,
				created_by,
				created_datetime,
				is_active
			FROM getCivilStatusData
			WHERE SrNo BETWEEN @StartRow AND @EndRow
		end
GO