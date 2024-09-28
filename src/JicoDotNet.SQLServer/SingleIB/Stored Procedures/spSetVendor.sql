CREATE PROCEDURE [SingleIB].[spSetVendor]
(
	@VendorId			bigint = 0,
	@VendorTypeId		bigint = 0,
	
	@ContactPerson		nvarchar(64) = NULL,
	@Email				nvarchar(64) = NULL,
	@Mobile				nvarchar(16) = NULL,

	@CompanyName		nvarchar(1204) = NULL,
	@CompanyType		nvarchar(64) = NULL,
	@StateCode			nvarchar(2) = NULL,
	@IsGSTRegistered		bit = 0,
	@GSTStateCode		nvarchar(2) = NULL,
	@GSTNumber			nvarchar(16) = NULL,
	@PANNumber			nvarchar(16) = NULL,

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
				INSERT INTO SingleIB.mVendor
					(VendorTypeId,					
					CompanyName,
					CompanyType,
					StateCode,
					IsGSTRegistered,
					GSTStateCode,
					GSTNumber,
					PANNumber,
					ContactPerson,
					Email,
					Mobile,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@VendorTypeId, 
					@CompanyName, 
					@CompanyType, 
					@StateCode,
					@IsGSTRegistered,
					@GSTStateCode,
					@GSTNumber, 
					@PANNumber, 
					@ContactPerson,
					@Email,
					@Mobile,
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendor WHERE VendorId = @VendorId)
				BEGIN
					UPDATE SingleIB.mVendor
					SET VendorTypeId = @VendorTypeId, 						
						CompanyName = @CompanyName, 
						CompanyType = @CompanyType, 
						StateCode = @StateCode,
						IsGSTRegistered = @IsGSTRegistered, 
						GSTStateCode = @GSTStateCode,
						GSTNumber = @GSTNumber, 
						PANNumber = @PANNumber, 
						ContactPerson = @ContactPerson,
						Email = @Email,
						Mobile = @Mobile,
						IsActive = 1, 
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE VendorId = @VendorId;
					SET @OutParam = @VendorId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendor WHERE VendorId = @VendorId)
				BEGIN
					UPDATE SingleIB.mVendor
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE VendorId = @VendorId;
					SET @OutParam = @VendorId;
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