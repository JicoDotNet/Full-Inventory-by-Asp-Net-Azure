


CREATE PROCEDURE [dbo].[spSetPaymentOut]
(
	@VendorId			bigint = 0,
	@VendorBankId		bigint = 0,

	@IsTDSApplicable	bit = 0,
	@TDSPercentage		decimal(18, 5) = NULL,
	@TDSAmount			decimal(18, 5) = NULL,
	@PayAmount			decimal(18, 5) = 0.0,

	@TotalAmount		decimal(18, 5) = 0.0,
	@PaymentDate		datetime = NULL,
	@PaymentMode		smallint = 0,
	@ReferenceNo		nvarchar(64) = NULL,
	@Remarks			nvarchar(512) = NULL,

	@ChequeNo			nvarchar(8) = NULL,
	@ChequeDate			datetime = NULL,
	@ChequeIFSC			nvarchar(16) = NULL,
    
	@PaymentOutDetail [dbo].[PaymentOutDetailType] READONLY, 

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
		@PaymentOutId bigint;

	SELECT @T1 = 'PaymentOutInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				IF(@IsTDSApplicable = 0)
				BEGIN
					SET @TDSPercentage = NULL;
					SET @TDSAmount = NULL;
					SET @PayAmount = @TotalAmount;
				END
				INSERT INTO dbo.mPaymentOutHeader
				   (VendorId,
				   VendorBankId,
				   IsTDSApplicable,
				   TDSPercentage,	
				   TDSAmount, 
				   PayAmount,
				   TotalAmount,
				   PaymentDate,
				   PaymentMode,
				   ReferenceNo,
				   ChequeNo,
				   ChequeDate,
				   ChequeIFSC,
				   Remarks,
				   IsActive,
				   TransactionDate,
				   RequestId)
				VALUES
					(@VendorId, 
					@VendorBankId,
					@IsTDSApplicable,
				    @TDSPercentage,	
				    @TDSAmount,		
				    @PayAmount,
					@TotalAmount, 
					@PaymentDate, 
					@PaymentMode,
					@ReferenceNo, 
					@ChequeNo, 
					@ChequeDate, 
					@ChequeIFSC, 
					@Remarks, 
					1, 
					dbo.ISTNow(), 
					@RequestId)
				SET @PaymentOutId = CAST(SCOPE_IDENTITY() AS bigint);

				INSERT INTO dbo.mPaymentOutDetail
					(PaymentOutId,
					BillId,
					BillNumber,
					Amount,
					IsFullPaid,
					PaymentDate,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@PaymentOutId AS PaymentOutId,
					BillId,
					BillNumber,
					Amount,
					IsFullPaid,
					PaymentDate,
					Description,
					1 AS IsActive,
					dbo.ISTNow() AS TransactionDate,
					@RequestId as RequestId
				FROM @PaymentOutDetail;

				IF(@IsTDSApplicable = 1)
				BEGIN
					EXEC [dbo].[spSetTDSPay]
						@PaymentOutId = @PaymentOutId,
						@VendorId = @VendorId,
						@TDSAmount = @TDSAmount,
						@RequestId = @RequestId,
						@QueryType = 'INSERT',
						@OutParam = @OutParam;
				END

				SET @OutParam = @PaymentOutId;
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