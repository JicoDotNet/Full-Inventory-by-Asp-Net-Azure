CREATE PROCEDURE [SingleIB].[spGetStock]
(
	@ProductId			bigint = 0,
	@WareHouseId		bigint = 0,
	@GRNOrShipmentDate  datetime = null,
	
	@QueryType			varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'GET';

	SET NOCOUNT ON;
	DECLARE @SQL VARCHAR(8000) = NULL,
	        @WhereClause	varchar(8000) = null
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='CURRENT')
			BEGIN
				-- Get Stock
				SET @SQL = 'SELECT stk.*,
							pro.Brand,
							pro.ProductName, 
							pro.ProductCode,
							pro.ProductImageUrl,
							pro.Description,
							pro.IsPerishableProduct,
							pro.HasExpirationDate,
							pro.HasBatchNo,
							who.WareHouseName,
							pro.UnitOfMeasureName
							FROM SingleIB.tStock as stk 
							INNER JOIN SingleIB.mProduct as pro
							ON stk.ProductId = pro.ProductId
							INNER JOIN 	SingleIB.mWareHouse as who
							ON stk.WareHouseId = who.WareHouseId ';

				IF @GRNOrShipmentDate IS NULL
				BEGIN
					IF @ProductId <> 0
					BEGIN
						IF @WhereClause IS NOT NULL
						BEGIN
							SET @WhereClause = @WhereClause + ' AND stk.ProductId = ''' + CAST(@ProductId as VARCHAR) + ''''
						END
						ELSE
						BEGIN
							SET @WhereClause = ' stk.ProductId = ''' + CAST(@ProductId as VARCHAR) + ''''
						END
					END
					IF @WareHouseId <> 0
					BEGIN
						IF @WhereClause IS NOT NULL
						BEGIN
							SET @WhereClause = @WhereClause + ' AND stk.WareHouseId = ''' + CAST(@WareHouseId as VARCHAR) + ''''
						END
						ELSE
						BEGIN
							SET @WhereClause = ' stk.WareHouseId = ''' + CAST(@WareHouseId as VARCHAR) + ''''
						END
					END
					IF @WhereClause IS NOT NULL
					BEGIN
						SET @SQL = @SQL + ' WHERE ' + @WhereClause + ' AND stk.IsActive = 1 '
					END
					ELSE
					BEGIN
						SET @SQL = @SQL + ' WHERE stk.IsActive = 1 '
					END
				END 

				--PRINT @SQL
				EXECUTE(@SQL) 
			END
			ELSE IF(@QueryType ='DETAIL')
			BEGIN
				EXEC [SingleIB].[spGetStock]
					@ProductId = @ProductId,
					@WareHouseId = @WareHouseId,
					@GRNOrShipmentDate = @GRNOrShipmentDate,
					@QueryType = 'CURRENT';

				SET @SQL = 'SELECT stkd.* FROM [SingleIB].[tStockDetail] AS stkd ';
				IF @ProductId <> 0
				BEGIN
					IF @WhereClause IS NOT NULL
					BEGIN
						SET @WhereClause = @WhereClause + ' AND stkd.ProductId = ''' + CAST(@ProductId as VARCHAR) + ''''
					END
					ELSE
					BEGIN
						SET @WhereClause = ' stkd.ProductId = ''' + CAST(@ProductId as VARCHAR) + ''''
					END
				END
				IF @WareHouseId <> 0
				BEGIN
					IF @WhereClause IS NOT NULL
					BEGIN
						SET @WhereClause = @WhereClause + ' AND stkd.WareHouseId = ''' + CAST(@WareHouseId as VARCHAR) + ''''
					END
					ELSE
					BEGIN
						SET @WhereClause = ' stkd.WareHouseId = ''' + CAST(@WareHouseId as VARCHAR) + ''''
					END
				END
				IF @WhereClause IS NOT NULL
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause + ' AND stkd.IsActive = 1 AND stkd.Stock > 0 '
									
				END
				ELSE
				BEGIN
					SET @SQL = @SQL + ' WHERE stkd.IsActive = 1 AND stkd.Stock > 0 '
				END
				EXECUTE(@SQL) ;
				-- Old Code
				--SELECT stkd.* FROM [SingleIB].[tStockDetail] AS stkd
				--WHERE stkd.IsActive = 1 
				--	AND stkd.Stock > 0 
				--	AND stkd.WareHouseId = @WareHouseId
			END
			ELSE IF(@QueryType ='NOTOPNINGSTOCK')
			BEGIN
				SELECT Count(*) AS ProductQuantity
				FROM SingleIB.tStock as stk  
				WHERE stk.ProductId = @ProductId
					AND stk.StockType <> 10;
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