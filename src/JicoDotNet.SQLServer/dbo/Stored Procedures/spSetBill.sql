
CREATE PROCEDURE [dbo].[spSetBill]
(
	@BillTypeId			bigint = 0,
	@BillDate			datetime = NULL,
	@BillDueDate		datetime = NULL,
	@BillNumber			nvarchar(16) = NULL,
	@VendorId			bigint = 0,
	@PurchaseOrderId	bigint = 0,
    @VendorDONumber		nvarchar(64) = NULL,
    @VendorInvoiceNumber nvarchar(64) = NULL,
	@VendorInvoiceDate	datetime = null,
	@Remarks			nvarchar(512) = NULL,    

	@IsGstApplicable		bit = 0,
	@GSTNumber			nvarchar(16) = NULL,
	@GSTStateCode		nvarchar(2) = NULL,
	@GSTType			smallint = NULL,	
    @BilledAmount		decimal(18,5) = 0,
	@TaxAmount			decimal(18,5) = 0,
	@TotalAmount		decimal(18,5) = 0,
	
	@IsFullBilled		bit = 0,

	@BillDetail [dbo].[BillDetailType] READONLY, 
	@RequestId			nvarchar(64),
	@QueryType			varchar(16),
	@OutParam			nvarchar(512) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@BillId bigint,		
		@NextBillNo bigint;

	SELECT @T1 = 'BillInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** Bill Number Generate **/
				SELECT @NextBillNo = (COUNT(*) + 1) FROM dbo.mBillHeader;

				SET @BillNumber = @BillNumber + RIGHT('000000' + CAST(@NextBillNo as varchar(16)), 6);

				/** INSERT INTO mBillHeader **/
				INSERT INTO dbo.mBillHeader
					(BillTypeId,
					BillDate,
					BillDueDate,
					BillNumber,
					IsFullBilled,
					PurchaseOrderId,
					VendorId,
					IsGstApplicable,
					GSTNumber,
					GSTStateCode,
					GSTType,
					BilledAmount,
					TaxAmount,
					TotalAmount,
					VendorDONumber,
					VendorInvoiceNumber,
					VendorInvoiceDate,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@BillTypeId, 
					@BillDate, 
					@BillDueDate, 
					@BillNumber, 
					@IsFullBilled, 
					@PurchaseOrderId,
					@VendorId, 
					@IsGstApplicable, 
					@GSTNumber, 
					@GSTStateCode, 
					@GSTType, 
					@BilledAmount, 
					@TaxAmount, 
					@TotalAmount,
					@VendorDONumber, 
					@VendorInvoiceNumber, 
					@VendorInvoiceDate, 
					@Remarks, 
					1, 
					dbo.ISTNow(), 
					@RequestId)

				SET @BillId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO mBillDetail **/
				INSERT INTO dbo.mBillDetail
					(BillId,
					BillNumber,
					PurchaseOrderDetailId,
					ProductId,
					HSNSAC,
					Price,
					BilledQuantity,
					SubTotal,
					TaxPercentage,
					CGSTAmount,
					SGSTAmount,
					IGSTAmount,
					Total,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@BillId AS BillId,
					@BillNumber AS BillNumber,
					PurchaseOrderDetailId,
					ProductId,
					HSNSAC,
					Price,
					BilledQuantity,
					SubTotal,
					TaxPercentage,
					CGSTAmount,
					SGSTAmount,
					IGSTAmount,
					Total,
					Description,
					1,
					dbo.ISTNow(),
					@RequestId AS RequestId
				FROM @BillDetail;

				/* Check Full Billed or not */
				UPDATE dbo.mBillHeader
				SET IsFullBilled = dbo.IsFullBilled(@PurchaseOrderId)
				WHERE PurchaseOrderId = @PurchaseOrderId
					AND BillId = @BillId;
				
				SET @OutParam = '{ "BillId" : ' +
							CAST(@BillId as VARCHAR(64)) +
							', "BillNumber" : "' +
							CAST(@BillNumber AS VARCHAR(16)) +
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