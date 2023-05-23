
CREATE PROCEDURE [dbo].[spSetStock]
(
	@ProductId			bigint = 0,
	@WareHouseId		bigint = 0,
	@FromWareHouseId	bigint = 0,
	@Quantity			decimal(18, 5) = 0.0,
	@Remarks			nvarchar(512) = NULL,
	@GRNOrShipmentDate	datetime = null,
	@GRNOrShipmentId	bigint = 0,

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),

	@OutParam			nvarchar(32) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	DECLARE @CurrentStock decimal(18, 5) = 0.0;
	SELECT @T1 = 'SET';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='GRNADD')
			BEGIN
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM dbo.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock + @Quantity;

				-- Update Old Row
				UPDATE  dbo.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO dbo.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					1,
					1, 
					dbo.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='SHIPSUB')
			BEGIN
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM dbo.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock - @Quantity;

				-- Update Old Row
				UPDATE  dbo.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO dbo.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					-1,
					1, 
					dbo.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='SALESRTN')
			BEGIN
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM dbo.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock + @Quantity;

				-- Update Old Row
				UPDATE  dbo.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO dbo.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					2,
					1, 
					dbo.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='PURCHRTN')
			BEGIN
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM dbo.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock - @Quantity;

				-- Update Old Row
				UPDATE  dbo.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO dbo.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					-2,
					1, 
					dbo.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF (@QueryType ='TRNSFR')
			BEGIN
				-- Calculate Stock of FromWareHouse
				SELECT @CurrentStock = CurrentStock
				FROM dbo.tStock as stk
				WHERE stk.WareHouseId = @FromWareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock - @Quantity;

				-- Update Old Row of FromWareHouse
				UPDATE  dbo.tStock SET IsActive = 0
				WHERE WareHouseId = @FromWareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row of FromWareHouse
				INSERT INTO dbo.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@FromWareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					0,
					1, 
					dbo.ISTNow(), 
					@RequestId)

				-- Calculate Stock of FromWareHouse
				SET @CurrentStock = 0.0;
				SELECT @CurrentStock = CurrentStock
				FROM dbo.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock + @Quantity;

				-- Update Old Row of FromWareHouse
				UPDATE  dbo.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row of FromWareHouse
				INSERT INTO dbo.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					0,
					1, 
					dbo.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF (@QueryType ='OPENING')
			BEGIN
				-- SAME WITH GRNADD, HERE GRNOrShipmentId = NULL & StockType = 10 (OPENING)
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM dbo.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock + @Quantity;

				-- Update Old Row
				UPDATE  dbo.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO dbo.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					NULL,
					10,
					1, 
					dbo.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF (@QueryType ='ADJUSTADD')
			BEGIN
				-- SAME WITH GRNADD, HERE GRNOrShipmentId = NULL & StockType = 20 (AdjustIncrease)
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM dbo.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock + @Quantity;

				-- Update Old Row
				UPDATE  dbo.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO dbo.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					20,
					1, 
					dbo.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF (@QueryType ='ADJUSTSUB')
			BEGIN
				-- SAME WITH SHIPSUB, HERE GRNOrShipmentId = NULL & StockType = -20 (AdjustDecrease)
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM dbo.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock - @Quantity;

				-- Update Old Row
				UPDATE  dbo.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO dbo.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					-20,
					1, 
					dbo.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
		COMMIT TRANSACTION @T1;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END