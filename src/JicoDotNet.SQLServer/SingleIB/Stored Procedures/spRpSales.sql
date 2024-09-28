CREATE PROCEDURE [SingleIB].[spRpSales]
(
	@StartDate			datetime = null,
	@EndDate			datetime = null,
	@ForRetail			bit = null,

	-- Customer
	@CustomerTypeId		bigint = 0,
	@CustomerId			bigint = 0,	

	-- Product
	@ProductId			bigint = 0,
	@ProductTypeId		bigint = 0,

	@QueryType			varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'SalesReport';

	DECLARE @SQL VARCHAR(8000) = NULL,
	        @WhereClause	varchar(8000) = null
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='BYCUSTOMER')
			BEGIN
				SET @SQL = 'SELECT * FROM 
						(SELECT ivh.CustomerId as CustomerId, 
						COUNT(*) as TotalInvoiceCount, 
						SUM(ivh.InvoicedAmount) as SumInvoicedAmount,
						SUM(ivh.TaxAmount) as SumTaxAmount,
						SUM(ivh.TotalAmount) as SumTotalAmount
						FROM SingleIB.mInvoiceHeader as ivh
						WHERE (ivh.InvoiceDate BETWEEN CONVERT(DATE, ''' + CAST(YEAR(@StartDate) as VARCHAR) + '-' 
															+ CAST(MONTH(@StartDate) as VARCHAR) + '-' 
															+ CAST(DAY(@StartDate) as VARCHAR) + ''') 
								AND CONVERT(DATE, ''' + CAST(YEAR(@EndDate) as VARCHAR) + '-' 
										+ CAST(MONTH(@EndDate) as VARCHAR) + '-' 
										+ CAST(DAY(@EndDate) as VARCHAR) + '''))
							GROUP BY ivh.CustomerId) 
					AS inrtab,
					SingleIB.mCustomer as cus 
					WHERE cus.CustomerId = inrtab.CustomerId';
				
				--SELECT @StartDate;
				--SELECT CAST(@StartDate as VARCHAR);
				--SELECT CAST(CAST(@StartDate as VARCHAR) as DATETIME);

				IF @CustomerId <> 0
				BEGIN
					IF @WhereClause IS NOT NULL
					BEGIN
						SET @WhereClause = @WhereClause + ' AND cus.CustomerId = ' + CAST(@CustomerId as VARCHAR);
					END
					ELSE
					BEGIN
						SET @WhereClause = ' cus.CustomerId = ' + CAST(@CustomerId as VARCHAR);
					END
				END
				IF @CustomerTypeId <> 0
				BEGIN
					IF @WhereClause IS NOT NULL
					BEGIN
						SET @WhereClause = @WhereClause + ' AND cus.CustomerTypeId = ' + CAST(@CustomerTypeId as VARCHAR);
					END
					ELSE
					BEGIN
						SET @WhereClause = ' cus.CustomerTypeId = ' + CAST(@CustomerTypeId as VARCHAR);
					END
				END
				IF @ForRetail IS NOT NULL
				BEGIN
					IF @WhereClause IS NOT NULL
					BEGIN
						SET @WhereClause = @WhereClause + ' AND cus.IsRetailCustomer = ' + CAST(@ForRetail as VARCHAR);
					END
					ELSE
					BEGIN
						SET @WhereClause = ' cus.IsRetailCustomer = ' + CAST(@ForRetail as VARCHAR);
					END
				END
				IF @WhereClause IS NOT NULL
				BEGIN
					SET @SQL = @SQL + ' AND ' + @WhereClause 
				END

				--PRINT(@WhereClause);
				--PRINT (@SQL);
				EXECUTE(@SQL);
			END
			ELSE IF (@QueryType ='BYPRODUCT')
			BEGIN
				SET @SQL = 'SELECT * FROM 
							(SELECT ivd.ProductId as ProductId, 
							SUM(ivd.InvoicedQuantity) as TotalQuantity, 
							SUM(ivd.Total) as TotalInvoicedAmount,
							SUM(ivd.SubTotal) as TotalPrice,
							SUM(ivd.CGSTAmount) as TotalCGST,
							SUM(ivd.SGSTAmount) as TotalSGST,
							SUM(ivd.IGSTAmount) as TotalIGST,
							SUM(ivd.CGSTAmount + ivd.SGSTAmount + ivd.IGSTAmount) as TotalTax
							FROM SingleIB.mInvoiceDetail as ivd
							LEFT JOIN SingleIB.mInvoiceHeader as ivh
								ON ivd.InvoiceId = ivh.InvoiceId
							WHERE (ivh.InvoiceDate BETWEEN CONVERT(DATE, ''' + CAST(YEAR(@StartDate) as VARCHAR) + '-' 
															+ CAST(MONTH(@StartDate) as VARCHAR) + '-' 
															+ CAST(DAY(@StartDate) as VARCHAR) + ''') 
									AND CONVERT(DATE, ''' + CAST(YEAR(@EndDate) as VARCHAR) + '-' 
										+ CAST(MONTH(@EndDate) as VARCHAR) + '-' 
										+ CAST(DAY(@EndDate) as VARCHAR) + ''')) ' +
								IIF(@ForRetail IS NULL, 
									' ', 
									' AND ivh.CustomerId IN (SELECT CustomerId FROM SingleIB.mCustomer 
											WHERE IsRetailCustomer = '+ CAST(@ForRetail as varchar(1))+') ') +								
							' GROUP BY ivd.ProductId) 
						AS inrtab,
						SingleIB.mProduct as pro 
						WHERE pro.ProductId = inrtab.ProductId 
							AND pro.ProductInOut IN (0, 2) ';

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