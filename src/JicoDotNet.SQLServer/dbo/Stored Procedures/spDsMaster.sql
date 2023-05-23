CREATE PROCEDURE [dbo].[spDsMaster]
(
	@QueryType	varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';

	DECLARE @CurrentDate datetime;
	Set @CurrentDate = dbo.ISTNow();
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='HOMECOUNT')
			BEGIN
				SELECT 
					(SELECT COUNT(*) FROM dbo.mUnitOfMeasure WHERE IsActive = 1) AS UnitOfMeasure,
					(SELECT COUNT(*) FROM dbo.mProduct WHERE IsActive = 1) AS Product,
					(SELECT COUNT(*) FROM dbo.mWareHouse WHERE IsActive = 1) AS Warehouse,


					(SELECT COUNT(*) FROM dbo.mBillHeader WHERE IsActive = 1
										AND YEAR(BillDate) = YEAR(@CurrentDate) AND MONTH(BillDate) = MONTH(@CurrentDate)) AS Bill,
					(SELECT COUNT(*) FROM dbo.mBillType WHERE IsActive = 1) AS BillType,
					(SELECT COUNT(*) FROM dbo.mInvoiceHeader WHERE IsActive = 1
										AND YEAR(InvoiceDate) = YEAR(@CurrentDate) AND MONTH(InvoiceDate) = MONTH(@CurrentDate)) AS Invoice,
					(SELECT COUNT(*) FROM dbo.mInvoiceType WHERE IsActive = 1) AS InvoiceType,
					(SELECT COUNT(*) FROM dbo.mPurchaseType WHERE IsActive = 1) AS PurchaseType,
					(SELECT COUNT(*) FROM dbo.mSalesType WHERE IsActive = 1) AS SalesType,
					(SELECT COUNT(*) FROM dbo.mPurchaseOrderHeader 
									WHERE IsActive = 1 
										AND YEAR(PurchaseOrderDate) = YEAR(@CurrentDate) AND MONTH(PurchaseOrderDate) = MONTH(@CurrentDate)) AS PurchaseOrder,
					(SELECT COUNT(*) FROM dbo.mSalesOrderHeader 
									WHERE IsActive = 1
										AND YEAR(SalesOrderDate) = YEAR(@CurrentDate) AND MONTH(SalesOrderDate) = MONTH(@CurrentDate)) AS SalesOrder,

					(SELECT COUNT(*) FROM dbo.mBranch WHERE IsActive = 1) AS Branch,
					(SELECT COUNT(*) FROM dbo.mProductType WHERE IsActive = 1) AS ProductType,
					(SELECT COUNT(*) FROM dbo.mVendorType WHERE IsActive = 1) AS VendorType,
					(SELECT COUNT(*) FROM dbo.mVendor WHERE IsActive = 1) AS Vendor,
					(SELECT COUNT(*) FROM dbo.mCustomerType WHERE IsActive = 1) AS CustomerType,
					(SELECT COUNT(*) FROM dbo.mCustomer WHERE IsActive = 1) AS Customer,					
					(SELECT COUNT(*) FROM dbo.mShipmentType WHERE IsActive = 1) AS ShipmentType;
			END
			ELSE IF (@QueryType ='REPORTCOUNT')
			BEGIN
				SELECT 
					(SELECT COUNT(*) FROM dbo.mPurchaseOrderHeader WHERE 
							YEAR(PurchaseOrderDate) = YEAR(@CurrentDate) 
							AND MONTH(PurchaseOrderDate) = MONTH(@CurrentDate)
							AND IsActive = 1) 
						AS PurchaseOrderMonth,
					(SELECT SUM(PurchaseOrderTotalAmount) FROM dbo.mPurchaseOrderHeader WHERE 
							YEAR(PurchaseOrderDate) = YEAR(@CurrentDate) 
							AND MONTH(PurchaseOrderDate) = MONTH(@CurrentDate)
							AND IsActive = 1) 
						AS PurchaseOrderTotalAmountMonth,
					-- GRN					
					(SELECT SUM(TotalAmount) FROM dbo.mBillHeader WHERE 
							YEAR(BillDate) = YEAR(@CurrentDate) 
							AND MONTH(BillDate) = MONTH(@CurrentDate)) 
						AS BilledAmountMonth,
					(SELECT SUM(TotalAmount) FROM dbo.mPaymentOutHeader WHERE 
							YEAR(PaymentDate) = YEAR(@CurrentDate) 
							AND MONTH(PaymentDate) = MONTH(@CurrentDate)) 
						AS PayTotalAmountMonth,

					(SELECT COUNT(*) FROM dbo.mSalesOrderHeader WHERE 
							YEAR(SalesOrderDate) = YEAR(@CurrentDate) 
							AND MONTH(SalesOrderDate) = MONTH(@CurrentDate)
							AND IsActive = 1) 
						AS SalesOrderMonth,
					(SELECT SUM(SalesOrderTotalAmount) FROM dbo.mSalesOrderHeader WHERE 
							YEAR(SalesOrderDate) = YEAR(@CurrentDate) 
							AND MONTH(SalesOrderDate) = MONTH(@CurrentDate)
							AND IsActive = 1) 
						AS SalesOrderTotalAmountMonth,
					-- SHIP					
					(SELECT SUM(TotalAmount) FROM dbo.mInvoiceHeader WHERE 
							YEAR(InvoiceDate) = YEAR(@CurrentDate) 
							AND MONTH(InvoiceDate) = MONTH(@CurrentDate)) 
						AS InvoicedAmountMonth,
					(SELECT SUM(TotalAmount) FROM dbo.mPaymentInHeader WHERE 
							YEAR(PaymentDate) = YEAR(@CurrentDate) 
							AND MONTH(PaymentDate) = MONTH(@CurrentDate)) 
						AS ReceiveTotalAmountMonth,
					(SELECT SUM(TDSAmount) FROM dbo.mPaymentOutHeader WHERE 
							YEAR(PaymentDate) = YEAR(@CurrentDate) 
							AND MONTH(PaymentDate) = MONTH(@CurrentDate)) 
						AS TDSPayAmountMonth, 
					(SELECT SUM(TDSAmount) FROM dbo.tTDSPay WHERE 
							IsPaid = 1
							AND YEAR(PayDate) = YEAR(@CurrentDate) 
							AND MONTH(PayDate) = MONTH(@CurrentDate)) 
						AS TDSPaidAmountMonth, 

					(SELECT SUM(TDSAmount) FROM dbo.mPaymentInHeader WHERE 
							YEAR(PaymentDate) = YEAR(@CurrentDate) 
							AND MONTH(PaymentDate) = MONTH(@CurrentDate)) 
						AS TDSReceiveAmountMonth, 
					(SELECT SUM(TDSAmount) FROM dbo.tTDSReceive WHERE 
							IsReceived = 1
							AND YEAR(ReceivedDate) = YEAR(@CurrentDate) 
							AND MONTH(ReceivedDate) = MONTH(@CurrentDate)) 
						AS TDSReceivedAmountMonth;					
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