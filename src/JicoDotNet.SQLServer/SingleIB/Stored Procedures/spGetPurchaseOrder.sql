


CREATE PROCEDURE [SingleIB].[spGetPurchaseOrder]
(
	@PurchaseOrderId int = 0,

	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ENTRY')
			BEGIN
				SELECT *
				FROM SingleIB.mPurchaseType
				WHERE IsActive = 1;				
				SELECT *
				FROM SingleIB.mBranch
				WHERE IsActive = 1;
				SELECT *
				FROM SingleIB.mVendor
				WHERE IsActive = 1;
				SELECT p.*
				FROM SingleIB.mProduct as p					
				WHERE p.IsActive = 1
					AND (p.ProductInOut = 0 OR p.ProductInOut = 1);
			END
			ELSE IF (@QueryType ='DETAIL')
			BEGIN
				-- Header
				SELECT 
					CASE 
						WHEN poh.PurchaseOrderNumber IS NULL THEN (SELECT grh.GRNNumber FROM SingleIB.mGRNHeader as grh 
								WHERE grh.PurchaseOrderId = @PurchaseOrderId
									AND grh.IsDirect = 1)
						ELSE NULL
					END AS GRNNumber,
					poh.*,
					-- GRN & Bill Status
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mGRNHeader] AS grn WHERE grn.PurchaseOrderId = @PurchaseOrderId
										AND grn.IsFullReceived = 1 AND grn.IsActive = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mGRNHeader] AS grn WHERE grn.PurchaseOrderId = @PurchaseOrderId
									AND grn.IsFullReceived <> 1 AND grn.IsActive = 1)
							THEN 0
							ELSE null
						END) AS BIT) AS GoodsReceivedStatus,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = @PurchaseOrderId
										AND bil.IsFullBilled = 1 AND bil.IsActive = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = @PurchaseOrderId
									AND bil.IsFullBilled <> 1 AND bil.IsActive = 1)
							THEN 0
							ELSE null
						END) AS BIT) AS BilledStatus,
					-- Status
					bra.BranchName, bra.BranchCode,
					vnd.CompanyName, vnd.CompanyType, vnd.ContactPerson, 
						vnd.GSTNumber, vnd.GSTStateCode, vnd.StateCode, vnd.IsGSTRegistered, vnd.PANNumber
				FROM SingleIB.mPurchaseOrderHeader AS poh
					INNER JOIN SingleIB.mBranch as bra
					ON poh.BranchId = bra.BranchId
					INNER JOIN SingleIB.mVendor as vnd
					ON poh.VendorId = vnd.VendorId
				WHERE poh.PurchaseOrderId = @PurchaseOrderId					
					AND poh.IsActive = 1;

				-- Detail
				SELECT pro.Brand, pro.ProductName, pro.ProductCode, pro.ProductId, 
					pro.ProductImageUrl, 
					pro.ProductInOut,
					pro.UnitOfMeasureName, 
					pod.*
				FROM SingleIB.mPurchaseOrderDetail as pod
					INNER JOIN SingleIB.mProduct as pro
					ON pod.ProductId = pro.ProductId
				WHERE pod.PurchaseOrderId = @PurchaseOrderId 
					AND pod.IsActive = 1;
			END
			ELSE IF(@QueryType ='LIST')
			BEGIN
				SELECT bra.BranchName, 
					bra.BranchCode, 
					vdr.CompanyName,
					vdr.IsGSTRegistered, 
					vdr.GSTStateCode,
					vdr.GSTNumber, 
					vdr.PANNumber, 
					poh.*,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mGRNHeader] AS grn WHERE grn.PurchaseOrderId = poh.PurchaseOrderId
										AND grn.IsFullReceived = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mGRNHeader] AS grn WHERE grn.PurchaseOrderId = poh.PurchaseOrderId
									AND grn.IsFullReceived <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS GoodsReceivedStatus,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = poh.PurchaseOrderId
										AND bil.IsFullBilled = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = poh.PurchaseOrderId
									AND bil.IsFullBilled <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS BilledStatus
				FROM SingleIB.mPurchaseOrderHeader AS poh
					INNER JOIN SingleIB.mBranch AS bra
				ON poh.BranchId = bra.BranchId 
					INNER JOIN SingleIB.mVendor AS vdr
				ON poh.VendorId = vdr.VendorId
				WHERE poh.PurchaseOrderNumber IS NOT NULL
					AND poh.IsActive = 1
				ORDER BY poh.PurchaseOrderDate DESC, poh.PurchaseOrderId DESC;
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