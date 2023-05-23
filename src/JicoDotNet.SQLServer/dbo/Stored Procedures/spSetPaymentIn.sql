


CREATE PROCEDURE [dbo].[spSetPaymentIn]
(
	@CustomerId			bigint = 0,
	@CompanyBankId		bigint = 0,

	@IsTDSApplicable	bit = 0,
	@TDSPercentage		decimal(18, 5) = NULL,
	@TDSAmount			decimal(18, 5) = NULL,
	@ReceiveAmount		decimal(18, 5) = 0.0,

	@TotalAmount		decimal(18, 5) = 0.0,
	@PaymentDate		datetime = NULL,
	@PaymentMode		smallint = 0,
	@ReferenceNo		nvarchar(64) = NULL,
	@Remarks			nvarchar(512) = NULL,

	@ChequeNo			nvarchar(8) = NULL,
	@ChequeDate			datetime = NULL,
	@ChequeIFSC			nvarchar(16) = NULL,
    
	@PaymentInDetail [dbo].[PaymentInDetailType] READONLY, 

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
		@PaymentInId bigint;

	SELECT @T1 = 'PaymentOutInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				IF(@IsTDSApplicable = 0)
				BEGIN
					SET @TDSPercentage = NULL;
					SET @TDSAmount = NULL;
					SET @ReceiveAmount = @TotalAmount;
				END
				INSERT INTO dbo.mPaymentInHeader
				   (CustomerId,
				   CompanyBankId,
				   IsTDSApplicable,
				   TDSPercentage,	
				   TDSAmount,		
				   ReceiveAmount,
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
					(@CustomerId, 
					@CompanyBankId,
					@IsTDSApplicable,
				    @TDSPercentage,	
				    @TDSAmount,		
				    @ReceiveAmount,
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
				SET @PaymentInId = CAST(SCOPE_IDENTITY() AS bigint);

				INSERT INTO dbo.mPaymentInDetail
					(PaymentInId,
					InvoiceId,
					InvoiceNumber,
					Amount,
					IsFullReceived,
					PaymentDate,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@PaymentInId AS PaymentInId,
					InvoiceId,
					InvoiceNumber,
					Amount,
					IsFullReceived,
					PaymentDate,
					Description,
					1 AS IsActive,
					dbo.ISTNow() AS TransactionDate,
					@RequestId as RequestId
				FROM @PaymentInDetail;
				IF(@IsTDSApplicable = 1)
				BEGIN
					EXEC [dbo].[spSetTDSReceive]
						@PaymentInId = @PaymentInId,
						@CustomerId = @CustomerId,
						@TDSAmount = @TDSAmount,
						@RequestId = @RequestId,
						@QueryType = 'INSERT',
						@OutParam = @OutParam;
				END

				SET @OutParam = @PaymentInId;				
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