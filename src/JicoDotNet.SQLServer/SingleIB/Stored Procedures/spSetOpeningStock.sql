


CREATE PROCEDURE [SingleIB].[spSetOpeningStock]
(
	@OpeningStockDetail [SingleIB].[OpeningStockDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(16),
	@OutParam			nvarchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30);

	SELECT @T1 = 'GRNInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN				
				/** UPDATE STOCK **/
				DECLARE @Counter INT , @MaxId INT;
				DECLARE @StockId bigint;

				SELECT @Counter = min(Id) , @MaxId = max(Id) 
				FROM @OpeningStockDetail
				DECLARE @ProductId bigint, 
					@WareHouseId bigint,
					@Quantity decimal(18, 5), 
					@GRNDate datetime,
					@Description nvarchar(256),
					@IsPerishable bit, 
					@ExpiryDate datetime, 
					@BatchNo nvarchar(256);

				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN
					
					SELECT 
						@WareHouseId = WareHouseId,
						@ProductId = ProductId, 
						@Quantity = Quantity, 
						@GRNDate = GRNDate,
						@IsPerishable = IsPerishable, 
						@BatchNo = BatchNo,
						@ExpiryDate = ExpiryDate,
						@Description = Description						
					FROM @OpeningStockDetail WHERE Id = @Counter;

					-- Stock
					EXEC SingleIB.spSetStock
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Quantity = @Quantity,
						@Remarks = @Description,
						@GRNOrShipmentDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = 'OPENING',
						@OutParam = @StockId OUTPUT;
					
					-- Stock Detail
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = 0,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Stock = @Quantity,
						@IsPerishable = @IsPerishable,
						@ExpiryDate = @ExpiryDate,
						@BatchNo = @BatchNo,
						@Remarks = @Description,
						@GRNDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = 'ADD',
						@OutParam = @OutParam OUTPUT;

				   SET @Counter  = @Counter  + 1;       
				END
				SET @OutParam = @MaxId;
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