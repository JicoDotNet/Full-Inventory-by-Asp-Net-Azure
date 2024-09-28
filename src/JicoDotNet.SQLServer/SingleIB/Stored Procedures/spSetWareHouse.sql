CREATE PROCEDURE [SingleIB].[spSetWareHouse]
(
	@WareHouseId		bigint = 0,

	@BranchId			bigint = 0,
	@WareHouseName		nvarchar(64) = NULL,
	@IsRetailCounter	bit = 0,
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
				INSERT INTO SingleIB.mWareHouse
					(BranchId,
					WareHouseName,
					IsRetailCounter,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@BranchId, 
					@WareHouseName, 
					@IsRetailCounter,
					@Description, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mWareHouse WHERE WareHouseId = @WareHouseId)
				BEGIN
					UPDATE SingleIB.mWareHouse
					SET BranchId = @BranchId,
						WareHouseName = @WareHouseName, 
						IsRetailCounter = @IsRetailCounter,
						Description = @Description,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE WareHouseId = @WareHouseId;
					SET @OutParam = @WareHouseId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mWareHouse WHERE WareHouseId = @WareHouseId)
				BEGIN
					UPDATE SingleIB.mWareHouse
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE WareHouseId = @WareHouseId;
					SET @OutParam = @WareHouseId;
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