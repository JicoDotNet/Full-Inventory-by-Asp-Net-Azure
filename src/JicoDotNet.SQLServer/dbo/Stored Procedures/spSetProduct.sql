CREATE PROCEDURE [dbo].[spSetProduct]
(
	@ProductId			bigint = 0,

	@ProductTypeId		bigint = 0,
	@ProductInOut		smallint = 0,
	@Brand				nvarchar(256) = NULL,
	@ProductName		nvarchar(512) = NULL,
	@ProductCode		nvarchar(256) = NULL,
	@IsGoods			bit = 1,
	@SKU				nvarchar(128) = NULL,
	@PurchasePrice		decimal(18, 5) = 0.0,
	@SalePrice			decimal(18, 5) = 0.0,
	@HSNSAC				nvarchar(8) = NULL,
	@TaxPercentage		decimal(18, 5) = 0.0,
	@Description		nvarchar(1024) = NULL,
	@IsPerishableProduct	bit = 0,
	@HasExpirationDate		bit = 0,
	@HasBatchNo				bit = 0,
	@ProductImageUrl		nvarchar(256) = NULL,
	@UnitOfMeasureId		bigint = 0,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	DECLARE @UnitOfMeasureName nvarchar(8);

	-- /* Get UnitOfMeasureName From mUnitOfMeasure Table */
	SELECT @UnitOfMeasureName = UnitOfMeasureName 
	FROM dbo.mUnitOfMeasure 
	WHERE UnitOfMeasureId = @UnitOfMeasureId ;
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN				
				INSERT INTO dbo.mProduct
					(ProductTypeId,
					ProductInOut,
					Brand,
					ProductName,
					ProductCode,
					IsGoods,
					SKU,
					PurchasePrice,
					SalePrice,
					HSNSAC,
					TaxPercentage,
					Description,
					IsPerishableProduct,
					HasExpirationDate,
					HasBatchNo,
					ProductImageUrl,
					UnitOfMeasureId,
					UnitOfMeasureName,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductTypeId, 
					@ProductInOut, 
					@Brand,
					@ProductName, 
					@ProductCode, 
					@IsGoods,
					@SKU,
					@PurchasePrice,
					@SalePrice,
					@HSNSAC,
					@TaxPercentage,
					@Description, 
					@IsPerishableProduct, 
					@HasExpirationDate,
					@HasBatchNo,
					@ProductImageUrl,
					@UnitOfMeasureId, 
					@UnitOfMeasureName,
					1, 
					dbo.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM dbo.mProduct WHERE ProductId = @ProductId)
				BEGIN
					IF (@ProductImageUrl IS NULL OR @ProductImageUrl = '')
					BEGIN
						SELECT @ProductImageUrl = ProductImageUrl 
						FROM dbo.mProduct 
						WHERE ProductId = @ProductId;
					END
					UPDATE dbo.mProduct
					SET ProductTypeId = @ProductTypeId,
						ProductInOut = @ProductInOut, 
						Brand = @Brand,
						ProductName = @ProductName, 
						ProductCode = @ProductCode,
						IsGoods = @IsGoods,
						SKU = @SKU,
						PurchasePrice = @PurchasePrice,
						SalePrice = @SalePrice,
						HSNSAC = @HSNSAC,
						TaxPercentage = @TaxPercentage,
						Description = @Description, 
						-- IsPerishableProduct = @IsPerishableProduct,  -- Not Editable
						-- HasExpirationDate = @HasExpirationDate,  -- Not Editable
						-- HasBatchNo = @HasBatchNo,  -- Not Editable
						ProductImageUrl = @ProductImageUrl, 
						-- UnitOfMeasureId = @UnitOfMeasureId, -- Not Editable
						-- UnitOfMeasureName = @UnitOfMeasureName, -- Not Editable
						IsActive = 1,
						TransactionDate = dbo.ISTNow(),
						RequestId = @RequestId
					WHERE ProductId = @ProductId;
					SET @OutParam = @ProductId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='DEACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM dbo.mProduct WHERE ProductId = @ProductId)
				BEGIN
					UPDATE dbo.mProduct
					SET IsActive = 0,
						TransactionDate = dbo.ISTNow(),
						RequestId = @RequestId
					WHERE ProductId = @ProductId;
					SET @OutParam = @ProductId;
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