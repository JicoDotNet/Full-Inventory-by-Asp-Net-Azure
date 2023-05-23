
CREATE PROCEDURE [dbo].[spSetGRNDirect]
(
	@PurchaseTypeId		bigint = 0,	
	@VendorId			bigint = 0,
	@PurchaseOrderDate		datetime = NULL,
	@PurchaseOrderAmount		decimal(18,5) = 0.0,
	@PurchaseOrderTaxAmount		decimal(18,5) = 0.0,
	@PurchaseOrderTotalAmount	decimal(18,5) = 0.0,
	@Remarks					nvarchar(512) = NULL,
	@PurchaseOrderDetails		[dbo].[PurchaseOrderDetailType] READONLY,

	-- GRN
	@WareHouseId		bigint = 0,
	@GRNNumber			nvarchar(16) = NULL,
	@GRNDate			datetime = NULL,
	@IsDirect			bit = 0,
    @IsFullReceived		bit = 0,
    @VendorDONumber		nvarchar(64) = NULL,
    @VendorInvoiceNumber nvarchar(64) = NULL,
    @VendorInvoiceDate	datetime = NULL,       
	@GRNDetail [dbo].[GRNDetailType] READONLY, 

	@IsBillGenerated	bit = 0,
	@IsPaid				bit = 0,

	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@GRNId bigint,
		@NextGRNNo bigint;

	SELECT @T1 = 'GRNInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				DECLARE	@OutParamPO varchar(512),
						@PurchaseOrderId nvarchar(16), 
						@BranchId bigint;
				SELECT @BranchId = BranchId From dbo.mWarehouse WHERE WarehouseId = @WarehouseId;

				/* PO Entry */
				EXEC [dbo].[spSetPurchaseOrder]
					@PurchaseTypeId = @PurchaseTypeId,
					@BranchId = @BranchId,
					@VendorId = @VendorId,
					@PurchaseOrderDate = @PurchaseOrderDate,
					@PurchaseOrderNumber = NULL,
					@PurchaseOrderAmount = @PurchaseOrderAmount,
					@PurchaseOrderTaxAmount = @PurchaseOrderTaxAmount,
					@PurchaseOrderTotalAmount = @PurchaseOrderTotalAmount,
					@TandC = NULL,
					@Remarks = @Remarks,
					@PurchaseOrderDetails = @PurchaseOrderDetails,
					@RequestId = @RequestId,
					@QueryType = 'ENTRY',
					@OutParam = @OutParamPO OUTPUT,
					@IsGRNDirect = 1,
					@Id = @PurchaseOrderId OUTPUT
				
				/** GRN Number Generate **/
				SELECT @NextGRNNo = (COUNT(*) + 1) FROM dbo.mGRNHeader;

				SET @GRNNumber = @GRNNumber + RIGHT('000000' + CAST(@NextGRNNo as varchar(16)), 6);
				
				/** INSERT INTO mGRNHeader **/
				INSERT INTO dbo.mGRNHeader
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
				VALUES(
					@GRNDate, @GRNNumber, @IsDirect, @IsFullReceived, @PurchaseOrderId, NULL,
					@VendorDONumber, @VendorInvoiceNumber, @VendorInvoiceDate, @WareHouseId, @Remarks, 1, dbo.ISTNow(), @RequestId
				 );
				SET @GRNId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO mGRNDetail **/
				INSERT INTO dbo.mGRNDetail
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
					@GRNNumber AS GRNNumber,
					Id AS PurchaseOrderDetailId,
					ProductId,
					ReceivedQuantity,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					[Description],
					1,
					dbo.ISTNow(),
					@RequestId
				FROM @GRNDetail;
				
				/* Set PurchaseOrderDetailId into mGRNDetail
				 * from mPurchaseOrderDetail */
				WITH tempGRNDetail AS
				(
				   SELECT PurchaseOrderDetailId, GRNDetailId,
				   ROW_NUMBER() OVER(ORDER BY GRNDetailId) as rn 
				   FROM mGRNDetail 
				   WHERE GRNId = @GRNId
				),
				 tempPurchaseOrderDetail as 
				 ( 
					SELECT PurchaseOrderDetailId,
					ROW_NUMBER()  OVER(ORDER BY PurchaseOrderDetailId ) as rn 
					FROM mPurchaseOrderDetail
					WHERE PurchaseOrderId = @PurchaseOrderId
				 )
				SELECT tempPurchaseOrderDetail.PurchaseOrderDetailId, tempGRNDetail.GRNDetailId
				INTO #ttPurchaseGRN 
				FROM tempGRNDetail JOIN tempPurchaseOrderDetail 
				ON tempGRNDetail.rn = tempPurchaseOrderDetail.rn;
				--SELECT * FROM #ttPurchaseGRN

				UPDATE mGRNDetail
				SET PurchaseOrderDetailId = (SELECT tt.PurchaseOrderDetailId FROM #ttPurchaseGRN as tt 
									WHERE tt.GRNDetailId = mGRNDetail.GRNDetailId)
				WHERE GRNId = @GRNId;

				DROP TABLE #ttPurchaseGRN;
				/* End PurchaseOrderDetailId set */

				/** UPDATE STOCK **/
				DECLARE @Counter INT , @MaxId INT;
				DECLARE @StockId bigint;

				SELECT @Counter = min(Id) , @MaxId = max(Id) 
				FROM @GRNDetail
				DECLARE @ProductId bigint, @ReceivedQuantity decimal(18, 5), @Description nvarchar(256),
					@IsPerishable bit, @ExpiryDate datetime, @BatchNo nvarchar(256);

				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN
										
					SELECT @ProductId = ProductId, @ReceivedQuantity = ReceivedQuantity, @Description = Description,
						@IsPerishable = IsPerishable, @ExpiryDate = ExpiryDate, @BatchNo = BatchNo
					FROM @GRNDetail WHERE Id = @Counter;

					-- Stock
					EXEC dbo.spSetStock
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
					EXEC [dbo].[spSetStockDetail]
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