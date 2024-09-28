
CREATE PROCEDURE [SingleIB].[spSetCustomer]
(
	@CustomerId			bigint = 0,
	@CustomerTypeId		bigint = 0,
	
	@ContactPerson		nvarchar(64) = NULL,
	@Email				nvarchar(64) = NULL,
	@Mobile				nvarchar(16) = NULL,
	@IsRetailCustomer	bit = 0,

	@CompanyName		nvarchar(1204) = NULL,
	@CompanyType		nvarchar(64) = NULL,
	@StateCode			nvarchar(2) = null,
	@IsGSTRegistered	bit = 0,
	@GSTStateCode		nvarchar(2) = NULL,
	@GSTNumber			nvarchar(16) = NULL,
	@PANNumber			nvarchar(16) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),

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
				INSERT INTO SingleIB.mCustomer
					(CustomerTypeId,
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
					IsRetailCustomer,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@CustomerTypeId, 
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
					@IsRetailCustomer,
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mCustomer WHERE CustomerId = @CustomerId)
				BEGIN
					UPDATE SingleIB.mCustomer
					SET CustomerTypeId = @CustomerTypeId, 
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
					WHERE CustomerId = @CustomerId;
					SET @OutParam = @CustomerId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mCustomer WHERE CustomerId = @CustomerId)
				BEGIN
					UPDATE SingleIB.mCustomer
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE CustomerId = @CustomerId;
					SET @OutParam = @CustomerId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF (@QueryType ='RETAILINSERT')
			BEGIN
				INSERT INTO SingleIB.mCustomer
					(CustomerTypeId,
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
					IsRetailCustomer,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@CustomerTypeId, 
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
					@IsRetailCustomer,
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
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