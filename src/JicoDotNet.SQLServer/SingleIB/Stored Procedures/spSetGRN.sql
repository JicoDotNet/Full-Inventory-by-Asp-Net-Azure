


CREATE PROCEDURE [SingleIB].[spSetGRN]
(
	@PurchaseOrderId		bigint = 0,
	@WareHouseId		bigint = 0,
	@GRNNumber			nvarchar(16) = NULL,
	@GRNDate			datetime = NULL,
	@Remarks			nvarchar(512) = NULL,

	@IsDirect			bit = 0,
    @IsFullReceived		bit = 0,

    @VendorDONumber		nvarchar(64) = NULL,
    @VendorInvoiceNumber nvarchar(64) = NULL,
    @VendorInvoiceDate	datetime = NULL,
   
    
	@GRNDetail [SingleIB].[GRNDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@PurchaseOrderNumber nvarchar(16),
		@GRNId bigint,
		@NextGRNNo bigint;

	SELECT @T1 = 'GRNInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** GRN Number Generate **/
				SELECT @NextGRNNo = (COUNT(*) + 1) FROM SingleIB.mGRNHeader;

				SET @GRNNumber = @GRNNumber + RIGHT('000000' + CAST(@NextGRNNo as varchar(16)), 6);

				/** Get PO Number **/
				SELECT @PurchaseOrderNumber = PurchaseOrderNumber 
				FROM [SingleIB].[mPurchaseOrderHeader] 
				WHERE PurchaseOrderId = @PurchaseOrderId;

				/** UPDATE mGRNHeader **/
				--UPDATE SingleIB.mGRNHeader SET IsActive = 0 WHERE PurchaseOrderId = @PurchaseOrderId;

				/** INSERT INTO mGRNHeader **/
				INSERT INTO SingleIB.mGRNHeader
					(GRNDate,
					GRNNumber,
					IsDirect,
					IsFullReceived,
					PurchaseOrderId,
					PurchaseOrderNumber,
					VendorDONumber,
					VendorInvoiceNumber,
					VendorInvoiceDate,
					WareHouseId,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES(@GRNDate, @GRNNumber, @IsDirect, @IsFullReceived, @PurchaseOrderId, @PurchaseOrderNumber,
					@VendorDONumber, @VendorInvoiceNumber, @VendorInvoiceDate, @WareHouseId, @Remarks, 1, SingleIB.ISTNow(), @RequestId);
				SET @GRNId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO mGRNDetail **/
				INSERT INTO SingleIB.mGRNDetail
					(GRNId,
					GRNNumber,
					PurchaseOrderDetailId,
					ProductId,
					ReceivedQuantity,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@GRNId AS GRNId,
					@GRNNumber AS GRNNumber, PurchaseOrderDetailId, ProductId,
					ReceivedQuantity, IsPerishable, ExpiryDate,
					BatchNo, [Description], 1 as IsActive,
					SingleIB.ISTNow() as TransactionDate, @RequestId as RequestId
				FROM @GRNDetail

				/* Check Full Received or not */
				UPDATE SingleIB.mGRNHeader
				SET IsFullReceived = SingleIB.IsFullReceived(@PurchaseOrderId)
				WHERE PurchaseOrderId = @PurchaseOrderId
					AND GRNId = @GRNId;
				
				/** UPDATE STOCK **/
				DECLARE @Counter INT, @MaxId INT, @StockId bigint,
					@ProductId bigint, @ReceivedQuantity decimal(18, 5), @Description nvarchar(256),
					@IsPerishable bit, @ExpiryDate datetime, @BatchNo nvarchar(256);

				SELECT @Counter = min(Id) , @MaxId = max(Id) 
				FROM @GRNDetail;				

				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN
					SELECT @ProductId = ProductId, @ReceivedQuantity = ReceivedQuantity, @Description = Description,
						@IsPerishable = IsPerishable, @ExpiryDate = ExpiryDate, @BatchNo = BatchNo
					FROM @GRNDetail WHERE Id = @Counter;

					-- Stock
					EXEC [SingleIB].[spSetStock]
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Quantity = @ReceivedQuantity,
						@Remarks = @Description,
						@GRNOrShipmentId = @GRNId,
						@GRNOrShipmentDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = 'GRNADD',
						@OutParam = @StockId OUTPUT;
					
					-- Stock Detail
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = 0,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Stock = @ReceivedQuantity,
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
				SET @OutParam = '{ "GRNId" : ' +
							CAST(@GRNId as VARCHAR) +
							', "GRNNumber" : "' +
							CAST(@GRNNumber AS VARCHAR) +
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