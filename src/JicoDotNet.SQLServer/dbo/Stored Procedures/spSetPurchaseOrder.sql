CREATE PROCEDURE [dbo].[spSetPurchaseOrder]
(
	@PurchaseOrderId	bigint = 0,

	@PurchaseTypeId		bigint = 0,	
	@BranchId			bigint = 0,
	@VendorId			bigint = 0,

	@PurchaseOrderDate		datetime = NULL,
	@PurchaseOrderNumber	nvarchar(16) = NULL,
	@AmendmentNumber		nvarchar(4) = NULL,
	@AmendmentDate			datetime = NULL,

	@DeliveryDate			datetime = NULL,

	@PurchaseOrderAmount		decimal(18,5) = 0.0,
	@PurchaseOrderTaxAmount		decimal(18,5) = 0.0,
	@PurchaseOrderTotalAmount	decimal(18,5) = 0.0,


	@TandC		nvarchar(512) = NULL,
	@Remarks	nvarchar(512) = NULL,

	@PurchaseOrderDetails		[dbo].[PurchaseOrderDetailType] READONLY,
	
	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),

	@OutParam			varchar(512) OUTPUT,

	/* Reqired For GRN Direct - value of inserted PurchaseOrderId */
	@IsGRNDirect		bit = 0,
	@Id					nvarchar(18) = NULL OUTPUT	
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	DECLARE @IsGstAllowed bit;
	SELECT @T1 = 'PO-ENTRY';	
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ENTRY')
			BEGIN
				/** PO Number Generate **/
				IF(@IsGRNDirect = 0)
				BEGIN
					DECLARE @NextPoNo bigint;
					SELECT @NextPoNo = (COUNT(*) + 1) FROM dbo.mPurchaseOrderHeader
					WHERE PurchaseOrderNumber IS NOT NULL; -- PO no will be null if Goods recev without PO
					 
					SET @PurchaseOrderNumber = @PurchaseOrderNumber + RIGHT('000000' + CAST(@NextPoNo as varchar(16)), 6);
				END
				/* Get GST Eligibility*/
				SELECT @IsGstAllowed = IsGSTRegistered 
				FROM dbo.mVendor
				WHERE VendorId = @VendorId;

				SET @AmendmentNumber = null;
				INSERT INTO dbo.mPurchaseOrderHeader
					(PurchaseTypeId,
					BranchId,
					VendorId,
					IsGstAllowed,
					PurchaseOrderDate,
					PurchaseOrderNumber,
					AmendmentNumber,
					AmendmentDate,
					DeliveryDate,
					PurchaseOrderAmount,
					PurchaseOrderTaxAmount,
					PurchaseOrderTotalAmount,
					TandC,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@PurchaseTypeId, 
					@BranchId, 
					@VendorId, 
					@IsGstAllowed,
					@PurchaseOrderDate, 
					@PurchaseOrderNumber, 
					@AmendmentNumber, 
					NULL, 
					@DeliveryDate, 
					@PurchaseOrderAmount, 
					@PurchaseOrderTaxAmount, 
					@PurchaseOrderTotalAmount, 
					@TandC, 
					@Remarks, 
					1, 
					dbo.ISTNow(), 
					@RequestId);
				SET @PurchaseOrderId = CAST(SCOPE_IDENTITY() AS bigint);

				/* For GRN Direct */
				SET @Id = CAST(@PurchaseOrderId AS nvarchar(18));

				INSERT INTO dbo.mPurchaseOrderDetail
					(PurchaseOrderId,
					PurchaseOrderNumber,
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
				SELECT @PurchaseOrderId,
					@PurchaseOrderNumber,
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
				FROM @PurchaseOrderDetails

				SET @OutParam = '{ "PurchaseOrderId" : ' +
							CAST(@PurchaseOrderId as VARCHAR) +
							', "PurchaseOrderNumber" : "' +
							CAST(@PurchaseOrderNumber AS VARCHAR) +
							'" }';
			END
			ELSE IF (@QueryType ='DELETE')
			BEGIN
				IF (NOT EXISTS(SELECT * FROM [dbo].[mGRNHeader] WHERE PurchaseOrderId = @PurchaseOrderId AND IsActive = 1)
					AND NOT EXISTS(SELECT * FROM [dbo].[mBillHeader] WHERE PurchaseOrderId = @PurchaseOrderId AND IsActive = 1))
				BEGIN
					UPDATE dbo.mPurchaseOrderHeader
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE PurchaseOrderId = @PurchaseOrderId;

					UPDATE dbo.mPurchaseOrderDetail
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE PurchaseOrderId = @PurchaseOrderId;
					SET @OutParam = @PurchaseOrderId;
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
				SELECT @NextAmNo = (ISNULL(AmendmentNumber, 0) + 1) FROM dbo.mPurchaseOrderHeader
				WHERE PurchaseOrderId = @PurchaseOrderId;					
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
					('PO',
					@PurchaseOrderId,
					(SELECT AmendmentNumber as OldAmendmentNumber FROM mPurchaseOrderHeader WHERE PurchaseOrderId = @PurchaseOrderId),
					@AmendmentNumber,
					(SELECT (SELECT * FROM mPurchaseOrderHeader WHERE PurchaseOrderId = @PurchaseOrderId FOR JSON PATH) as HeaderJson),
					(SELECT (SELECT * FROM mPurchaseOrderDetail WHERE PurchaseOrderId = @PurchaseOrderId FOR JSON PATH) as DetailJson),
					1,
					dbo.ISTNow(),
					@RequestId);

				-- Update Header
				UPDATE dbo.mPurchaseOrderHeader
					SET AmendmentNumber = @AmendmentNumber,
						AmendmentDate = @AmendmentDate,
						PurchaseOrderAmount = @PurchaseOrderAmount,
						PurchaseOrderTaxAmount = @PurchaseOrderTaxAmount,
						PurchaseOrderTotalAmount = @PurchaseOrderTotalAmount,
						TandC = @TandC,
						Remarks = @Remarks,
						IsActive = 1,
						TransactionDate = dbo.ISTNow(),
						RequestId = @RequestId
					WHERE PurchaseOrderId = @PurchaseOrderId;

				-- Update Detail
				DECLARE @Counter int, @MaxId int;
				SELECT @Counter = min(Id) , @MaxId = max(Id) FROM @PurchaseOrderDetails;
				WHILE(@Counter <= @MaxId)
				BEGIN
					UPDATE mPurchaseOrderDetail
					SET AmendmentNumber = @AmendmentNumber,
						Amount = (SELECT TOP 1 Amount FROM @PurchaseOrderDetails WHERE Id = @Counter),
						DiscountPercentage = (SELECT TOP 1 DiscountPercentage FROM @PurchaseOrderDetails WHERE Id = @Counter),
						DiscountAmount = (SELECT TOP 1 DiscountAmount FROM @PurchaseOrderDetails WHERE Id = @Counter),
						Price = (SELECT TOP 1 Price FROM @PurchaseOrderDetails WHERE Id = @Counter),
						Quantity = (SELECT TOP 1 Quantity FROM @PurchaseOrderDetails WHERE Id = @Counter),
						SubTotal = (SELECT TOP 1 SubTotal FROM @PurchaseOrderDetails WHERE Id = @Counter),
						TaxPercentage = (SELECT TOP 1 TaxPercentage FROM @PurchaseOrderDetails WHERE Id = @Counter),
						TaxAmount = (SELECT TOP 1 TaxAmount FROM @PurchaseOrderDetails WHERE Id = @Counter),
						Total = (SELECT TOP 1 Total FROM @PurchaseOrderDetails WHERE Id = @Counter),
						IsActive = 1,
						TransactionDate = dbo.ISTNow(),
						RequestId = @RequestId
					WHERE PurchaseOrderId = @PurchaseOrderId
						AND PurchaseOrderDetailId = (SELECT TOP 1 PurchaseOrderDetailId FROM @PurchaseOrderDetails WHERE Id = @Counter);
									
					SET @Counter  = @Counter  + 1;
				END
				-- Check Full Received or not
				IF(dbo.IsFullReceived(@PurchaseOrderId) <> 1)
				BEGIN
					UPDATE dbo.mGRNHeader
					SET IsFullReceived = 0
					WHERE PurchaseOrderId = @PurchaseOrderId;
				END
				-- Check Full Billed or not
				IF(dbo.IsFullBilled(@PurchaseOrderId) <> 1)
				BEGIN
					UPDATE dbo.mBillHeader
					SET IsFullBilled = 0
					WHERE PurchaseOrderId = @PurchaseOrderId;
				END
				--Return
				SET @OutParam = '{ "PurchaseOrderId" : ' + 
							CAST(@PurchaseOrderId as VARCHAR) +
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