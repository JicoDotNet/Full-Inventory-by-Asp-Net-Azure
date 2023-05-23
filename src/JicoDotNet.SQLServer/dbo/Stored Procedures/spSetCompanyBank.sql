CREATE PROCEDURE [dbo].[spSetCompanyBank]
(
	@CompanyBankId		bigint = 0,
	
	@AccountName		nvarchar(1204) = NULL,
	@AccountNumber		nvarchar(64) = NULL,
	@BankName			nvarchar(128) = NULL,
	@IFSC				nvarchar(16) = NULL,
	@MICR				nvarchar(16) = NULL,
	@BranchName			nvarchar(64) = NULL,
	@BranchAddress		nvarchar(128) = NULL,
	@IsPrintable		bit = 0,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(32),

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
				INSERT INTO dbo.mCompanyBank
					(AccountName,
					AccountNumber,
					BankName,
					IFSC,
					MICR,
					BranchName,
					BranchAddress,
					IsPrintable,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@AccountName, 
					@AccountNumber, 
					@BankName, 
					@IFSC, 
					@MICR, 
					@BranchName, 
					@BranchAddress, 
					@IsPrintable, 
					1, 
					dbo.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM dbo.mCompanyBank WHERE CompanyBankId = @CompanyBankId)
				BEGIN
					UPDATE dbo.mCompanyBank
					SET AccountName = @AccountName, 
						AccountNumber = @AccountNumber, 
						BankName = @BankName, 
						IFSC = @IFSC, 
						MICR = @MICR, 
						BranchName = @BranchName, 
						BranchAddress = @BranchAddress, 
						IsActive = 1, 
						TransactionDate = dbo.ISTNow(), 
						RequestId = @RequestId
					WHERE CompanyBankId = @CompanyBankId ;
					SET @OutParam = @CompanyBankId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM dbo.mCompanyBank WHERE CompanyBankId = @CompanyBankId)
				BEGIN
					UPDATE dbo.mCompanyBank
					SET IsActive = 0,
						TransactionDate = dbo.ISTNow(), 
						RequestId = @RequestId
					WHERE CompanyBankId = @CompanyBankId ;
					SET @OutParam = @CompanyBankId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='PRINTABILITY')
			BEGIN
				IF EXISTS (SELECT * FROM dbo.mCompanyBank WHERE CompanyBankId = @CompanyBankId)
				BEGIN
					UPDATE dbo.mCompanyBank
					SET IsPrintable = 0;

					UPDATE dbo.mCompanyBank
					SET IsPrintable = @IsPrintable, 
						TransactionDate = dbo.ISTNow(), 
						RequestId = @RequestId
					WHERE CompanyBankId = @CompanyBankId ;
					SET @OutParam = @CompanyBankId;
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