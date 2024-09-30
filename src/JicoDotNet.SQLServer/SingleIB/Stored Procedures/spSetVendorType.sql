CREATE PROCEDURE [SingleIB].[spSetVendorType]
(
	@VendorTypeId		bigint = 0,
	@VendorTypeName		nvarchar(128) = NULL,
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
				INSERT INTO SingleIB.mVendorType
					(VendorTypeName,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@VendorTypeName, 
					@Description, 
					1,
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendorType WHERE VendorTypeId = @VendorTypeId)
				BEGIN
					UPDATE SingleIB.mVendorType
					SET VendorTypeName = @VendorTypeName, 
						Description = @Description, 
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE VendorTypeId = @VendorTypeId;
					SET @OutParam = @VendorTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendorType WHERE VendorTypeId = @VendorTypeId)
				BEGIN
					UPDATE SingleIB.mVendorType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE VendorTypeId = @VendorTypeId;
					SET @OutParam = @VendorTypeId;
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