CREATE FUNCTION [dbo].[IsFullShipped]
(
	@SalesOrderId bigint
)  
RETURNS bit   
AS  
BEGIN 
	DECLARE @Flag bit;
	SET @Flag = 0;
	-- Service is not allowed to receive as Stock
	-- Only Goods Allowed for Stock
	-- Added on 01-09-2021
	IF((SELECT SUM(sod.Quantity) FROM mSalesOrderDetail as sod
				INNER JOIN mProduct as pro
			ON sod.ProductId = pro.ProductId
			WHERE sod.SalesOrderId = @SalesOrderId
				AND pro.IsGoods = 1) 
		<= 
		(SELECT ISNULL(SUM(ShippedQuantity), 0) from mShipmentDetail where SalesOrderDetailId IN 
			(SELECT SalesOrderDetailId from mSalesOrderDetail where SalesOrderId = @SalesOrderId)))
	-- END
	BEGIN
		SET @Flag = 1;
	END
	RETURN @Flag;
END;