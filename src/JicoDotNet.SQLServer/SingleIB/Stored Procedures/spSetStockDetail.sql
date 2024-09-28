

CREATE PROCEDURE [SingleIB].[spSetStockDetail]
(
	@StockDetailId		bigint = 0,		-- Pass value for decrease and trnsfr Stock

	@StockId			bigint = 0,
	@ProductId			bigint = 0,
	@WareHouseId		bigint = 0,
	@Stock				decimal(18,5) = 0.0,
	@IsPerishable		bit = 0,
	@ExpiryDate			datetime = null,
	@BatchNo			nvarchar(256) = null,
	@Remarks			nvarchar(512) = NULL,
	@GRNDate			datetime = null,

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),

	@OutParam			nvarchar(32) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'SET';
	DECLARE @CurrentStock decimal(18, 5) = 0.0;
	DECLARE @IsActive bit = 1;
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@Stock IS NOT NULL AND @Stock <> 0)
			BEGIN
				PRINT(1);
				IF (@QueryType ='ADD')
				BEGIN
					PRINT(2);
					-- (IF) Product Either Perishable or has Batch No or has Expiration Date
					IF EXISTS(SELECT * FROM SingleIB.mProduct 
									WHERE (ProductId = @ProductId )
										AND (IsPerishableProduct = 1
											OR HasBatchNo = 1
											OR HasExpirationDate = 1))
					BEGIN
						PRINT(3);
						/* -- This block is to find Same BatchNo and/or ExpiryDate of a Product is EXISTS or not
						-- Need to recheck 
						DECLARE @PrevExpiryDate datetime = null,
								@PrevBatchNo nvarchar(256) = null;
						SELECT @StockDetailId = StockDetailId, 
								@CurrentStock = Stock,
								@PrevExpiryDate = ExpiryDate,
								@PrevBatchNo = BatchNo
						FROM SingleIB.tStockDetail
						WHERE WareHouseId = @WareHouseId 
							AND ProductId = @ProductId;
						-- Add New Row for every Perishable Products
						IF((SELECT IsPerishableProduct FROM SingleIB.mProduct WHERE ProductId = @ProductId) = 1)
						BEGIN
							PRINT(3.1);
							SET @StockDetailId = 0.0;
						END
						-- Add New Row if ExpiryDate differs from Previous ExpiryDate
						IF(@PrevExpiryDate <> @ExpiryDate)
						BEGIN
							PRINT(3.2);
							SET @StockDetailId = 0.0;
						END
						-- Add New Row if ExpiryDate differs from Previous ExpiryDate
						IF(@PrevBatchNo <> @BatchNo)
						BEGIN
							PRINT(3.3);
							SET @StockDetailId = 0.0;
						END
						*/

						-- (IF) Stocked Product is Perishable or does not have same BatchNo and/or ExpiryDate - INSERT NEW ROW
						IF(@StockDetailId IS NULL OR @StockDetailId = 0)
						BEGIN
							PRINT(4);
							SET @CurrentStock = 0.0;
							SET @Stock = @Stock + @CurrentStock;
							INSERT INTO SingleIB.tStockDetail
								(StockId,
								ProductId,
								WareHouseId,
								Stock,
								IsPerishable,
								ExpiryDate,
								BatchNo,
								Remarks,
								GRNDate,
								IsActive,
								TransactionDate,
								RequestId)
							VALUES
								(@StockId,@ProductId, @WareHouseId, @Stock, @IsPerishable,
								@ExpiryDate, @BatchNo, @Remarks, @GRNDate, 1, SingleIB.ISTNow(), @RequestId);
							SET @OutParam = SCOPE_IDENTITY();
						END
						-- (ELSE) Stocked Product is Not Perishable or does have same BatchNo and/or ExpiryDate - UPDATE ROW
						-- According to this script's logic this else block will never execute. Need to do it later
						ELSE
						BEGIN
							PRINT(5);
							SET @Stock = @Stock + @CurrentStock;
							UPDATE SingleIB.tStockDetail
							SET Stock = @Stock,
								GRNDate = @GRNDate,
								StockId = @StockId,
								Remarks = @Remarks,
								IsActive = 1,
								TransactionDate = SingleIB.ISTNow(),
								RequestId = @RequestId
							WHERE StockDetailId = @StockDetailId
							SET @OutParam = @StockDetailId;
						END
					END
					-- (ELSE) Product Nither Perishable nor has Batch No nor has Expiration Date
					ELSE
					BEGIN
						PRINT(6);
						SELECT @StockDetailId = StockDetailId, 
								@CurrentStock = Stock
						FROM SingleIB.tStockDetail
						WHERE WareHouseId = @WareHouseId
							AND ProductId = @ProductId;

						-- (IF) Stocked Product is not available - INSERT NEW ROW
						PRINT(@StockDetailId);
						IF(@StockDetailId IS NULL OR @StockDetailId = 0)
						BEGIN
							PRINT(7);
							SET @CurrentStock = 0.0;
							SET @Stock = @Stock + @CurrentStock;
							INSERT INTO SingleIB.tStockDetail
								(StockId,
								ProductId,
								WareHouseId,
								Stock,
								IsPerishable,
								ExpiryDate,
								BatchNo,
								Remarks,
								GRNDate,
								IsActive,
								TransactionDate,
								RequestId)
							VALUES
								(@StockId,@ProductId, @WareHouseId, @Stock, @IsPerishable,
								@ExpiryDate, @BatchNo, @Remarks, @GRNDate, 1, SingleIB.ISTNow(), @RequestId);
							SET @OutParam = SCOPE_IDENTITY();
						END
						-- ELSE) Stocked Product is available - UPDATE ROW
						ELSE
						BEGIN
							PRINT(8);
							SET @Stock = @Stock + @CurrentStock;
							UPDATE SingleIB.tStockDetail
							SET Stock = @Stock,
								GRNDate = @GRNDate,
								StockId = @StockId,
								Remarks = @Remarks,
								IsActive = 1,
								TransactionDate = SingleIB.ISTNow(),
								RequestId = @RequestId
							WHERE StockDetailId = @StockDetailId
							SET @OutParam = @StockDetailId;
						END
					END
				END
				ELSE IF(@QueryType = 'SUB')
				BEGIN
					SELECT @CurrentStock = Stock
					FROM SingleIB.tStockDetail
					WHERE StockDetailId = @StockDetailId;
				
					SET @Stock = @CurrentStock - @Stock;
					IF(@Stock = 0.0)
					BEGIN
						SET @IsActive = 0;
					END
					IF(@Stock < 0.0)
					BEGIN
						-- Negative stock can't be inserted, 
						-- Throw an error to roll back the entire transction
						SELECT 1 / 0 AS Error; 
					END

					UPDATE SingleIB.tStockDetail
					SET Stock = @Stock,
						Remarks = @Remarks,
						IsActive = @IsActive,
						StockId = @StockId,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE StockDetailId = @StockDetailId
					SET @OutParam = @StockDetailId;
				END
				ELSE IF(@QueryType = 'TRNSFR')
				BEGIN
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = @StockDetailId,
						@StockId = @StockId,
						@Stock = @Stock,
						@Remarks = @Remarks,
						@RequestId = @RequestId,
						@QueryType = 'SUB',
						@OutParam = @OutParam OUTPUT;

					SELECT @GRNDate = GRNDate FROM SingleIB.tStockDetail WHERE StockDetailId = @StockDetailId;
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = 0,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Stock = @Stock,
						@IsPerishable = @IsPerishable,
						@ExpiryDate = @ExpiryDate,
						@BatchNo = @BatchNo,
						@Remarks = @Remarks,
						@GRNDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = 'ADD',
						@OutParam = @OutParam OUTPUT;						
				END
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