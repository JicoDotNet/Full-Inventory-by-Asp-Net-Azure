-- ======================================================================================
-- Author:  <Soubhik Nandy>
-- Create date: <08th May, 2020>
-- Description: <Send Keywords and find out the name of the SPS having the keyword>
-- ======================================================================================
CREATE PROCEDURE [dbo].[_FindByKeyWord]
 @keyword  NVARCHAR(MAX)
AS
BEGIN
 SET NOCOUNT ON;
	SELECT ROUTINE_NAME as 'Name', ROUTINE_TYPE as 'Type', ROUTINE_DEFINITION as 'Definition'
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_DEFINITION LIKE '%' + @keyword + '%'
		AND (ROUTINE_TYPE='PROCEDURE'
		OR ROUTINE_TYPE='FUNCTION')
	ORDER BY ROUTINE_NAME
END