CREATE PROCEDURE [SingleIB].[spSetPurchaseType]
(
	@PurchaseTypeId		int = 0,

	@PurchaseTypeName	nvarchar(128) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN				
				INSERT INTO SingleIB.mPurchaseType
					(PurchaseTypeName,
					[Description],
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@PurchaseTypeName, 
					@Description, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mPurchaseType WHERE PurchaseTypeId = @PurchaseTypeId)
				BEGIN
					UPDATE SingleIB.mPurchaseType
					SET PurchaseTypeName = @PurchaseTypeName,
						Description = @Description,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE PurchaseTypeId = @PurchaseTypeId;
					SET @OutParam = @PurchaseTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mPurchaseType WHERE PurchaseTypeId = @PurchaseTypeId)
				BEGIN
					UPDATE SingleIB.mPurchaseType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE PurchaseTypeId = @PurchaseTypeId;
					SET @OutParam = @PurchaseTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END