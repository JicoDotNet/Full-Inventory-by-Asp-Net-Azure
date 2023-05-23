CREATE PROCEDURE [dbo].[spSetProductType]
(
	@ProductTypeId		bigint = 0,

	@ProductTypeName	nvarchar(128) = NULL,
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
				INSERT INTO dbo.mProductType
					(ProductTypeName,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductTypeName, 
					@Description, 
					1, 
					dbo.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM dbo.mProductType WHERE ProductTypeId = @ProductTypeId)
				BEGIN
					UPDATE dbo.mProductType
					SET ProductTypeName = @ProductTypeName,
						Description = @Description,
						TransactionDate = dbo.ISTNow(),
						RequestId = @RequestId
					WHERE ProductTypeId = @ProductTypeId;
					SET @OutParam = @ProductTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='DEACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM dbo.mProductType WHERE ProductTypeId = @ProductTypeId)
				BEGIN
					UPDATE dbo.mProductType
					SET IsActive = 0,
						TransactionDate = dbo.ISTNow(),
						RequestId = @RequestId
					WHERE ProductTypeId = @ProductTypeId;
					SET @OutParam = @ProductTypeId;
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