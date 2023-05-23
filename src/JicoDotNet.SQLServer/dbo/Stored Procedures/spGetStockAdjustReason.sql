CREATE PROCEDURE [dbo].[spGetStockAdjustReason]
(
	@QueryType	varchar(10)
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
				FROM dbo.sStockAdjustReason
				WHERE IsDefault = 1
					AND IsActive = 1
				UNION
				SELECT *
				FROM dbo.sStockAdjustReason
					WHERE IsDefault = 0
					AND IsActive = 1;
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