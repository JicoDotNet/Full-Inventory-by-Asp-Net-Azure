CREATE PROCEDURE [SingleIB].[spSetStockAdjustReason]
(
	@AdjustReason		nvarchar(64) = NULL,

	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(10),
	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Insert';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.sStockAdjustReason
					(AdjustReason,
					IsDefault,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@AdjustReason, 
					0, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId)

				SET @OutParam = SCOPE_IDENTITY();
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