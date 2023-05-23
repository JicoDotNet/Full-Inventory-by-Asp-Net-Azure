CREATE PROCEDURE [dbo].[spSetSalesOrder]
(	
	@SalesOrderId		bigint = 0,

	@ComapnyIsGSTRegistered		bit = 0,

	@QuotationId		bigint = 0,
	@SalesTypeId		bigint = 0,	
	@BranchId			bigint = 0,
	@CustomerId			bigint = 0,

	@SalesOrderDate		datetime = NULL,
	@SalesOrderNumber	nvarchar(16) = NULL,
	@AmendmentNumber		nvarchar(4) = NULL,
	@AmendmentDate			datetime = NULL,

	@CustomerPONumber		nvarchar(128) = NULL,
	@CustomerPODate			datetime = NULL,
	@DeliveryDate			datetime = NULL,

	@SalesOrderAmount		decimal(18,5) = 0.0,
	@SalesOrderTaxAmount	decimal(18,5) = 0.0,
	@SalesOrderTotalAmount	decimal(18,5) = 0.0,


	@TandC		nvarchar(512) = NULL,
	@Remarks	nvarchar(512) = NULL,

	@SalesOrderDetails	[dbo].[SalesOrderDetailType] READONLY,
	
	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(512) OUTPUT,

	/* Reqired For Shipment Direct - value of inserted SalesOrderId */
	@IsShipmentDirect		bit = 0,
	@Id						nvarchar(18) = NULL OUTPUT	
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	DECLARE @IsGstAllowed bit;
	SELECT @T1 = 'SO';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ENTRY')
			BEGIN
				IF(@IsShipmentDirect = 0)
				BEGIN
					DECLARE @NextSoNo bigint;
					SELECT @NextSoNo = (COUNT(*) + 1) FROM dbo.mSalesOrderHeader
					WHERE SalesOrderNumber IS NOT NULL; -- SO no will be null if Goods ship without SO

					SET @SalesOrderNumber = @SalesOrderNumber + RIGHT('000000' + CAST(@NextSoNo as varchar(16)), 6);
				END
				
				/* Get GST Eligibility*/
				SET @IsGstAllowed = @ComapnyIsGSTRegistered;

				SET @AmendmentNumber = null;
				INSERT INTO dbo.mSalesOrderHeader
					(QuotationId,
					SalesTypeId,					
					BranchId,
					CustomerId,
					IsGstAllowed,
					SalesOrderDate,
					SalesOrderNumber,
					AmendmentNumber,
					AmendmentDate,
					CustomerPONumber, 
					CustomerPODate, 
					DeliveryDate,
					SalesOrderAmount,
					SalesOrderTaxAmount,
					SalesOrderTotalAmount,
					TandC,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@QuotationId,
					@SalesTypeId,
					@BranchId, 
					@CustomerId, 
					@IsGstAllowed,
					@SalesOrderDate, 
					@SalesOrderNumber, 
					@AmendmentNumber, 
					NULL, 
					@CustomerPONumber, 
					@CustomerPODate, 
					@DeliveryDate, 
					@SalesOrderAmount, 
					@SalesOrderTaxAmount, 
					@SalesOrderTotalAmount, 
					@TandC, 
					@Remarks, 
					1, 
					dbo.ISTNow(), 
					@RequestId);
				SET @SalesOrderId = CAST(SCOPE_IDENTITY() AS bigint);

				/* For Shipment Direct */
				SET @Id = CAST(@SalesOrderId AS nvarchar(18));

				INSERT INTO dbo.mSalesOrderDetail
					(SalesOrderId,
					SalesOrderNumber,
					AmendmentNumber,
					ProductId,
					HSNSAC,
					Amount,
					DiscountPercentage,
					DiscountAmount,
					Price,
					Quantity,
					SubTotal,
					TaxPercentage,
					TaxAmount,
					Total,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT @SalesOrderId,
					@SalesOrderNumber,
					@AmendmentNumber,
					ProductId,
					HSNSAC,
					Amount,
					DiscountPercentage,
					DiscountAmount,
					Price,
					Quantity,
					SubTotal,
					TaxPercentage,
					TaxAmount,
					Total,
					Description,
					1 AS IsActive,
					dbo.ISTNow() AS TransactionDate,
					@RequestId
				FROM @SalesOrderDetails 
				SET @OutParam = '{ "SalesOrderId" : ' +
							CAST(@SalesOrderId as VARCHAR) +
							', "SalesOrderNumber" : "' +
							CAST(@SalesOrderNumber AS VARCHAR) +
							'" }';
			END
			ELSE IF (@QueryType ='DELETE')
			BEGIN
				IF (NOT EXISTS(SELECT * FROM [dbo].[mShipmentHeader] WHERE SalesOrderId = @SalesOrderId AND IsActive = 1)
					AND NOT EXISTS(SELECT * FROM [dbo].[mInvoiceHeader] WHERE SalesOrderId = @SalesOrderId AND IsActive = 1))
				BEGIN
					UPDATE dbo.mSalesOrderHeader
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE SalesOrderId = @SalesOrderId;

					UPDATE dbo.mSalesOrderDetail
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE SalesOrderId = @SalesOrderId;
					SET @OutParam = @SalesOrderId;
				END
				ELSE
				BEGIN
					SET @OutParam = '-1';
				END
			END
			ELSE IF (@QueryType ='AMENDMENT')
			BEGIN
				-- AmendmentNumber Preparation
				DECLARE @NextAmNo bigint;
				SELECT @NextAmNo = (ISNULL(AmendmentNumber, 0) + 1) FROM dbo.mSalesOrderHeader
				WHERE SalesOrderId = @SalesOrderId;					
				SET @AmendmentNumber = @AmendmentNumber + CAST(@NextAmNo as varchar(16));
				
				INSERT tAmendmentTrack
					(AmendmentOf,
					AmendmentObjectId,
					OldAmendmentNumber,
					AmendmentNumber,
					HeaderJson,
					DetailJson,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					('SO',
					@SalesOrderId,
					(SELECT AmendmentNumber as OldAmendmentNumber FROM mSalesOrderHeader WHERE SalesOrderId = @SalesOrderId),
					@AmendmentNumber,
					(SELECT (SELECT * FROM mSalesOrderHeader WHERE SalesOrderId = @SalesOrderId FOR JSON PATH) as HeaderJson),
					(SELECT (SELECT * FROM mSalesOrderDetail WHERE SalesOrderId = @SalesOrderId FOR JSON PATH) as DetailJson),
					1,
					dbo.ISTNow(),
					@RequestId);
					
				-- Update Header
				UPDATE dbo.mSalesOrderHeader
					SET AmendmentNumber = @AmendmentNumber,
						AmendmentDate = @AmendmentDate,
						SalesOrderAmount = @SalesOrderAmount,
						SalesOrderTaxAmount = @SalesOrderTaxAmount,
						SalesOrderTotalAmount = @SalesOrderTotalAmount,
						TandC = @TandC,
						Remarks = @Remarks,
						IsActive = 1,
						TransactionDate = dbo.ISTNow(),
						RequestId = @RequestId
					WHERE SalesOrderId = @SalesOrderId;			

				-- Update Detail
				DECLARE @Counter int, @MaxId int;
				SELECT @Counter = min(Id) , @MaxId = max(Id) FROM @SalesOrderDetails;
				WHILE(@Counter <= @MaxId)
				BEGIN
					UPDATE mSalesOrderDetail
					SET AmendmentNumber = @AmendmentNumber,
						Amount = (SELECT TOP 1 Amount FROM @SalesOrderDetails WHERE Id = @Counter),
						DiscountPercentage = (SELECT TOP 1 DiscountPercentage FROM @SalesOrderDetails WHERE Id = @Counter),
						DiscountAmount = (SELECT TOP 1 DiscountAmount FROM @SalesOrderDetails WHERE Id = @Counter),
						Price = (SELECT TOP 1 Price FROM @SalesOrderDetails WHERE Id = @Counter),
						Quantity = (SELECT TOP 1 Quantity FROM @SalesOrderDetails WHERE Id = @Counter),
						SubTotal = (SELECT TOP 1 SubTotal FROM @SalesOrderDetails WHERE Id = @Counter),
						TaxPercentage = (SELECT TOP 1 TaxPercentage FROM @SalesOrderDetails WHERE Id = @Counter),
						TaxAmount = (SELECT TOP 1 TaxAmount FROM @SalesOrderDetails WHERE Id = @Counter),
						Total = (SELECT TOP 1 Total FROM @SalesOrderDetails WHERE Id = @Counter),
						IsActive = 1,
						TransactionDate = dbo.ISTNow(),
						RequestId = @RequestId
					WHERE SalesOrderId = @SalesOrderId
						AND SalesOrderDetailId = (SELECT TOP 1 SalesOrderDetailId FROM @SalesOrderDetails WHERE Id = @Counter);
									
					SET @Counter  = @Counter  + 1;
				END

				-- Check Full Shipped or not
				IF(dbo.IsFullShipped(@SalesOrderId) <> 1)
				BEGIN
					UPDATE dbo.mShipmentHeader
					SET IsFullShipped = 0
					WHERE SalesOrderId = @SalesOrderId;
				END
				-- Check Full Invoiced or not
				IF(dbo.IsFullInvoiced(@SalesOrderId) <> 1)
				BEGIN
					UPDATE dbo.mInvoiceHeader
					SET IsFullInvoiced = 0
					WHERE SalesOrderId = @SalesOrderId;
				END
				SET @OutParam = '{ "SalesOrderId" : ' + 
							CAST(@SalesOrderId as VARCHAR) +
							', "AmendmentNumber" : "' +
							CAST(@AmendmentNumber AS VARCHAR) +
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