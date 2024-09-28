CREATE PROCEDURE [SingleIB].[spSetUnitOfMeasure]
(
	@UnitOfMeasureId	int = 0,

	@UnitOfMeasureName	nvarchar(8) = NULL,
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
				INSERT INTO SingleIB.mUnitOfMeasure
					(UnitOfMeasureName,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@UnitOfMeasureName, 
					@Description, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mUnitOfMeasure WHERE UnitOfMeasureId = @UnitOfMeasureId)
				BEGIN
					UPDATE SingleIB.mUnitOfMeasure
					SET UnitOfMeasureName = @UnitOfMeasureName,
						Description = @Description,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE UnitOfMeasureId = @UnitOfMeasureId;

					-- /* Update UnitOfMeasureName of Product Table */
					UPDATE SingleIB.mProduct 
					SET UnitOfMeasureName = @UnitOfMeasureName
					WHERE UnitOfMeasureId = @UnitOfMeasureId;
					 
					SET @OutParam = @UnitOfMeasureId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='DEACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mUnitOfMeasure WHERE UnitOfMeasureId = @UnitOfMeasureId)
				BEGIN
					UPDATE SingleIB.mUnitOfMeasure
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE UnitOfMeasureId = @UnitOfMeasureId;
					SET @OutParam = @UnitOfMeasureId;
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