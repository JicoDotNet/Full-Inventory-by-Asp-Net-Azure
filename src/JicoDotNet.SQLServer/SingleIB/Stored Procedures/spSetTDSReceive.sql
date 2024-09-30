
CREATE PROCEDURE [SingleIB].[spSetTDSReceive]
(
	@PaymentInId		bigint = 0,
	@CustomerId			bigint = 0,	
	@TDSAmount			decimal(18, 5) = NULL,

	-- When This amout will Received at IncomeTax againest company's TAN
	@TDSReceiveId		bigint = 0,
	@ReceivedDate		datetime = null,
	-- End

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				DECLARE @PANNumber nvarchar(16);
				SELECT @PANNumber = PANNumber 
				FROM SingleIB.mCustomer 
				WHERE CustomerId = @CustomerId;
				IF(@TDSAmount IS NOT NULL AND @TDSAmount > 0)
				BEGIN
					INSERT INTO SingleIB.tTDSReceive
						(PaymentInId,
						CustomerId,
						PANNumber,
						TDSAmount,
						IsReceived,
						ReceivedDate,
						IsActive,
						TransactionDate,
						RequestId)
					VALUES
						(@PaymentInId,
						@CustomerId,
						@PANNumber, 
						@TDSAmount, 
						0, 
						NULL, 
						1, 
						SingleIB.ISTNow(), 
						@RequestId)
					SET @OutParam = SCOPE_IDENTITY();
				END
				SET @OutParam = '0';
			END
			ELSE IF(@QueryType ='RECEIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.tTDSReceive WHERE TDSReceiveId = @TDSReceiveId)
				BEGIN
					UPDATE SingleIB.tTDSReceive
					SET ReceivedDate = @ReceivedDate,
						IsReceived = 1,
						RequestId = @RequestId,
						TransactionDate = SingleIB.ISTNow()
					WHERE TDSReceiveId = @TDSReceiveId;
					SET @OutParam = @TDSReceiveId;
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END