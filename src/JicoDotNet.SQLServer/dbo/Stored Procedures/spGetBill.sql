


CREATE PROCEDURE [dbo].[spGetBill]
(
	@PurchaseOrderId	bigint = 0,
	@VendorId			bigint = 0,
	@BillId				bigint = 0,

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
				SELECT bil.*, vod.CompanyName,
				poh.PurchaseOrderNumber,
				CASE 
					WHEN poh.PurchaseOrderNumber IS NULL THEN (SELECT grh.GRNNumber FROM dbo.mGRNHeader as grh 
							WHERE grh.PurchaseOrderId = bil.PurchaseOrderId
								AND grh.IsDirect = 1)
					ELSE NULL
				END AS GRNNumber,
				CAST(
					(CASE 
						WHEN EXISTS (SELECT * FROM [dbo].[mPaymentOutDetail] AS pod WHERE  bil. BillId = pod.BillId
									AND pod.IsFullPaid = 1)
							THEN 1
						WHEN EXISTS (SELECT * FROM [dbo].[mPaymentOutDetail] AS pod WHERE  bil. BillId = pod.BillId
								AND pod.IsFullPaid <> 1)
						THEN 0
						ELSE null
					END) AS BIT) AS PaymentStatus
				FROM dbo.mBillHeader as bil
				INNER JOIN dbo.mVendor as vod
					ON bil.VendorId = vod.VendorId
				INNER JOIN dbo.mPurchaseOrderHeader as poh
					ON bil.PurchaseOrderId = poh.PurchaseOrderId
				ORDER BY bil.BillDate DESC, bil.BillId DESC;
			END
			ELSE IF (@QueryType ='SINGLE')
			BEGIN
				SELECT bil.*, vod.CompanyName,
					poh.PurchaseOrderNumber,
				CASE 
					WHEN poh.PurchaseOrderNumber IS NULL THEN (SELECT grh.GRNNumber FROM dbo.mGRNHeader as grh 
							WHERE grh.PurchaseOrderId = bil.PurchaseOrderId
								AND grh.IsDirect = 1)
					ELSE NULL
				END AS GRNNumber,
				CAST(
					(CASE 
						WHEN EXISTS (SELECT * FROM [dbo].[mPaymentOutDetail] AS pod WHERE  bil. BillId = pod.BillId
									AND pod.IsFullPaid = 1)
							THEN 1
						WHEN EXISTS (SELECT * FROM [dbo].[mPaymentOutDetail] AS pod WHERE  bil. BillId = pod.BillId
								AND pod.IsFullPaid <> 1)
						THEN 0
						ELSE null
					END) AS BIT) AS PaymentStatus
				FROM dbo.mBillHeader as bil
				INNER JOIN dbo.mVendor as vod
					ON bil.VendorId = vod.VendorId
				INNER JOIN dbo.mPurchaseOrderHeader as poh
					ON bil.PurchaseOrderId = poh.PurchaseOrderId
				WHERE bil.BillId = @BillId
				ORDER BY bil.BillDate DESC, bil.BillId DESC;
			END
			ELSE IF (@QueryType = 'COMMUTATIVE')
			BEGIN
				SELECT
					0 AS BillDetailId,
					0 AS BillId,
					NULL AS BillNumber,
					billd.PurchaseOrderDetailId,
					0 AS ProductId,
					SUM(billd.BilledQuantity) AS BilledQuantity,
					NULL AS Description,
					CAST(1 AS BIT) AS IsActive,
					NULL AS TransactionDate,
					NULL AS RequestId
				FROM dbo.mBillDetail as billd
				WHERE billd.BillId IN 
						(SELECT billh.BillId
						FROM dbo.mBillHeader as billh
						WHERE billh.PurchaseOrderId = @PurchaseOrderId)
				GROUP BY billd.PurchaseOrderDetailId;
			END
			ELSE IF (@QueryType = 'ENTRY')
			BEGIN
				-- PO
				SELECT poh.PurchaseOrderId, 
					poh.PurchaseOrderNumber
				FROM dbo.mPurchaseOrderHeader poh
				WHERE poh.PurchaseOrderNumber IS NOT NULL
					AND poh.IsActive = 1
					AND poh.PurchaseOrderId NOT IN (SELECT bilh.PurchaseOrderId
						FROM dbo.mBillHeader AS bilh
						WHERE bilh.IsFullBilled = 1)
				UNION
				-- GRN
				SELECT grnh.PurchaseOrderId, 
					grnh.GRNNumber AS PurchaseOrderNumber
				FROM dbo.mGRNHeader grnh
				WHERE grnh.IsDirect = 1
					AND grnh.PurchaseOrderId NOT IN (SELECT bilh.PurchaseOrderId
						FROM dbo.mBillHeader AS bilh
						WHERE bilh.IsFullBilled = 1);
			END
			ELSE IF (@QueryType = 'ENTRYSINGLE')
			BEGIN
				IF NOT EXISTS (
					SELECT bilh.PurchaseOrderId
						FROM dbo.mBillHeader AS bilh
						WHERE bilh.IsFullBilled = 1 
							AND bilh.PurchaseOrderId = @PurchaseOrderId)
				BEGIN
					SELECT @PurchaseOrderId as PurchaseOrderId
				END
			END
			ELSE IF (@QueryType = 'PAYMENT')
			BEGIN
				SELECT blh.*
				FROM dbo.mBillHeader as blh
				WHERE blh.VendorId = @VendorId
					AND blh.BillId NOT IN 
							(SELECT outdtl.BillId
							FROM dbo.mPaymentOutDetail as outdtl
							WHERE outdtl.IsFullPaid = 1);
			END
			ELSE IF (@QueryType = 'DETAIL')
			BEGIN
				SELECT bil.*, --vod.*,
				poh.PurchaseOrderNumber,
				CASE 
					WHEN poh.PurchaseOrderNumber IS NULL THEN (SELECT grh.GRNNumber FROM dbo.mGRNHeader as grh 
							WHERE grh.PurchaseOrderId = bil.PurchaseOrderId
								AND grh.IsDirect = 1)
					ELSE NULL
				END AS GRNNumber
				FROM dbo.mBillHeader as bil
				--INNER JOIN dbo.mVendor as vod
				--	ON bil.VendorId = vod.VendorId
				INNER JOIN dbo.mPurchaseOrderHeader as poh
					ON bil.PurchaseOrderId = poh.PurchaseOrderId
				WHERE bil.BillId = @BillId;

				SELECT bld.*
				FROM mBillDetail as bld
				WHERE bld.BillId = @BillId;
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