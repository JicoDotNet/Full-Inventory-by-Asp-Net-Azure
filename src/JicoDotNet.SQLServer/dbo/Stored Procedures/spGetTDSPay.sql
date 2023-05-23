CREATE PROCEDURE [dbo].[spGetTDSPay]
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
			IF (@QueryType ='UNPAID')
			BEGIN
				SELECT tp.*, 
					poh.TDSPercentage, poh.TDSAmount, poh.PayAmount, poh.TotalAmount, poh.PaymentDate,
					vdr.CompanyName, vdr.CompanyType, vdr.StateCode, 
					vdr.IsGSTRegistered, vdr.GSTStateCode, vdr.GSTNumber
				FROM tTDSPay as tp
					INNER JOIN dbo.mPaymentOutHeader as poh
				ON tp.PaymentOutId = poh.PaymentOutId
					INNER JOIN dbo.mVendor as vdr
				ON vdr.VendorId = tp.VendorId
				WHERE tp.IsPaid = 0
					AND poh.IsTDSApplicable = 1
				ORDER BY poh.PaymentDate DESC, tp.TDSPayId DESC;
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