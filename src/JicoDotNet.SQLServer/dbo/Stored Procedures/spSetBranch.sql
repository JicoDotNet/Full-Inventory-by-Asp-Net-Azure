CREATE PROCEDURE [dbo].[spSetBranch]
(
	@BranchId			int = 0,

	@BranchName			nvarchar(64) = NULL,
	@BranchCode			nvarchar(64) = NULL,
	@Address			nvarchar(128) = NULL,
	@City				nvarchar(32) = NULL,
	@State				nvarchar(2) = NULL,
	@PostalCode			nvarchar(8) = NULL,
	@ContactPerson		nvarchar(64) = NULL,
	@Email				nvarchar(64) = NULL,
	@Phone				nvarchar(16) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'SET';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN				
				INSERT INTO dbo.mBranch
					(BranchName,
					BranchCode,
					Address,
					City,
					State,
					PostalCode,
					ContactPerson,
					Email,
					Phone,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@BranchName, 
					@BranchCode, 
					@Address, 
					@City, 
					@State, 
					@PostalCode, 
					@ContactPerson, 
					@Email, 
					@Phone, 
					@Description, 
					1,
					dbo.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM dbo.mBranch WHERE BranchId = @BranchId)
				BEGIN
					UPDATE dbo.mBranch
					SET BranchName = @BranchName, 
						BranchCode = @BranchCode, 
						Address = @Address, 
						City = @City, 
						State = @State, 
						PostalCode = @PostalCode, 
						ContactPerson = @ContactPerson, 
						Email = @Email, 
						Phone = @Phone, 
						Description = @Description,
						TransactionDate = dbo.ISTNow(), 
						RequestId = @RequestId
					WHERE BranchId = @BranchId;
					SET @OutParam = @BranchId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM dbo.mBranch WHERE BranchId = @BranchId)
				BEGIN
					UPDATE dbo.mBranch
					SET IsActive = 0,
						TransactionDate = dbo.ISTNow(),
						RequestId = @RequestId
					WHERE BranchId = @BranchId;
					SET @OutParam = @BranchId;
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