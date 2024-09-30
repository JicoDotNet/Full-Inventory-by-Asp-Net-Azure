CREATE PROCEDURE [SingleIB].[spSetVendorBank]
(
	@VendorBankId		bigint = 0,
	@VendorId			bigint = 0,
	
	@AccountName		nvarchar(1204) = NULL,
	@AccountNumber		nvarchar(64) = NULL,
	@BankName			nvarchar(128) = NULL,
	@IFSC				nvarchar(16) = NULL,
	@MICR				nvarchar(16) = NULL,
	@BranchName			nvarchar(64) = NULL,
	@BranchAddress		nvarchar(128) = NULL,

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
				INSERT INTO SingleIB.mVendorBank
					(VendorId,					
					AccountName,
					AccountNumber,
					BankName,
					IFSC,
					MICR,
					BranchName,
					BranchAddress,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@VendorId, 					
					@AccountName, 
					@AccountNumber, 
					@BankName, 
					@IFSC, 
					@MICR, 
					@BranchName, 
					@BranchAddress, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendorBank WHERE VendorBankId = @VendorBankId AND VendorId = @VendorId)
				BEGIN
					UPDATE SingleIB.mVendorBank
					SET AccountName = @AccountName, 
						AccountNumber = @AccountNumber, 
						BankName = @BankName, 
						IFSC = @IFSC, 
						MICR = @MICR, 
						BranchName = @BranchName, 
						BranchAddress = @BranchAddress, 
						IsActive = 1, 
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE VendorBankId = @VendorBankId 
						AND VendorId = @VendorId;
					SET @OutParam = @VendorBankId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendorBank WHERE VendorBankId = @VendorBankId AND VendorId = @VendorId)
				BEGIN
					UPDATE SingleIB.mVendorBank
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE VendorBankId = @VendorBankId 
						AND VendorId = @VendorId;
					SET @OutParam = @VendorBankId;
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