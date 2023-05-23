
CREATE PROCEDURE [dbo].[spSetShipmentDirect]
(
	@ComapnyIsGSTRegistered		bit = 0,

	-- SO
	@SalesTypeId		bigint = 0,	
	@CustomerId			bigint = 0,
	@SalesOrderDate		datetime = NULL,
	@SalesOrderAmount			decimal(18,5) = 0.0,
	@SalesOrderTaxAmount		decimal(18,5) = 0.0,
	@SalesOrderTotalAmount		decimal(18,5) = 0.0,
	@DeliveryDate				datetime = NULL,		-- extra
	@Remarks					nvarchar(512) = NULL,
	@SalesOrderDetails			[dbo].[SalesOrderDetailType] READONLY,

	-- Ship
	@WareHouseId		bigint = 0,
	@ShipmentTypeId		bigint = 0,		-- extra
	@ShipmentNumber		nvarchar(16) = NULL,
	@ShipmentDate		datetime = NULL,
	@IsDirect			bit = 0,
    @IsFullShipped		bit = 0,

    @VehicleNumber		nvarchar(16) = NULL,
    @HandOverPerson		nvarchar(64) = NULL,
    @HandOverPersonMobile	nvarchar(16) = NULL,   
    
	@ShipmentDetails [dbo].[ShipmentDetailType] READONLY, 

	@IsInvoiceGenerated		bit = 0,
	@IsReceived				bit = 0,

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT,

	/* Reqired For Retail Sales - value of inserted SalesOrderId */
	@Id						nvarchar(18) = NULL OUTPUT	
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@ShipmentId		bigint,
		@NextShipmentNo bigint;

	SELECT @T1 = 'ShipmentInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				DECLARE	@OutParamSO varchar(512),
						@SalesOrderId nvarchar(16), 
						@BranchId bigint;
				SELECT @BranchId = BranchId From dbo.mWarehouse WHERE WarehouseId = @WarehouseId;

				/*SO Entry */
				EXEC [dbo].[spSetSalesOrder]
					@ComapnyIsGSTRegistered = @ComapnyIsGSTRegistered,
					@SalesTypeId = @SalesTypeId,
					@BranchId = @BranchId,
					@CustomerId = @CustomerId,
					@SalesOrderDate = @SalesOrderDate,
					@SalesOrderNumber = NULL,
					@DeliveryDate = @DeliveryDate,
					@SalesOrderAmount = @SalesOrderAmount,
					@SalesOrderTaxAmount = @SalesOrderTaxAmount,
					@SalesOrderTotalAmount = @SalesOrderTotalAmount,
					@TandC = NULL,
					@Remarks = @Remarks,
					@SalesOrderDetails = @SalesOrderDetails,
					@RequestId = @RequestId,
					@QueryType = 'ENTRY',
					@OutParam = @OutParamSO OUTPUT,
					@IsShipmentDirect = 1,
					@Id = @SalesOrderId OUTPUT

				/* For Retail Sales */
				SET @Id = CAST(@SalesOrderId AS nvarchar(18));
				
				-- Service is not allowed to receive as Stock
				-- Only Goods Allowed for Stock
				-- Added on 01-09-2021
				IF((SELECT COUNT(*) FROM @ShipmentDetails) > 0)
				BEGIN
					/** Shipment Number Generate **/
					SELECT @NextShipmentNo = (COUNT(*) + 1) FROM dbo.mShipmentHeader;

					SET @ShipmentNumber = @ShipmentNumber + RIGHT('000000' + CAST(@NextShipmentNo as varchar(16)), 6);
				
					/** INSERT INTO mShipmentHeader **/
					INSERT INTO dbo.mShipmentHeader
						(ShipmentTypeId,
						ShipmentDate,
						ShipmentNumber,
						IsDirect,
						IsFullShipped,
						SalesOrderId,
						SalesOrderNumber,
						VehicleNumber,
						HandOverPerson,
						HandOverPersonMobile,
						WareHouseId,
						Remarks,
						IsActive,
						TransactionDate,
						RequestId)
					VALUES(@ShipmentTypeId, @ShipmentDate, @ShipmentNumber, @IsDirect, @IsFullShipped, @SalesOrderId, NULL,
						@VehicleNumber, @HandOverPerson, @HandOverPersonMobile, @WareHouseId, @Remarks, 1, dbo.ISTNow(), @RequestId);

					SET @ShipmentId = CAST(SCOPE_IDENTITY() AS bigint);

					/** INSERT INTO mShipmentDetail **/
					INSERT INTO dbo.mShipmentDetail
						(ShipmentId,
						ShipmentNumber,
						SalesOrderDetailId,
						ProductId,
						ShippedQuantity,
						StockDetailId,
						IsPerishable,
						ExpiryDate,
						BatchNo,
						Description,
						IsActive,
						TransactionDate,
						RequestId)
					SELECT
						@ShipmentId AS ShipmentId,
						@ShipmentNumber AS ShipmentNumber,
						Id AS SalesOrderDetailId,
						ProductId,
						ShippedQuantity,
						StockDetailId,
						IsPerishable,
						ExpiryDate,
						BatchNo,
						[Description],
						1 AS IsActive,
						dbo.ISTNow() AS TransactionDate,
						@RequestId AS RequestId 
					FROM @ShipmentDetails;

					/* Set SalesOrderDetailId into mShipmentDetail
					 * from mSalesOrderDetail */
					WITH tempShipmentDetail AS
					(
					   SELECT SalesOrderDetailId, ShipmentDetailId,
					   ROW_NUMBER() OVER(ORDER BY ShipmentDetailId) as rn 
					   FROM mShipmentDetail 
					   WHERE ShipmentId = @ShipmentId
					),
					 tempSalesOrderDetail as 
					 ( 
						SELECT SalesOrderDetailId,
						ROW_NUMBER()  OVER(ORDER BY SalesOrderDetailId ) as rn 
						FROM mSalesOrderDetail
						WHERE SalesOrderId = @SalesOrderId
					 )
					SELECT tempSalesOrderDetail.SalesOrderDetailId, tempShipmentDetail.ShipmentDetailId
					INTO #ttSalesShipment 
					FROM tempShipmentDetail JOIN tempSalesOrderDetail 
					ON tempShipmentDetail.rn = tempSalesOrderDetail.rn;
					--SELECT * FROM #ttSalesShipment

					UPDATE mShipmentDetail
					SET SalesOrderDetailId = (SELECT tt.SalesOrderDetailId FROM #ttSalesShipment as tt 
										WHERE tt.ShipmentDetailId = mShipmentDetail.ShipmentDetailId)
					WHERE ShipmentId = @ShipmentId;

					DROP TABLE #ttSalesShipment;
					/* End SalesOrderDetailId set */
				
					/** UPDATE STOCK **/
					DECLARE @Counter INT , @MaxId INT;
					DECLARE @StockId bigint;

					SELECT @Counter = min(Id) , @MaxId = max(Id) 
					FROM @ShipmentDetails;
					DECLARE @ProductId bigint, @StockDetailId bigint, 
						@ShippedQuantity decimal(18, 5), @Description nvarchar(256),
						@IsPerishable bit, @ExpiryDate datetime, @BatchNo nvarchar(256);

					-- Loop Start
					WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
					BEGIN
						SELECT @ProductId = ProductId, @StockDetailId = StockDetailId,
							@ShippedQuantity = ShippedQuantity, @Description = Description,
							@IsPerishable = IsPerishable, @ExpiryDate = ExpiryDate, @BatchNo = BatchNo
						FROM @ShipmentDetails WHERE Id = @Counter;

						-- Stock
						EXEC dbo.spSetStock
							@ProductId = @ProductId,
							@WareHouseId = @WareHouseId,
							@Quantity = @ShippedQuantity,
							@Remarks = @Description,
							@GRNOrShipmentId = @ShipmentId,
							@GRNOrShipmentDate = @ShipmentDate,
							@RequestId = @RequestId,
							@QueryType = 'SHIPSUB',
							@OutParam = @StockId OUTPUT;
					
						-- Stock Detail
						EXEC [dbo].[spSetStockDetail]
							@StockDetailId = @StockDetailId,
							@StockId = @StockId,
							@ProductId = @ProductId,
							@WareHouseId = @WareHouseId,
							@Stock = @ShippedQuantity,
							@IsPerishable = @IsPerishable,
							@ExpiryDate = @ExpiryDate,
							@BatchNo = @BatchNo,
							@Remarks = @Description,
							@GRNDate = @ShipmentDate,
							@RequestId = @RequestId,
							@QueryType = 'SUB',
							@OutParam = @OutParam OUTPUT;

					   SET @Counter  = @Counter  + 1;       
					END
				END

				SET @OutParam = '{ "ShipmentId" : ' +
							CAST(@ShipmentId as VARCHAR) +
							', "ShipmentNumber" : "' +
							CAST(@ShipmentNumber AS VARCHAR) +
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