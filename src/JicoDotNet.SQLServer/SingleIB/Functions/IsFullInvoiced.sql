CREATE FUNCTION [SingleIB].[IsFullInvoiced]
(
	@SalesOrderId bigint
)  
RETURNS bit   
AS  
BEGIN 
	DECLARE @Flag bit;
	SET @Flag = 0;
	IF((SELECT SUM(Quantity) from mSalesOrderDetail where SalesOrderId = @SalesOrderId) <= 
		(SELECT ISNULL(SUM(InvoicedQuantity), 0) from mInvoiceDetail where SalesOrderDetailId IN 
			(SELECT SalesOrderDetailId from mSalesOrderDetail where SalesOrderId = @SalesOrderId)))
	BEGIN
		SET @Flag = 1;
	END
	RETURN @Flag;
END;