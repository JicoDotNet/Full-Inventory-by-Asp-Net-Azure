
CREATE PROCEDURE [dbo].[spGetSalesOrder]
(
	@SalesOrderId bigint = 0,

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
				SELECT bra.BranchName, 
					bra.BranchCode, 
					cmr.CompanyName,
					cmr.IsGSTRegistered, 
					cmr.GSTStateCode,
					cmr.GSTNumber, 
					cmr.PANNumber, 
					soh.*,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [dbo].[mShipmentHeader] AS sph 
										WHERE sph.SalesOrderId = soh.SalesOrderId AND sph.IsFullShipped = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [dbo].[mShipmentHeader] AS grn 
										WHERE grn.SalesOrderId = soh.SalesOrderId AND grn.IsFullShipped <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS ShippedStatus,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [dbo].[mInvoiceHeader] AS inv 
										WHERE inv. SalesOrderId = soh.SalesOrderId AND inv.IsFullInvoiced = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [dbo].[mInvoiceHeader] AS inv 
									WHERE inv.SalesOrderId = soh.SalesOrderId AND inv.IsFullInvoiced <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS InvoicedStatus
				FROM dbo.mSalesOrderHeader AS soh
					INNER JOIN dbo.mBranch AS bra
				ON soh.BranchId = bra.BranchId 
					INNER JOIN dbo.mCustomer AS cmr
				ON soh.CustomerId = cmr.CustomerId
				WHERE soh.SalesOrderNumber IS NOT NULL
					AND soh.IsActive = 1
				ORDER BY soh.SalesOrderDate DESC, soh.SalesOrderId DESC;
			END
			IF (@QueryType ='ENTRY')
			BEGIN
				SELECT *
				FROM dbo.mSalesType
				WHERE IsActive = 1;				
				SELECT *
				FROM dbo.mBranch
				WHERE IsActive = 1;
				SELECT *
				FROM dbo.mCustomer
				WHERE IsRetailCustomer = 0
					AND IsActive = 1;
				SELECT p.*
				FROM dbo.mProduct as p					
				WHERE p.IsActive = 1
					AND (p.ProductInOut = 0 OR p.ProductInOut = 1);
			END
			ELSE IF (@QueryType ='DETAIL')
			BEGIN
				-- Table 0
				SELECT 
					CASE 
						WHEN soh.SalesOrderNumber IS NULL THEN (SELECT grh.ShipmentNumber FROM dbo.mShipmentHeader as grh 
								WHERE grh.SalesOrderId = @SalesOrderId
									AND grh.IsDirect = 1)
						ELSE NULL
					END AS ShipmentNumber,
					soh.*, 
						(SELECT TOP 1 qh.QuotationNumber FROM dbo.mQuotationHeader as qh 
							WHERE qh.QuotationId = soh.QuotationId
								AND qh.IsActive = 1) 
					as QuotationNumber,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [dbo].[mShipmentHeader] AS sph WHERE sph.SalesOrderId = @SalesOrderId
										AND sph.SalesOrderId = soh.SalesOrderId AND sph.IsFullShipped = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [dbo].[mShipmentHeader] AS sph WHERE sph.SalesOrderId = @SalesOrderId
										AND sph.SalesOrderId = soh.SalesOrderId AND sph.IsFullShipped <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS ShippedStatus,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [dbo].[mInvoiceHeader] AS inv WHERE inv.SalesOrderId = @SalesOrderId
										AND inv. SalesOrderId = soh.SalesOrderId AND inv.IsFullInvoiced = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [dbo].[mInvoiceHeader] AS inv WHERE inv.SalesOrderId = @SalesOrderId
										AND inv.SalesOrderId = soh.SalesOrderId AND inv.IsFullInvoiced <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS InvoicedStatus,
					bra.BranchName, bra.BranchCode,
					cus.CompanyName, cus.CompanyType, cus.ContactPerson, cus.StateCode,
						cus.GSTNumber, cus.GSTStateCode, cus.IsGSTRegistered, cus.PANNumber
				FROM dbo.mSalesOrderHeader AS soh
				INNER JOIN dbo.mBranch as bra
					ON soh.BranchId = bra.BranchId
				INNER JOIN dbo.mCustomer as cus
					ON soh.CustomerId = cus.CustomerId
				WHERE soh.SalesOrderId = @SalesOrderId
					AND soh.IsActive = 1; 

				-- Table 1
				SELECT pro.Brand, pro.ProductName, pro.ProductCode, 
					pro.ProductId, 
					pro.ProductImageUrl, 
					pro.ProductInOut,					
					pro.UnitOfMeasureName,
					pro.IsGoods,
					sod.*
				FROM dbo.mSalesOrderDetail as sod
					INNER JOIN dbo.mProduct as pro
					ON sod.ProductId = pro.ProductId
				WHERE sod.SalesOrderId = @SalesOrderId;
			END
			ELSE IF (@QueryType ='SHIPENTRY')
			BEGIN
				SELECT soh.*
				FROM dbo.mSalesOrderHeader soh
				WHERE soh.IsActive = 1
					AND soh.SalesOrderNumber IS NOT NULL

					-- Service is not allowed to receive as Stock
					-- Only Goods Allowed for Stock
					-- Added on 01-09-2021
					AND soh.SalesOrderId IN (SELECT sod.SalesOrderId 
							FROM mSalesOrderDetail as sod
								INNER JOIN mProduct as pro
							ON sod.ProductId = pro.ProductId
							WHERE pro.IsGoods = 1)
					-- END

					AND soh.SalesOrderId NOT IN (SELECT SalesOrderId
						FROM dbo.mShipmentHeader
						WHERE IsFullShipped = 1);
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