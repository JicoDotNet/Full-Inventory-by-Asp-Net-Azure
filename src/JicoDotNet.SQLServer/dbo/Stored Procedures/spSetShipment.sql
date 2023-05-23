
CREATE PROCEDURE [dbo].[spSetShipment]
(	
	@ShipmentTypeId		bigint = 0,
	@ShipmentDate		datetime = NULL,
	@ShipmentNumber		nvarchar(16) = NULL,

	@IsDirect			bit = 0,
    @IsFullShipped		bit = 0,

	@SalesOrderId		bigint = 0,
	@WareHouseId		bigint = 0,		
	@Remarks			nvarchar(512) = NULL,
	
    @VehicleNumber		nvarchar(16) = NULL,
    @HandOverPerson		nvarchar(64) = NULL,
    @HandOverPersonMobile	nvarchar(16) = NULL,   
    
	@ShipmentDetail [dbo].[ShipmentDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@SalesOrderNumber nvarchar(16),
		@ShipmentId bigint,
		@NextShipmentNo bigint;

	SELECT @T1 = 'ShipmentInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** Shipment Number Generate **/
				SELECT @NextShipmentNo = (COUNT(*) + 1) FROM dbo.mShipmentHeader;

				SET @ShipmentNumber = @ShipmentNumber + RIGHT('000000' + CAST(@NextShipmentNo as varchar(16)), 6);

				/** Get PO Number **/
				SELECT @SalesOrderNumber = SalesOrderNumber 
				FROM [dbo].[mSalesOrderHeader] 
				WHERE SalesOrderId = @SalesOrderId;

				/** UPDATE mShipmentHeader **/
				--UPDATE dbo.mShipmentHeader SET IsActive = 0 WHERE SalesOrderId = @SalesOrderId;

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
				VALUES(@ShipmentTypeId, @ShipmentDate, @ShipmentNumber, @IsDirect, @IsFullShipped, @SalesOrderId, @SalesOrderNumber,
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
					SalesOrderDetailId,
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
				FROM @ShipmentDetail;

				/* Check Full Shipped or not */
				UPDATE dbo.mShipmentHeader
				SET IsFullShipped = dbo.IsFullShipped(@SalesOrderId)
				WHERE SalesOrderId = @SalesOrderId
					AND ShipmentId = @ShipmentId;
				
				/** UPDATE STOCK **/
				DECLARE @Counter INT , @MaxId INT;
				DECLARE @StockId bigint;

				SELECT @Counter = min(Id) , @MaxId = max(Id) 
				FROM @ShipmentDetail;
				DECLARE @ProductId bigint, @StockDetailId bigint, 
					@ShippedQuantity decimal(18, 5), @Description nvarchar(256),
					@IsPerishable bit, @ExpiryDate datetime, @BatchNo nvarchar(256);

				-- Loop Start
				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN
					SELECT @ProductId = ProductId, @StockDetailId = StockDetailId,
					@ShippedQuantity = ShippedQuantity, @Description = Description,
						@IsPerishable = IsPerishable, @ExpiryDate = ExpiryDate, @BatchNo = BatchNo
					FROM @ShipmentDetail WHERE Id = @Counter;

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