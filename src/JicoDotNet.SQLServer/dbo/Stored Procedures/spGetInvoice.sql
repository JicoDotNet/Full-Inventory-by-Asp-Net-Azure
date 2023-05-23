


CREATE PROCEDURE [dbo].[spGetInvoice]
(
	@SalesOrderId		bigint = 0,
	@CustomerId			bigint = 0,
	@InvoiceId			bigint = 0,

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
				SELECT inv.*, cus.*,
					soh.SalesOrderNumber,
				CASE 
					WHEN soh.SalesOrderNumber IS NULL THEN (SELECT sph.ShipmentNumber FROM dbo.mShipmentHeader as sph 
							WHERE sph.SalesOrderId = inv.SalesOrderId
								AND sph.IsDirect = 1)
					ELSE NULL
				END AS ShipmentNumber,
				CAST(
					(CASE 
						WHEN EXISTS (SELECT * FROM [dbo].[mPaymentInDetail] AS pod WHERE  inv.InvoiceId = pod.InvoiceId
									AND pod.IsFullReceived = 1)
							THEN 1
						WHEN EXISTS (SELECT * FROM [dbo].[mPaymentInDetail] AS pod WHERE  inv.InvoiceId = pod.InvoiceId
								AND pod.IsFullReceived <> 1)
						THEN 0
						ELSE null
					END) AS BIT) AS PaymentStatus
				FROM dbo.mInvoiceHeader as inv
				INNER JOIN dbo.mCustomer as cus
					ON inv.CustomerId = cus.CustomerId
				INNER JOIN dbo.mSalesOrderHeader as soh
					ON inv.SalesOrderId = soh.SalesOrderId
				ORDER BY inv.InvoiceDate DESC, inv.InvoiceId DESC;
			END
			ELSE IF (@QueryType ='RETAIL')
			BEGIN
				SELECT inv.*, cus.*,
					soh.SalesOrderNumber,
				CASE 
					WHEN soh.SalesOrderNumber IS NULL THEN (SELECT sph.ShipmentNumber FROM dbo.mShipmentHeader as sph 
							WHERE sph.SalesOrderId = inv.SalesOrderId
								AND sph.IsDirect = 1)
					ELSE NULL
				END AS ShipmentNumber,
				CAST(
					(CASE 
						WHEN EXISTS (SELECT * FROM [dbo].[mPaymentInDetail] AS pod WHERE  inv.InvoiceId = pod.InvoiceId
									AND pod.IsFullReceived = 1)
							THEN 1
						WHEN EXISTS (SELECT * FROM [dbo].[mPaymentInDetail] AS pod WHERE  inv.InvoiceId = pod.InvoiceId
								AND pod.IsFullReceived <> 1)
						THEN 0
						ELSE null
					END) AS BIT) AS PaymentStatus
				FROM dbo.mInvoiceHeader as inv
				INNER JOIN dbo.mCustomer as cus
					ON inv.CustomerId = cus.CustomerId
				INNER JOIN dbo.mSalesOrderHeader as soh
					ON inv.SalesOrderId = soh.SalesOrderId
				WHERE cus.IsRetailCustomer = 1
				ORDER BY inv.InvoiceDate DESC, inv.InvoiceId DESC;
			END
			ELSE IF (@QueryType = 'COMULTATIVE')
			BEGIN
				SELECT
					0 AS InvoiceDetailId,
					0 AS InvoiceId,
					NULL AS InvoiceNumber,
					invd.SalesOrderDetailId,
					0 AS ProductId,
					SUM(invd.InvoicedQuantity) AS InvoicedQuantity,
					NULL AS Description,
					CAST(1 AS BIT) AS IsActive,
					NULL AS TransactionDate,
					NULL AS RequestId
				FROM dbo.mInvoiceDetail as invd
				WHERE invd.InvoiceId IN 
						(SELECT invh.InvoiceId
						FROM dbo.mInvoiceHeader as invh
						WHERE invh.SalesOrderId = @SalesOrderId)
				GROUP BY invd.SalesOrderDetailId;
			END
			ELSE IF (@QueryType = 'ENTRY')
			BEGIN
				-- SO
				SELECT soh.SalesOrderId, 
					soh.SalesOrderNumber
				FROM dbo.mSalesOrderHeader soh
				WHERE soh.SalesOrderNumber IS NOT NULL
					AND soh.IsActive = 1
					AND soh.SalesOrderId NOT IN (SELECT inv.SalesOrderId
						FROM dbo.mInvoiceHeader AS inv
						WHERE inv.IsFullInvoiced = 1)
				UNION
				-- Ship
				SELECT grnh.SalesOrderId, 
					grnh.ShipmentNumber AS SalesOrderNumber
				FROM dbo.mShipmentHeader grnh
				WHERE grnh.IsDirect = 1
					AND grnh.SalesOrderId NOT IN (SELECT inv.SalesOrderId
						FROM dbo.mInvoiceHeader AS inv
						WHERE inv.IsFullInvoiced = 1);
			END
			ELSE IF (@QueryType = 'PAYMENT')
			BEGIN
				SELECT invH.*
				FROM dbo.mInvoiceHeader as invH
				WHERE invH.CustomerId = @CustomerId
					AND invH.InvoiceId NOT IN (SELECT indtl.InvoiceId
						FROM dbo.mPaymentInDetail as indtl
						WHERE indtl.IsFullReceived = 1);
			END
			ELSE IF (@QueryType = 'DETAIL')
			BEGIN
				SELECT inv.*, --cus.*,
				poh.SalesOrderNumber,
				CASE 
					WHEN poh.SalesOrderNumber IS NULL THEN (SELECT sph.ShipmentNumber FROM dbo.mShipmentHeader as sph 
							WHERE sph.SalesOrderId = inv.SalesOrderId
								AND sph.IsDirect = 1)
					ELSE NULL
				END AS ShipmentNumber
				FROM dbo.mInvoiceHeader as inv
				--INNER JOIN dbo.mCustomer as cus
				--	ON inv.CustomerId = cus.CustomerId
				INNER JOIN dbo.mSalesOrderHeader as poh
					ON inv.SalesOrderId = poh.SalesOrderId
				WHERE inv.InvoiceId = @InvoiceId;

				SELECT ind.*
				FROM mInvoiceDetail as ind
				WHERE ind.InvoiceId = @InvoiceId;
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