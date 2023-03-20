IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetContacts') DROP PROCEDURE usp_GetContacts
GO
CREATE PROCEDURE usp_GetContacts
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
				SELECT *
				FROM tbContacts
			end
		else
			begin
				SELECT *
				FROM tbContacts
				WHERE name like @Keyword+'%'
			end
	end
else
	if (isnull(@Keyword,'') = '')
		begin
			WITH getContactData as 
			(
				SELECT ROW_NUMBER() OVER(ORDER BY id) as SrNo, * FROM tbContacts
			)
			SELECT * 
			FROM getContactData
			WHERE SrNo BETWEEN @StartRow AND @EndRow
		end
	else
		begin
			WITH getContactData as 
			(
				SELECT ROW_NUMBER() OVER(ORDER BY id) as SrNo, * FROM tbContacts WHERE name like @Keyword+'%'
			)
			SELECT * 
			FROM getContactData
			WHERE SrNo BETWEEN @StartRow AND @EndRow
		end
GO