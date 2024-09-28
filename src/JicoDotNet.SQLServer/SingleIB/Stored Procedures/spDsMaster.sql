CREATE PROCEDURE [SingleIB].[spDsMaster]
(
	@QueryType	varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';

	DECLARE @CurrentDate datetime;
	Set @CurrentDate = SingleIB.ISTNow();
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='HOMECOUNT')
			BEGIN
				SELECT 
					(SELECT COUNT(*) FROM SingleIB.mUnitOfMeasure WHERE IsActive = 1) AS UnitOfMeasure,
					(SELECT COUNT(*) FROM SingleIB.mProduct WHERE IsActive = 1) AS Product,
					(SELECT COUNT(*) FROM SingleIB.mWareHouse WHERE IsActive = 1) AS Warehouse,


					(SELECT COUNT(*) FROM SingleIB.mBillHeader WHERE IsActive = 1
										AND YEAR(BillDate) = YEAR(@CurrentDate) AND MONTH(BillDate) = MONTH(@CurrentDate)) AS Bill,
					(SELECT COUNT(*) FROM SingleIB.mBillType WHERE IsActive = 1) AS BillType,
					(SELECT COUNT(*) FROM SingleIB.mInvoiceHeader WHERE IsActive = 1
										AND YEAR(InvoiceDate) = YEAR(@CurrentDate) AND MONTH(InvoiceDate) = MONTH(@CurrentDate)) AS Invoice,
					(SELECT COUNT(*) FROM SingleIB.mInvoiceType WHERE IsActive = 1) AS InvoiceType,
					(SELECT COUNT(*) FROM SingleIB.mPurchaseType WHERE IsActive = 1) AS PurchaseType,
					(SELECT COUNT(*) FROM SingleIB.mSalesType WHERE IsActive = 1) AS SalesType,
					(SELECT COUNT(*) FROM SingleIB.mPurchaseOrderHeader 
									WHERE IsActive = 1 
										AND YEAR(PurchaseOrderDate) = YEAR(@CurrentDate) AND MONTH(PurchaseOrderDate) = MONTH(@CurrentDate)) AS PurchaseOrder,
					(SELECT COUNT(*) FROM SingleIB.mSalesOrderHeader 
									WHERE IsActive = 1
										AND YEAR(SalesOrderDate) = YEAR(@CurrentDate) AND MONTH(SalesOrderDate) = MONTH(@CurrentDate)) AS SalesOrder,

					(SELECT COUNT(*) FROM SingleIB.mBranch WHERE IsActive = 1) AS Branch,
					(SELECT COUNT(*) FROM SingleIB.mProductType WHERE IsActive = 1) AS ProductType,
					(SELECT COUNT(*) FROM SingleIB.mVendorType WHERE IsActive = 1) AS VendorType,
					(SELECT COUNT(*) FROM SingleIB.mVendor WHERE IsActive = 1) AS Vendor,
					(SELECT COUNT(*) FROM SingleIB.mCustomerType WHERE IsActive = 1) AS CustomerType,
					(SELECT COUNT(*) FROM SingleIB.mCustomer WHERE IsActive = 1) AS Customer,					
					(SELECT COUNT(*) FROM SingleIB.mShipmentType WHERE IsActive = 1) AS ShipmentType;
			END
			ELSE IF (@QueryType ='REPORTCOUNT')
			BEGIN
				SELECT 
					(SELECT COUNT(*) FROM SingleIB.mPurchaseOrderHeader WHERE 
							YEAR(PurchaseOrderDate) = YEAR(@CurrentDate) 
							AND MONTH(PurchaseOrderDate) = MONTH(@CurrentDate)
							AND IsActive = 1) 
						AS PurchaseOrderMonth,
					(SELECT SUM(PurchaseOrderTotalAmount) FROM SingleIB.mPurchaseOrderHeader WHERE 
							YEAR(PurchaseOrderDate) = YEAR(@CurrentDate) 
							AND MONTH(PurchaseOrderDate) = MONTH(@CurrentDate)
							AND IsActive = 1) 
						AS PurchaseOrderTotalAmountMonth,
					-- GRN					
					(SELECT SUM(TotalAmount) FROM SingleIB.mBillHeader WHERE 
							YEAR(BillDate) = YEAR(@CurrentDate) 
							AND MONTH(BillDate) = MONTH(@CurrentDate)) 
						AS BilledAmountMonth,
					(SELECT SUM(TotalAmount) FROM SingleIB.mPaymentOutHeader WHERE 
							YEAR(PaymentDate) = YEAR(@CurrentDate) 
							AND MONTH(PaymentDate) = MONTH(@CurrentDate)) 
						AS PayTotalAmountMonth,

					(SELECT COUNT(*) FROM SingleIB.mSalesOrderHeader WHERE 
							YEAR(SalesOrderDate) = YEAR(@CurrentDate) 
							AND MONTH(SalesOrderDate) = MONTH(@CurrentDate)
							AND IsActive = 1) 
						AS SalesOrderMonth,
					(SELECT SUM(SalesOrderTotalAmount) FROM SingleIB.mSalesOrderHeader WHERE 
							YEAR(SalesOrderDate) = YEAR(@CurrentDate) 
							AND MONTH(SalesOrderDate) = MONTH(@CurrentDate)
							AND IsActive = 1) 
						AS SalesOrderTotalAmountMonth,
					-- SHIP					
					(SELECT SUM(TotalAmount) FROM SingleIB.mInvoiceHeader WHERE 
							YEAR(InvoiceDate) = YEAR(@CurrentDate) 
							AND MONTH(InvoiceDate) = MONTH(@CurrentDate)) 
						AS InvoicedAmountMonth,
					(SELECT SUM(TotalAmount) FROM SingleIB.mPaymentInHeader WHERE 
							YEAR(PaymentDate) = YEAR(@CurrentDate) 
							AND MONTH(PaymentDate) = MONTH(@CurrentDate)) 
						AS ReceiveTotalAmountMonth,
					(SELECT SUM(TDSAmount) FROM SingleIB.mPaymentOutHeader WHERE 
							YEAR(PaymentDate) = YEAR(@CurrentDate) 
							AND MONTH(PaymentDate) = MONTH(@CurrentDate)) 
						AS TDSPayAmountMonth, 
					(SELECT SUM(TDSAmount) FROM SingleIB.tTDSPay WHERE 
							IsPaid = 1
							AND YEAR(PayDate) = YEAR(@CurrentDate) 
							AND MONTH(PayDate) = MONTH(@CurrentDate)) 
						AS TDSPaidAmountMonth, 

					(SELECT SUM(TDSAmount) FROM SingleIB.mPaymentInHeader WHERE 
							YEAR(PaymentDate) = YEAR(@CurrentDate) 
							AND MONTH(PaymentDate) = MONTH(@CurrentDate)) 
						AS TDSReceiveAmountMonth, 
					(SELECT SUM(TDSAmount) FROM SingleIB.tTDSReceive WHERE 
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