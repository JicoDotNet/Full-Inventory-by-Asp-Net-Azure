﻿CREATE PROCEDURE [dbo].[spGetCompanyBank]
(
	@QueryType		varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM dbo.mCompanyBank
				ORDER BY IsActive Desc;
			END
			ELSE IF (@QueryType ='PRINTABLE')
			BEGIN
				SELECT *
				FROM dbo.mCompanyBank
				WHERE IsActive = 1
					AND IsPrintable = 1;
			END
		COMMIT TRANSACTION @T1;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END