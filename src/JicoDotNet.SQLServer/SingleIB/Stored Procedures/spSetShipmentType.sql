CREATE PROCEDURE [SingleIB].[spSetShipmentType]
(
	@ShipmentTypeId		bigint = 0,
	@ShipmentTypeName	nvarchar(128) = NULL,
	@Description		nvarchar(256) = NULL,
	
	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(10),
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
				INSERT INTO SingleIB.mShipmentType
					(ShipmentTypeName,
					[Description],
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ShipmentTypeName, 
					@Description,
					1,
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mShipmentType WHERE ShipmentTypeId = @ShipmentTypeId)
				BEGIN
					UPDATE SingleIB.mShipmentType
					SET ShipmentTypeName = @ShipmentTypeName, 
						[Description] = @Description,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE ShipmentTypeId = @ShipmentTypeId;
					SET @OutParam = @ShipmentTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mShipmentType WHERE ShipmentTypeId = @ShipmentTypeId)
				BEGIN
					UPDATE SingleIB.mShipmentType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE ShipmentTypeId = @ShipmentTypeId;
					SET @OutParam = @ShipmentTypeId;
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