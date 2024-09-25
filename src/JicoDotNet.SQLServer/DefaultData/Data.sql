INSERT INTO dbo.mBranch(BranchName, State, IsActive, TransactionDate, RequestId)
VALUES ('Head Branch', '29' , 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO mWareHouse (BranchId, WareHouseName, IsRetailCounter, IsActive, TransactionDate, RequestId)
VALUES (CAST(SCOPE_IDENTITY() as BIGINT), 'Default Warehouse', 1, 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO dbo.mUnitOfMeasure( UnitOfMeasureName, Description, IsActive, TransactionDate, RequestId)
VALUES ('Pc', 'Piece', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO dbo.mProductType (ProductTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Product Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO dbo.mVendorType (VendorTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Vendor Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO dbo.mCustomerType (CustomerTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Customer Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO dbo.mPaymentType (PaymentTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Payment Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO dbo.mShipmentType (ShipmentTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Shipment Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO dbo.mBillType (BillTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Bill Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO dbo.mInvoiceType (InvoiceTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Invoice Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO dbo.mPurchaseType (PurchaseTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Purchase Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO dbo.mSalesType (SalesTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Sales Type', 1, GETDATE(), 'DefaultDataInsert');