
CREATE PROCEDURE [dbo].[spSetInvoice]
(
	@InvoiceTypeId		bigint = 0,
	@InvoiceDate		datetime = NULL,
	@InvoiceDueDate		datetime = NULL,
	@InvoiceNumber		nvarchar(16) = NULL,
	@CustomerId			bigint = 0,
	@SalesOrderId		bigint = 0,

    @VehicleNumber			nvarchar(16) = NULL,
    @HandOverPerson			nvarchar(64) = NULL,
	@HandOverPersonMobile	nvarchar(16) = null,

	@Remarks				nvarchar(512) = NULL,    

	@IsGstApplicable		bit = 0,
	@GSTNumber			nvarchar(16) = NULL,
	@GSTStateCode		nvarchar(2) = NULL,
	@GSTType			smallint = NULL,	
    @InvoicedAmount		decimal(18,5) = 0,
	@TaxAmount			decimal(18,5) = 0,
	@TotalAmount		decimal(18,5) = 0,
	
	@IsFullInvoiced		bit = 0,

	@InvoiceDetail [dbo].[InvoiceDetailType] READONLY, 
	@RequestId			nvarchar(64),
	@QueryType			varchar(16),
	@OutParam			nvarchar(512) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@InvoiceId bigint,		
		@NextInvoiceNo bigint;

	SELECT @T1 = 'InvoiceInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** Invoice Number Generate **/
				SELECT @NextInvoiceNo = (COUNT(*) + 1) FROM dbo.mInvoiceHeader;

				SET @InvoiceNumber = @InvoiceNumber + RIGHT('000000' + CAST(@NextInvoiceNo as varchar(16)), 6);

				/** INSERT INTO mInvoiceHeader **/
				INSERT INTO dbo.mInvoiceHeader
					(InvoiceTypeId,
					InvoiceDate,
					InvoiceDueDate,
					InvoiceNumber,
					IsFullInvoiced,
					SalesOrderId,
					CustomerId,
					IsGstApplicable,
					GSTNumber,
					GSTStateCode,
					GSTType,
					InvoicedAmount,
					TaxAmount,
					TotalAmount,
					VehicleNumber,
					HandOverPerson,
					HandOverPersonMobile,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@InvoiceTypeId, 
					@InvoiceDate, 
					@InvoiceDueDate, 
					@InvoiceNumber, 
					@IsFullInvoiced, 
					@SalesOrderId, 
					@CustomerId, 
					@IsGstApplicable, 
					@GSTNumber, 
					@GSTStateCode, 
					@GSTType, 
					@InvoicedAmount, 
					@TaxAmount, 
					@TotalAmount, 
					@VehicleNumber, 
					@HandOverPerson, 
					@HandOverPersonMobile, 
					@Remarks, 
					1, 
					dbo.ISTNow(), 
					@RequestId)

				SET @InvoiceId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO mInvoiceDetail **/
				INSERT INTO dbo.mInvoiceDetail
					(InvoiceId,
					InvoiceNumber,
					SalesOrderDetailId,
					ProductId,
					HSNSAC,
					Price,
					InvoicedQuantity,
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
					@InvoiceId as InvoiceId,
					@InvoiceNumber as InvoiceNumber,
					SalesOrderDetailId,
					ProductId,
					HSNSAC,
					Price,
					InvoicedQuantity,
					SubTotal,
					TaxPercentage,
					CGSTAmount,
					SGSTAmount,
					IGSTAmount,
					Total,
					Description,
					1 AS IsActive,
					dbo.ISTNow() as  TransactionDate,
					@RequestId as RequestId
				FROM @InvoiceDetail;

				/* Check Full Invoiced or not */
				UPDATE dbo.mInvoiceHeader
				SET IsFullInvoiced = dbo.IsFullInvoiced(@SalesOrderId)
				WHERE SalesOrderId = @SalesOrderId
					AND InvoiceId = @InvoiceId;
				
				SET @OutParam = '{ "InvoiceId" : ' +
							CAST(@InvoiceId as VARCHAR(64)) +
							', "InvoiceNumber" : "' +
							CAST(@InvoiceNumber AS VARCHAR(16)) +
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