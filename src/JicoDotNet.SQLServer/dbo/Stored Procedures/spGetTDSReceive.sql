CREATE PROCEDURE [dbo].[spGetTDSReceive]
(
	@QueryType	varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='UNRECEIVED')
			BEGIN
				SELECT tr.*, 
					pih.TDSPercentage, pih.TDSAmount, pih.ReceiveAmount, pih.TotalAmount, pih.PaymentDate,
					cus.CompanyName, cus.CompanyType, cus.StateCode, 
					cus.IsGSTRegistered, cus.GSTStateCode, cus.GSTNumber
				FROM tTDSReceive as tr
					INNER JOIN dbo.mPaymentInHeader as pih
				ON tr.PaymentInId = pih.PaymentInId
					INNER JOIN dbo.mCustomer as cus
				ON cus.CustomerId = tr.CustomerId
				WHERE tr.IsReceived = 0
					AND pih.IsTDSApplicable = 1
				ORDER BY pih.PaymentDate DESC, tr.TDSReceiveId DESC;
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