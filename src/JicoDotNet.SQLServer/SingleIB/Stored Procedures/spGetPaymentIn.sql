


CREATE PROCEDURE [SingleIB].[spGetPaymentIn]
(
	@CustomerId	bigint = 0,

	@QueryType	varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='LIST')
			BEGIN
				SELECT pih.*, cus.*
				FROM SingleIB.mPaymentInHeader as pih
					INNER JOIN SingleIB.mCustomer as cus
				ON pih.CustomerId = cus.CustomerId
				ORDER BY pih.PaymentDate DESC, pih.PaymentInId DESC;
			END
			ELSE IF (@QueryType = 'COMULTATIVE')
			BEGIN
				SELECT SUM(pitdl.Amount) AS Amount, 
						pitdl.InvoiceId				 
				FROM mPaymentInDetail as pitdl
				WHERE pitdl.PaymentInId IN 
						(SELECT pitdh.PaymentInId
						FROM SingleIB.mPaymentInHeader as pitdh
						WHERE pitdh.CustomerId = @CustomerId)
				GROUP BY pitdl.InvoiceId;
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