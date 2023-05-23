CREATE FUNCTION [dbo].[IsFullReceived]
(
	@PurchaseOrderId bigint
)  
RETURNS bit   
AS  
BEGIN 	
	DECLARE @Flag bit;
	SET @Flag = 0;
	IF((SELECT SUM(Quantity) from mPurchaseOrderDetail where PurchaseOrderId = @PurchaseOrderId) <= 
		(SELECT ISNULL(SUM(ReceivedQuantity), 0) from mGRNDetail where PurchaseOrderDetailId IN 
			(SELECT PurchaseOrderDetailId from mPurchaseOrderDetail where PurchaseOrderId = @PurchaseOrderId)))
	BEGIN
		SET @Flag = 1;
	END
	RETURN @Flag;
END;