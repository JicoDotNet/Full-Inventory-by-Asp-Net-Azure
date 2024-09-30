


CREATE PROCEDURE [SingleIB].[spGetPaymentOut]
(
	@VendorId	bigint = 0,

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
				SELECT poh.*, vod.*
				FROM SingleIB.mPaymentOutHeader as poh
					INNER JOIN SingleIB.mVendor as vod
				ON poh.VendorId = vod.VendorId
				ORDER BY poh.PaymentDate DESC, poh.PaymentOutId DESC;
			END
			ELSE IF (@QueryType = 'COMULTATIVE')
			BEGIN
				SELECT SUM(potdl.Amount) AS Amount, 
						potdl.BillId				 
				FROM mPaymentOutDetail as potdl
				WHERE potdl.PaymentOutId IN 
						(SELECT potdh.PaymentOutId
						FROM SingleIB.mPaymentOutHeader as potdh
						WHERE potdh.VendorId = @VendorId)
				GROUP BY potdl.BillId;
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