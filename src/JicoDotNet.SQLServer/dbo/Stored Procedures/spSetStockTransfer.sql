
CREATE PROCEDURE [dbo].[spSetStockTransfer]
(
	@FromWareHouseId		bigint = 0,
	@ToWareHouseId			bigint = 0,
	@StockTransferNumber	nvarchar(16) = NULL,
	@StockTransferDate		datetime = NULL,
	@Remarks			nvarchar(512) = NULL,
	        
	@STDetail [dbo].[StockTransferDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@StockTransferId		bigint,
		@NextSTNo				bigint;

	SELECT @T1 = 'STKTRNSFR';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** STK Number Generate **/
				SELECT @NextSTNo = (COUNT(*) + 1) FROM dbo.tStockTransferHeader;

				SET @StockTransferNumber = @StockTransferNumber + RIGHT('000000' + CAST(@NextSTNo as varchar(16)), 6);				

				/** INSERT INTO tStockTransferHeader **/
				INSERT INTO dbo.tStockTransferHeader
					(StockTransferNumber,
					StockTransferDate,
					FromWareHouseId,
					ToWareHouseId,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@StockTransferNumber, 
					@StockTransferDate, 
					@FromWareHouseId, 
					@ToWareHouseId, 
					@Remarks, 
					1, 
					dbo.ISTNow(), 
					@RequestId)
				SET @StockTransferId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO tStockTransferDetail **/
				INSERT INTO dbo.tStockTransferDetail
					(StockTransferId,
					StockTransferNumber,
					ProductId,
					AvailableQuantity,
					TransferQuantity,
					StockDetailId,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@StockTransferId,
					@StockTransferNumber,
					ProductId,
					AvailableQuantity,
					TransferQuantity,
					StockDetailId,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					1,
					dbo.ISTNow(),
					@RequestId
				FROM @STDetail;

				/** UPDATE STOCK **/
				DECLARE @Counter INT , @MaxId INT;
				DECLARE @StockId bigint;

				SELECT @Counter = min(Id) , @MaxId = max(Id) 
				FROM @STDetail
				DECLARE @ProductId bigint, 
					@AvailableQuantity decimal(18, 5),
					@TransferQuantity decimal(18, 5), 
					@Description nvarchar(512),
					@StockDetailId bigint,
					@IsPerishable bit,
					@ExpiryDate datetime,
					@BatchNo nvarchar(256);

				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN					
					SELECT @ProductId = ProductId, 
						@AvailableQuantity = AvailableQuantity, 
						@TransferQuantity = TransferQuantity,
						@Description = Description,
						@StockDetailId = StockDetailId,
						@IsPerishable = IsPerishable,
						@ExpiryDate = ExpiryDate,
						@BatchNo = BatchNo
					FROM @STDetail WHERE Id = @Counter;

					-- Stock
					EXEC dbo.spSetStock						
						@ProductId = @ProductId,
						@FromWareHouseId = @FromWareHouseId,
						@WareHouseId = @ToWareHouseId,
						@Quantity = @TransferQuantity,
						@Remarks = @Description,
						@GRNOrShipmentId = @StockTransferId,
						@GRNOrShipmentDate = @StockTransferDate,
						@RequestId = @RequestId,
						@QueryType = 'TRNSFR',
						@OutParam = @StockId OUTPUT;

					-- Stock Detail
					EXEC [dbo].[spSetStockDetail]
						@StockDetailId = @StockDetailId,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @ToWareHouseId,
						@Stock = @TransferQuantity,
						@IsPerishable = @IsPerishable,
						@ExpiryDate = @ExpiryDate,
						@BatchNo = @BatchNo,
						@Remarks = @Description,
						@GRNDate = NULL,
						@RequestId = @RequestId,
						@QueryType = 'TRNSFR',
						@OutParam = @OutParam OUTPUT;

				   SET @Counter  = @Counter  + 1        
				END
				SET @OutParam = '{ "StockTransferId" : ' +
							CAST(@StockTransferId as VARCHAR) +
							', "StockTransferNumber" : "' +
							CAST(@StockTransferNumber AS VARCHAR) +
							'" }';
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