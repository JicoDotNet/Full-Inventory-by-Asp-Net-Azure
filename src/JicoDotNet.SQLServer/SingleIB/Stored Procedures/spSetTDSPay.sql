
CREATE PROCEDURE [SingleIB].[spSetTDSPay]
(
	@PaymentOutId		bigint = 0,
	@VendorId			bigint = 0,
	
	@TDSAmount			decimal(18, 5) = NULL,

	-- When This amout will pay to IncomeTax againest TAN
	@TDSPayId			bigint = 0,
	@PayDate			datetime = null,
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
				FROM SingleIB.mVendor 
				WHERE VendorId = @VendorId;
				IF(@TDSAmount IS NOT NULL AND @TDSAmount > 0)
				BEGIN
					INSERT INTO SingleIB.tTDSPay
						(PaymentOutId,
						VendorId,
						PANNumber,
						TDSAmount,
						IsPaid,
						PayDate,
						IsActive,
						TransactionDate,
						RequestId)
					VALUES
						(@PaymentOutId,
						@VendorId,
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
			ELSE IF(@QueryType ='PAY')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.tTDSPay WHERE TDSPayId = @TDSPayId)
				BEGIN
					UPDATE SingleIB.tTDSPay
					SET PayDate = @PayDate,
						IsPaid = 1,
						RequestId = @RequestId,
						TransactionDate = SingleIB.ISTNow()
					WHERE TDSPayId = @TDSPayId;
					SET @OutParam = @TDSPayId;
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