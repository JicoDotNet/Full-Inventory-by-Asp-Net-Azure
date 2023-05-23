CREATE PROCEDURE [dbo].[spRpPurchase]
(
	@StartDate			datetime = null,
	@EndDate			datetime = null,

	-- Vendor
	@VendorTypeId		bigint = 0,
	@VendorId			bigint = 0,

	-- Product
	@ProductId			bigint = 0,
	@ProductTypeId		bigint = 0,

	@QueryType			varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'PurchaseReport';

	DECLARE @SQL VARCHAR(8000) = NULL,
	        @WhereClause	varchar(8000) = null
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='BYVENDOR')
			BEGIN
				SET @SQL = 'SELECT * FROM 
						(SELECT bilh.VendorId as VendorId, 
						COUNT(*) as TotalBillCount, 
						SUM(bilh.BilledAmount) as SumBilledAmount,
						SUM(bilh.TaxAmount) as SumTaxAmount,
						SUM(bilh.TotalAmount) as SumTotalAmount
						FROM dbo.mBillHeader as bilh
						WHERE (bilh.BillDate BETWEEN CONVERT(DATE, ''' + CAST(YEAR(@StartDate) as VARCHAR) + '-' 
															+ CAST(MONTH(@StartDate) as VARCHAR) + '-' 
															+ CAST(DAY(@StartDate) as VARCHAR) + ''') 
								AND CONVERT(DATE, ''' + CAST(YEAR(@EndDate) as VARCHAR) + '-' 
										+ CAST(MONTH(@EndDate) as VARCHAR) + '-' 
										+ CAST(DAY(@EndDate) as VARCHAR) + '''))
							GROUP BY bilh.VendorId) 
					AS inrtab,
					dbo.mVendor as ven 
					WHERE ven.VendorId = inrtab.VendorId';
				
				--SELECT @StartDate;
				--SELECT CAST(@StartDate as VARCHAR);
				--SELECT CAST(CAST(@StartDate as VARCHAR) as DATETIME);

				IF @VendorId <> 0
				BEGIN
					IF @WhereClause IS NOT NULL
					BEGIN
						SET @WhereClause = @WhereClause + ' AND ven.VendorId = ' + CAST(@VendorId as VARCHAR);
					END
					ELSE
					BEGIN
						SET @WhereClause = ' ven.VendorId = ' + CAST(@VendorId as VARCHAR);
					END
				END
				IF @VendorTypeId <> 0
				BEGIN
					IF @WhereClause IS NOT NULL
					BEGIN
						SET @WhereClause = @WhereClause + ' AND ven.VendorTypeId = ' + CAST(@VendorTypeId as VARCHAR);
					END
					ELSE
					BEGIN
						SET @WhereClause = ' ven.VendorTypeId = ' + CAST(@VendorTypeId as VARCHAR);
					END
				END
				IF @WhereClause IS NOT NULL
				BEGIN
					SET @SQL = @SQL + ' AND ' + @WhereClause 
				END

				--PRINT(@WhereClause);
				--PRINT (@SQL);
				EXECUTE(@SQL);
				SELECT @SQL as asd;
			END
			ELSE IF (@QueryType ='BYPRODUCT')
			BEGIN
				SET @SQL = 'SELECT * FROM 
						(SELECT billd.ProductId as ProductId, 
						SUM(billd.BilledQuantity) as TotalQuantity, 
						SUM(billd.Total) as TotalBilledAmount,
						SUM(billd.SubTotal) as TotalPrice,
						SUM(billd.CGSTAmount) as TotalCGST,
						SUM(billd.SGSTAmount) as TotalSGST,
						SUM(billd.IGSTAmount) as TotalIGST,
						SUM(billd.CGSTAmount + billd.SGSTAmount + billd.IGSTAmount) as TotalTax
						FROM dbo.mBillDetail as billd
						LEFT JOIN dbo.mBillHeader as billh
							ON billd.BillId = billh.BillId
						WHERE (billh.BillDate BETWEEN CONVERT(DATE, ''' + CAST(YEAR(@StartDate) as VARCHAR) + '-' 
														+ CAST(MONTH(@StartDate) as VARCHAR) + '-' 
														+ CAST(DAY(@StartDate) as VARCHAR) + ''') 
								AND CONVERT(DATE, ''' + CAST(YEAR(@EndDate) as VARCHAR) + '-' 
									+ CAST(MONTH(@EndDate) as VARCHAR) + '-' 
									+ CAST(DAY(@EndDate) as VARCHAR) + ''')) 
						GROUP BY billd.ProductId) AS inrtab,
						dbo.mProduct as pro 
						WHERE pro.ProductId = inrtab.ProductId
							AND pro.ProductInOut IN (0, 1) ';

				IF @ProductId <> 0
				BEGIN
					IF @WhereClause IS NOT NULL
					BEGIN
						SET @WhereClause = @WhereClause + ' AND pro.ProductId = ' + CAST(@ProductId as VARCHAR);
					END
					ELSE
					BEGIN
						SET @WhereClause = ' pro.ProductId = ' + CAST(@ProductId as VARCHAR);
					END
				END
				IF @ProductTypeId <> 0
				BEGIN
					IF @WhereClause IS NOT NULL
					BEGIN
						SET @WhereClause = @WhereClause + ' AND pro.ProductTypeId = ' + CAST(@ProductTypeId as VARCHAR);
					END
					ELSE
					BEGIN
						SET @WhereClause = ' pro.ProductTypeId = ' + CAST(@ProductTypeId as VARCHAR);
					END
				END
				IF @WhereClause IS NOT NULL
				BEGIN
					SET @SQL = @SQL + ' AND ' + @WhereClause 
				END

				--PRINT (@SQL);
				EXECUTE(@SQL);
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