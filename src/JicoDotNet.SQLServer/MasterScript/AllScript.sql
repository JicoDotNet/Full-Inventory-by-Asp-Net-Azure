GO
CREATE SCHEMA [SingleIB]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[BillDetailType]...';

GO
CREATE TYPE [SingleIB].[BillDetailType] AS TABLE (
    [Id]                    INT             NULL,
    [PurchaseOrderDetailId] BIGINT          NULL,
    [ProductId]             BIGINT          NULL,
    [HSNSAC]                NVARCHAR (8)    NULL,
    [Price]                 DECIMAL (18, 5) NULL,
    [BilledQuantity]        DECIMAL (18, 5) NULL,
    [SubTotal]              DECIMAL (18, 5) NULL,
    [TaxPercentage]         DECIMAL (18, 5) NULL,
    [CGSTAmount]            DECIMAL (18, 5) NULL,
    [SGSTAmount]            DECIMAL (18, 5) NULL,
    [IGSTAmount]            DECIMAL (18, 5) NULL,
    [Total]                 DECIMAL (18, 5) NULL,
    [Description]           NVARCHAR (256)  NULL);

GO
PRINT N'Creating User-Defined Table Type [SingleIB].[GRNDetailType]...';


GO
CREATE TYPE [SingleIB].[GRNDetailType] AS TABLE (
    [Id]                    INT             NULL,
    [PurchaseOrderDetailId] BIGINT          NULL,
    [ProductId]             BIGINT          NULL,
    [ReceivedQuantity]      DECIMAL (18, 5) NULL,
    [Description]           NVARCHAR (256)  NULL,
    [IsPerishable]          BIT             NULL,
    [BatchNo]               NVARCHAR (256)  NULL,
    [ExpiryDate]            DATETIME        NULL);


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[InvoiceDetailType]...';


GO
CREATE TYPE [SingleIB].[InvoiceDetailType] AS TABLE (
    [Id]                 INT             NULL,
    [SalesOrderDetailId] BIGINT          NULL,
    [ProductId]          BIGINT          NULL,
    [HSNSAC]             NVARCHAR (8)    NULL,
    [Price]              DECIMAL (18, 5) NULL,
    [InvoicedQuantity]   DECIMAL (18, 5) NULL,
    [SubTotal]           DECIMAL (18, 5) NULL,
    [TaxPercentage]      DECIMAL (18, 5) NULL,
    [CGSTAmount]         DECIMAL (18, 5) NULL,
    [SGSTAmount]         DECIMAL (18, 5) NULL,
    [IGSTAmount]         DECIMAL (18, 5) NULL,
    [Total]              DECIMAL (18, 5) NULL,
    [Description]        NVARCHAR (256)  NULL);


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[OpeningStockDetailType]...';


GO
CREATE TYPE [SingleIB].[OpeningStockDetailType] AS TABLE (
    [Id]           INT             NULL,
    [WareHouseId]  BIGINT          NULL,
    [ProductId]    BIGINT          NULL,
    [Quantity]     DECIMAL (18, 5) NULL,
    [GRNDate]      DATETIME        NULL,
    [IsPerishable] BIT             NULL,
    [BatchNo]      NVARCHAR (256)  NULL,
    [ExpiryDate]   DATETIME        NULL,
    [Description]  NVARCHAR (256)  NULL);


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[PaymentInDetailType]...';


GO
CREATE TYPE [SingleIB].[PaymentInDetailType] AS TABLE (
    [Id]             INT             NULL,
    [InvoiceId]      BIGINT          NULL,
    [InvoiceNumber]  NVARCHAR (16)   NULL,
    [Amount]         DECIMAL (18, 5) NULL,
    [IsFullReceived] BIT             NULL,
    [PaymentDate]    DATETIME        NULL,
    [Description]    NVARCHAR (256)  NULL);


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[PaymentOutDetailType]...';


GO
CREATE TYPE [SingleIB].[PaymentOutDetailType] AS TABLE (
    [Id]          INT             NULL,
    [BillId]      BIGINT          NULL,
    [BillNumber]  NVARCHAR (16)   NULL,
    [Amount]      DECIMAL (18, 5) NULL,
    [IsFullPaid]  BIT             NULL,
    [PaymentDate] DATETIME        NULL,
    [Description] NVARCHAR (256)  NULL);


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[PurchaseOrderDetailType]...';


GO
CREATE TYPE [SingleIB].[PurchaseOrderDetailType] AS TABLE (
    [Id]                    INT             NULL,
    [PurchaseOrderDetailId] BIGINT          NULL,
    [PurchaseOrderId]       BIGINT          NULL,
    [PurchaseOrderNumber]   NVARCHAR (16)   NULL,
    [AmendmentNumber]       NVARCHAR (4)    NULL,
    [ProductId]             BIGINT          NULL,
    [HSNSAC]                NVARCHAR (8)    NULL,
    [Amount]                DECIMAL (18, 5) NULL,
    [DiscountPercentage]    DECIMAL (18, 5) NULL,
    [DiscountAmount]        DECIMAL (18, 5) NULL,
    [Price]                 DECIMAL (18, 5) NULL,
    [Quantity]              DECIMAL (18, 5) NULL,
    [SubTotal]              DECIMAL (18, 5) NULL,
    [TaxPercentage]         DECIMAL (18, 5) NULL,
    [TaxAmount]             DECIMAL (18, 5) NULL,
    [Total]                 DECIMAL (18, 5) NULL,
    [Description]           NVARCHAR (256)  NULL,
    [TransactionDate]       DATETIME2 (7)   NULL,
    [IsActive]              BIT             NULL,
    [RequestId]             NVARCHAR (64)   NULL);


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[QuotationDetailType]...';


GO
CREATE TYPE [SingleIB].[QuotationDetailType] AS TABLE (
    [Id]                 INT             NULL,
    [ProductId]          BIGINT          NULL,
    [HSNSAC]             NVARCHAR (8)    NULL,
    [Amount]             DECIMAL (18, 5) NULL,
    [DiscountPercentage] DECIMAL (18, 5) NULL,
    [DiscountAmount]     DECIMAL (18, 5) NULL,
    [Price]              DECIMAL (18, 5) NULL,
    [Quantity]           DECIMAL (18, 5) NULL,
    [SubTotal]           DECIMAL (18, 5) NULL,
    [TaxPercentage]      DECIMAL (18, 5) NULL,
    [TaxAmount]          DECIMAL (18, 5) NULL,
    [Total]              DECIMAL (18, 5) NULL,
    [Description]        NVARCHAR (256)  NULL);


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[SalesOrderDetailType]...';


GO
CREATE TYPE [SingleIB].[SalesOrderDetailType] AS TABLE (
    [Id]                 INT             NULL,
    [SalesOrderDetailId] BIGINT          NULL,
    [SalesOrderId]       BIGINT          NULL,
    [SalesOrderNumber]   NVARCHAR (16)   NULL,
    [AmendmentNumber]    NVARCHAR (4)    NULL,
    [ProductId]          BIGINT          NULL,
    [HSNSAC]             NVARCHAR (8)    NULL,
    [Amount]             DECIMAL (18, 5) NULL,
    [DiscountPercentage] DECIMAL (18, 5) NULL,
    [DiscountAmount]     DECIMAL (18, 5) NULL,
    [Price]              DECIMAL (18, 5) NULL,
    [Quantity]           DECIMAL (18, 5) NULL,
    [SubTotal]           DECIMAL (18, 5) NULL,
    [TaxPercentage]      DECIMAL (18, 5) NULL,
    [TaxAmount]          DECIMAL (18, 5) NULL,
    [Total]              DECIMAL (18, 5) NULL,
    [Description]        NVARCHAR (256)  NULL);


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[SearchType]...';


GO
CREATE TYPE [SingleIB].[SearchType] AS TABLE (
    [Id]          INT           NULL,
    [SearchValue] NVARCHAR (64) NULL);


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[ShipmentDetailType]...';


GO
CREATE TYPE [SingleIB].[ShipmentDetailType] AS TABLE (
    [Id]                 INT             NULL,
    [SalesOrderDetailId] BIGINT          NULL,
    [ProductId]          BIGINT          NULL,
    [ShippedQuantity]    DECIMAL (18, 5) NULL,
    [StockDetailId]      BIGINT          NULL,
    [IsPerishable]       BIT             NULL,
    [ExpiryDate]         DATETIME        NULL,
    [BatchNo]            NVARCHAR (256)  NULL,
    [Description]        NVARCHAR (256)  NULL);


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[StockAdjustDetailType]...';


GO
CREATE TYPE [SingleIB].[StockAdjustDetailType] AS TABLE (
    [Id]                INT             NULL,
    [ProductId]         BIGINT          NULL,
    [AvailableQuantity] DECIMAL (18, 5) NULL,
    [AdjustQuantity]    DECIMAL (18, 5) NULL,
    [StockDetailId]     BIGINT          NULL,
    [GRNDate]           DATETIME        NULL,
    [IsPerishable]      BIT             NULL,
    [BatchNo]           NVARCHAR (256)  NULL,
    [ExpiryDate]        DATETIME        NULL,
    [Description]       NVARCHAR (256)  NULL);


GO
PRINT N'Creating User-Defined Table Type [SingleIB].[StockTransferDetailType]...';


GO
CREATE TYPE [SingleIB].[StockTransferDetailType] AS TABLE (
    [Id]                INT             NULL,
    [ProductId]         BIGINT          NULL,
    [AvailableQuantity] DECIMAL (18, 5) NULL,
    [TransferQuantity]  DECIMAL (18, 5) NULL,
    [Description]       NVARCHAR (256)  NULL,
    [StockDetailId]     BIGINT          NULL,
    [IsPerishable]      BIT             NULL,
    [BatchNo]           NVARCHAR (256)  NULL,
    [ExpiryDate]        DATETIME        NULL);


GO
PRINT N'Creating Table [SingleIB].[mBillDetail]...';


GO
CREATE TABLE [SingleIB].[mBillDetail] (
    [BillDetailId]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [BillId]                BIGINT          NULL,
    [BillNumber]            NVARCHAR (16)   NULL,
    [PurchaseOrderDetailId] BIGINT          NULL,
    [ProductId]             BIGINT          NULL,
    [HSNSAC]                NVARCHAR (8)    NULL,
    [Price]                 DECIMAL (18, 5) NULL,
    [BilledQuantity]        DECIMAL (18, 5) NULL,
    [SubTotal]              DECIMAL (18, 5) NULL,
    [TaxPercentage]         DECIMAL (18, 5) NULL,
    [CGSTAmount]            DECIMAL (18, 5) NULL,
    [SGSTAmount]            DECIMAL (18, 5) NULL,
    [IGSTAmount]            DECIMAL (18, 5) NULL,
    [Total]                 DECIMAL (18, 5) NULL,
    [Description]           NVARCHAR (256)  NULL,
    [IsActive]              BIT             NULL,
    [TransactionDate]       DATETIME        NULL,
    [RequestId]             NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mBillDetail] PRIMARY KEY CLUSTERED ([BillDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mBillHeader]...';


GO
CREATE TABLE [SingleIB].[mBillHeader] (
    [BillId]              BIGINT          IDENTITY (1, 1) NOT NULL,
    [BillTypeId]          BIGINT          NULL,
    [BillDate]            DATETIME        NULL,
    [BillDueDate]         DATETIME        NULL,
    [BillNumber]          NVARCHAR (16)   NULL,
    [IsFullBilled]        BIT             NULL,
    [PurchaseOrderId]     BIGINT          NULL,
    [VendorId]            BIGINT          NULL,
    [IsGstApplicable]     BIT             NULL,
    [GSTNumber]           NVARCHAR (16)   NULL,
    [GSTStateCode]        NVARCHAR (2)    NULL,
    [GSTType]             SMALLINT        NULL,
    [BilledAmount]        DECIMAL (18, 5) NULL,
    [TaxAmount]           DECIMAL (18, 5) NULL,
    [TotalAmount]         DECIMAL (18, 5) NULL,
    [VendorDONumber]      NVARCHAR (64)   NULL,
    [VendorInvoiceNumber] NVARCHAR (64)   NULL,
    [VendorInvoiceDate]   DATETIME        NULL,
    [Remarks]             NVARCHAR (512)  NULL,
    [IsActive]            BIT             NULL,
    [TransactionDate]     DATETIME        NULL,
    [RequestId]           NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mBill] PRIMARY KEY CLUSTERED ([BillId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mBillType]...';


GO
CREATE TABLE [SingleIB].[mBillType] (
    [BillTypeId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [BillTypeName]    NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mBillType] PRIMARY KEY CLUSTERED ([BillTypeId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mBranch]...';


GO
CREATE TABLE [SingleIB].[mBranch] (
    [BranchId]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [BranchName]      NVARCHAR (64)  NULL,
    [BranchCode]      NVARCHAR (64)  NULL,
    [Address]         NVARCHAR (128) NULL,
    [City]            NVARCHAR (32)  NULL,
    [State]           NVARCHAR (2)   NULL,
    [PostalCode]      NVARCHAR (8)   NULL,
    [ContactPerson]   NVARCHAR (64)  NULL,
    [Email]           NVARCHAR (64)  NULL,
    [Phone]           NVARCHAR (16)  NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mBranch] PRIMARY KEY CLUSTERED ([BranchId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mCompanyBank]...';


GO
CREATE TABLE [SingleIB].[mCompanyBank] (
    [CompanyBankId]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [AccountName]     NVARCHAR (1024) NULL,
    [AccountNumber]   VARCHAR (64)    NULL,
    [BankName]        NVARCHAR (128)  NULL,
    [IFSC]            NVARCHAR (16)   NULL,
    [MICR]            NVARCHAR (16)   NULL,
    [BranchName]      NVARCHAR (64)   NULL,
    [BranchAddress]   NVARCHAR (128)  NULL,
    [IsPrintable]     BIT             NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mCompanyBank] PRIMARY KEY CLUSTERED ([CompanyBankId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mCustomer]...';


GO
CREATE TABLE [SingleIB].[mCustomer] (
    [CustomerId]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [CustomerTypeId]   BIGINT          NULL,
    [CompanyName]      NVARCHAR (1204) NULL,
    [CompanyType]      NVARCHAR (64)   NULL,
    [StateCode]        NVARCHAR (2)    NULL,
    [IsGSTRegistered]  BIT             NULL,
    [GSTStateCode]     NVARCHAR (2)    NULL,
    [GSTNumber]        NVARCHAR (16)   NULL,
    [PANNumber]        NVARCHAR (16)   NULL,
    [ContactPerson]    NVARCHAR (64)   NULL,
    [Email]            NVARCHAR (64)   NULL,
    [Mobile]           NVARCHAR (16)   NULL,
    [IsRetailCustomer] BIT             NULL,
    [IsActive]         BIT             NULL,
    [TransactionDate]  DATETIME        NULL,
    [RequestId]        NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mCustomer] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mCustomerType]...';


GO
CREATE TABLE [SingleIB].[mCustomerType] (
    [CustomerTypeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [CustomerTypeName] NVARCHAR (128) NULL,
    [Description]      NVARCHAR (256) NULL,
    [IsActive]         BIT            NULL,
    [TransactionDate]  DATETIME       NULL,
    [RequestId]        NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mCustomerType] PRIMARY KEY CLUSTERED ([CustomerTypeId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mGRNDetail]...';


GO
CREATE TABLE [SingleIB].[mGRNDetail] (
    [GRNDetailId]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [GRNId]                 BIGINT          NULL,
    [GRNNumber]             NVARCHAR (16)   NULL,
    [PurchaseOrderDetailId] BIGINT          NULL,
    [ProductId]             BIGINT          NULL,
    [ReceivedQuantity]      DECIMAL (18, 5) NULL,
    [IsPerishable]          BIT             NULL,
    [ExpiryDate]            DATETIME        NULL,
    [BatchNo]               NVARCHAR (256)  NULL,
    [Description]           NVARCHAR (256)  NULL,
    [IsActive]              BIT             NULL,
    [TransactionDate]       DATETIME        NULL,
    [RequestId]             NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mGRNDetail] PRIMARY KEY CLUSTERED ([GRNDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mGRNHeader]...';


GO
CREATE TABLE [SingleIB].[mGRNHeader] (
    [GRNId]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [GRNDate]             DATETIME       NULL,
    [GRNNumber]           NVARCHAR (16)  NULL,
    [IsDirect]            BIT            NULL,
    [IsFullReceived]      BIT            NULL,
    [PurchaseOrderId]     BIGINT         NULL,
    [PurchaseOrderNumber] NVARCHAR (16)  NULL,
    [VendorDONumber]      NVARCHAR (64)  NULL,
    [VendorInvoiceNumber] NVARCHAR (64)  NULL,
    [VendorInvoiceDate]   DATETIME       NULL,
    [WareHouseId]         BIGINT         NULL,
    [Remarks]             NVARCHAR (512) NULL,
    [IsActive]            BIT            NULL,
    [TransactionDate]     DATETIME       NULL,
    [RequestId]           NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mGRNHeader] PRIMARY KEY CLUSTERED ([GRNId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mInvoiceDetail]...';


GO
CREATE TABLE [SingleIB].[mInvoiceDetail] (
    [InvoiceDetailId]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [InvoiceId]          BIGINT          NULL,
    [InvoiceNumber]      NVARCHAR (16)   NULL,
    [SalesOrderDetailId] BIGINT          NULL,
    [ProductId]          BIGINT          NULL,
    [HSNSAC]             NVARCHAR (8)    NULL,
    [Price]              DECIMAL (18, 5) NULL,
    [InvoicedQuantity]   DECIMAL (18, 5) NULL,
    [SubTotal]           DECIMAL (18, 5) NULL,
    [TaxPercentage]      DECIMAL (18, 5) NULL,
    [CGSTAmount]         DECIMAL (18, 5) NULL,
    [SGSTAmount]         DECIMAL (18, 5) NULL,
    [IGSTAmount]         DECIMAL (18, 5) NULL,
    [Total]              DECIMAL (18, 5) NULL,
    [Description]        NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [TransactionDate]    DATETIME        NULL,
    [RequestId]          NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mInvoiceDetail] PRIMARY KEY CLUSTERED ([InvoiceDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mInvoiceHeader]...';


GO
CREATE TABLE [SingleIB].[mInvoiceHeader] (
    [InvoiceId]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [InvoiceTypeId]        BIGINT          NULL,
    [InvoiceDate]          DATETIME        NULL,
    [InvoiceDueDate]       DATETIME        NULL,
    [InvoiceNumber]        NVARCHAR (16)   NULL,
    [IsFullInvoiced]       BIT             NULL,
    [SalesOrderId]         BIGINT          NULL,
    [CustomerId]           BIGINT          NULL,
    [IsGstApplicable]      BIT             NULL,
    [GSTNumber]            NVARCHAR (16)   NULL,
    [GSTStateCode]         NVARCHAR (2)    NULL,
    [GSTType]              SMALLINT        NULL,
    [InvoicedAmount]       DECIMAL (18, 5) NULL,
    [TaxAmount]            DECIMAL (18, 5) NULL,
    [TotalAmount]          DECIMAL (18, 5) NULL,
    [VehicleNumber]        NVARCHAR (16)   NULL,
    [HandOverPerson]       NVARCHAR (64)   NULL,
    [HandOverPersonMobile] NVARCHAR (16)   NULL,
    [Remarks]              NVARCHAR (512)  NULL,
    [IsActive]             BIT             NULL,
    [TransactionDate]      DATETIME        NULL,
    [RequestId]            NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mInvoiceHeader] PRIMARY KEY CLUSTERED ([InvoiceId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mInvoiceType]...';


GO
CREATE TABLE [SingleIB].[mInvoiceType] (
    [InvoiceTypeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [InvoiceTypeName] NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mInvoiceType] PRIMARY KEY CLUSTERED ([InvoiceTypeId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mPaymentInDetail]...';


GO
CREATE TABLE [SingleIB].[mPaymentInDetail] (
    [PaymentInDetailId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [PaymentInId]       BIGINT          NULL,
    [InvoiceId]         BIGINT          NULL,
    [InvoiceNumber]     NVARCHAR (16)   NULL,
    [Amount]            DECIMAL (18, 5) NULL,
    [IsFullReceived]    BIT             NULL,
    [PaymentDate]       DATETIME        NULL,
    [Description]       NVARCHAR (256)  NULL,
    [IsActive]          BIT             NULL,
    [TransactionDate]   DATETIME        NULL,
    [RequestId]         NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mPaymentInDetail] PRIMARY KEY CLUSTERED ([PaymentInDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mPaymentInHeader]...';


GO
CREATE TABLE [SingleIB].[mPaymentInHeader] (
    [PaymentInId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [CustomerId]      BIGINT          NULL,
    [CompanyBankId]   BIGINT          NULL,
    [IsTDSApplicable] BIT             NULL,
    [TDSPercentage]   DECIMAL (18, 5) NULL,
    [TDSAmount]       DECIMAL (18, 5) NULL,
    [ReceiveAmount]   DECIMAL (18, 5) NULL,
    [TotalAmount]     DECIMAL (18, 5) NULL,
    [PaymentDate]     DATETIME        NULL,
    [PaymentMode]     SMALLINT        NULL,
    [ReferenceNo]     NVARCHAR (64)   NULL,
    [ChequeNo]        NVARCHAR (8)    NULL,
    [ChequeDate]      DATETIME        NULL,
    [ChequeIFSC]      NVARCHAR (16)   NULL,
    [Remarks]         NVARCHAR (512)  NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mPaymentInHeader] PRIMARY KEY CLUSTERED ([PaymentInId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mPaymentOutDetail]...';


GO
CREATE TABLE [SingleIB].[mPaymentOutDetail] (
    [PaymentOutDetailId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [PaymentOutId]       BIGINT          NULL,
    [BillId]             BIGINT          NULL,
    [BillNumber]         NVARCHAR (16)   NULL,
    [Amount]             DECIMAL (18, 5) NULL,
    [IsFullPaid]         BIT             NULL,
    [PaymentDate]        DATETIME        NULL,
    [Description]        NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [TransactionDate]    DATETIME        NULL,
    [RequestId]          NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mPaymentOutDetail] PRIMARY KEY CLUSTERED ([PaymentOutDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mPaymentOutHeader]...';


GO
CREATE TABLE [SingleIB].[mPaymentOutHeader] (
    [PaymentOutId]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [VendorId]        BIGINT          NULL,
    [VendorBankId]    BIGINT          NULL,
    [IsTDSApplicable] BIT             NULL,
    [TDSPercentage]   DECIMAL (18, 5) NULL,
    [TDSAmount]       DECIMAL (18, 5) NULL,
    [PayAmount]       DECIMAL (18, 5) NULL,
    [TotalAmount]     DECIMAL (18, 5) NULL,
    [PaymentDate]     DATETIME        NULL,
    [PaymentMode]     SMALLINT        NULL,
    [ReferenceNo]     NVARCHAR (64)   NULL,
    [ChequeNo]        NVARCHAR (8)    NULL,
    [ChequeDate]      DATETIME        NULL,
    [ChequeIFSC]      NVARCHAR (16)   NULL,
    [Remarks]         NVARCHAR (512)  NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mPaymentOut] PRIMARY KEY CLUSTERED ([PaymentOutId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mPaymentType]...';


GO
CREATE TABLE [SingleIB].[mPaymentType] (
    [PaymentTypeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [PaymentTypeName] NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mPaymentType] PRIMARY KEY CLUSTERED ([PaymentTypeId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mProduct]...';


GO
CREATE TABLE [SingleIB].[mProduct] (
    [ProductId]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [ProductTypeId]       INT             NULL,
    [ProductInOut]        SMALLINT        NULL,
    [Brand]               NVARCHAR (256)  NULL,
    [ProductName]         NVARCHAR (512)  NULL,
    [ProductCode]         NVARCHAR (256)  NULL,
    [IsGoods]             BIT             NULL,
    [SKU]                 NVARCHAR (128)  NULL,
    [PurchasePrice]       DECIMAL (18, 5) NULL,
    [SalePrice]           DECIMAL (18, 5) NULL,
    [HSNSAC]              NVARCHAR (8)    NULL,
    [TaxPercentage]       DECIMAL (18, 5) NULL,
    [Description]         NVARCHAR (1024) NULL,
    [IsPerishableProduct] BIT             NULL,
    [HasExpirationDate]   BIT             NULL,
    [HasBatchNo]          BIT             NULL,
    [ProductImageUrl]     NVARCHAR (256)  NULL,
    [UnitOfMeasureId]     BIGINT          NULL,
    [UnitOfMeasureName]   NVARCHAR (8)    NULL,
    [IsActive]            BIT             NULL,
    [TransactionDate]     DATETIME        NULL,
    [RequestId]           NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mProduct] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mProductType]...';


GO
CREATE TABLE [SingleIB].[mProductType] (
    [ProductTypeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [ProductTypeName] NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mProductType] PRIMARY KEY CLUSTERED ([ProductTypeId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mPurchaseOrderDetail]...';


GO
CREATE TABLE [SingleIB].[mPurchaseOrderDetail] (
    [PurchaseOrderDetailId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [PurchaseOrderId]       BIGINT          NULL,
    [PurchaseOrderNumber]   NVARCHAR (16)   NULL,
    [AmendmentNumber]       NVARCHAR (4)    NULL,
    [ProductId]             BIGINT          NULL,
    [HSNSAC]                NVARCHAR (8)    NULL,
    [Amount]                DECIMAL (18, 5) NULL,
    [DiscountPercentage]    DECIMAL (18, 5) NULL,
    [DiscountAmount]        DECIMAL (18, 5) NULL,
    [Price]                 DECIMAL (18, 5) NULL,
    [Quantity]              DECIMAL (18, 5) NULL,
    [SubTotal]              DECIMAL (18, 5) NULL,
    [TaxPercentage]         DECIMAL (18, 5) NULL,
    [TaxAmount]             DECIMAL (18, 5) NULL,
    [Total]                 DECIMAL (18, 5) NULL,
    [Description]           NVARCHAR (256)  NULL,
    [IsActive]              BIT             NULL,
    [TransactionDate]       DATETIME        NULL,
    [RequestId]             NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mPurchaseOrderDetail] PRIMARY KEY CLUSTERED ([PurchaseOrderDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mPurchaseOrderHeader]...';


GO
CREATE TABLE [SingleIB].[mPurchaseOrderHeader] (
    [PurchaseOrderId]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [PurchaseTypeId]           BIGINT          NULL,
    [BranchId]                 BIGINT          NULL,
    [VendorId]                 BIGINT          NULL,
    [IsGstAllowed]             BIT             NULL,
    [PurchaseOrderDate]        DATETIME        NULL,
    [PurchaseOrderNumber]      NVARCHAR (16)   NULL,
    [AmendmentNumber]          NVARCHAR (4)    NULL,
    [AmendmentDate]            DATETIME        NULL,
    [DeliveryDate]             DATETIME        NULL,
    [PurchaseOrderAmount]      DECIMAL (18, 5) NULL,
    [PurchaseOrderTaxAmount]   DECIMAL (18, 5) NULL,
    [PurchaseOrderTotalAmount] DECIMAL (18, 5) NULL,
    [TandC]                    NVARCHAR (512)  NULL,
    [Remarks]                  NVARCHAR (512)  NULL,
    [IsActive]                 BIT             NULL,
    [TransactionDate]          DATETIME        NULL,
    [RequestId]                NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mPurchaseOrderHeader] PRIMARY KEY CLUSTERED ([PurchaseOrderId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mPurchaseType]...';


GO
CREATE TABLE [SingleIB].[mPurchaseType] (
    [PurchaseTypeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [PurchaseTypeName] NVARCHAR (128) NULL,
    [Description]      NVARCHAR (256) NULL,
    [IsActive]         BIT            NULL,
    [TransactionDate]  DATETIME       NULL,
    [RequestId]        NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mPurchaseType] PRIMARY KEY CLUSTERED ([PurchaseTypeId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mQuotationDetail]...';


GO
CREATE TABLE [SingleIB].[mQuotationDetail] (
    [QuotationDetailId]  BIGINT          IDENTITY (1, 1) NOT NULL,
    [QuotationId]        BIGINT          NULL,
    [QuotationNumber]    NVARCHAR (16)   NULL,
    [ProductId]          BIGINT          NULL,
    [HSNSAC]             NVARCHAR (8)    NULL,
    [Amount]             DECIMAL (18, 5) NULL,
    [DiscountPercentage] DECIMAL (18, 5) NULL,
    [DiscountAmount]     DECIMAL (18, 5) NULL,
    [Price]              DECIMAL (18, 5) NULL,
    [Quantity]           DECIMAL (18, 5) NULL,
    [SubTotal]           DECIMAL (18, 5) NULL,
    [TaxPercentage]      DECIMAL (18, 5) NULL,
    [TaxAmount]          DECIMAL (18, 5) NULL,
    [Total]              DECIMAL (18, 5) NULL,
    [Description]        NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [TransactionDate]    DATETIME        NULL,
    [RequestId]          NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mQuotationDetail_1] PRIMARY KEY CLUSTERED ([QuotationDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mQuotationHeader]...';


GO
CREATE TABLE [SingleIB].[mQuotationHeader] (
    [QuotationId]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [CustomerId]           BIGINT          NULL,
    [IsGstAllowed]         BIT             NULL,
    [QuotationDate]        DATETIME        NULL,
    [QuotationNumber]      NVARCHAR (16)   NULL,
    [QuotationAmount]      DECIMAL (18, 5) NULL,
    [QuotationTaxAmount]   DECIMAL (18, 5) NULL,
    [QuotationTotalAmount] DECIMAL (18, 5) NULL,
    [TandC]                TEXT            NULL,
    [Remarks]              NVARCHAR (512)  NULL,
    [IsActive]             BIT             NULL,
    [TransactionDate]      DATETIME        NULL,
    [RequestId]            NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mQuotationHeader] PRIMARY KEY CLUSTERED ([QuotationId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mSalesOrderDetail]...';


GO
CREATE TABLE [SingleIB].[mSalesOrderDetail] (
    [SalesOrderDetailId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [SalesOrderId]       BIGINT          NULL,
    [SalesOrderNumber]   NVARCHAR (16)   NULL,
    [AmendmentNumber]    NVARCHAR (4)    NULL,
    [ProductId]          BIGINT          NULL,
    [HSNSAC]             NVARCHAR (8)    NULL,
    [Amount]             DECIMAL (18, 5) NULL,
    [DiscountPercentage] DECIMAL (18, 5) NULL,
    [DiscountAmount]     DECIMAL (18, 5) NULL,
    [Price]              DECIMAL (18, 5) NULL,
    [Quantity]           DECIMAL (18, 5) NULL,
    [SubTotal]           DECIMAL (18, 5) NULL,
    [TaxPercentage]      DECIMAL (18, 5) NULL,
    [TaxAmount]          DECIMAL (18, 5) NULL,
    [Total]              DECIMAL (18, 5) NULL,
    [Description]        NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [TransactionDate]    DATETIME        NULL,
    [RequestId]          NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mSalesOrderDetail] PRIMARY KEY CLUSTERED ([SalesOrderDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mSalesOrderHeader]...';


GO
CREATE TABLE [SingleIB].[mSalesOrderHeader] (
    [SalesOrderId]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [QuotationId]           BIGINT          NULL,
    [SalesTypeId]           BIGINT          NULL,
    [BranchId]              BIGINT          NULL,
    [CustomerId]            BIGINT          NULL,
    [IsGstAllowed]          BIT             NULL,
    [SalesOrderDate]        DATETIME        NULL,
    [SalesOrderNumber]      NVARCHAR (16)   NULL,
    [AmendmentNumber]       NVARCHAR (4)    NULL,
    [AmendmentDate]         DATETIME        NULL,
    [CustomerPONumber]      NVARCHAR (128)  NULL,
    [CustomerPODate]        DATETIME        NULL,
    [DeliveryDate]          DATETIME        NULL,
    [SalesOrderAmount]      DECIMAL (18, 5) NULL,
    [SalesOrderTaxAmount]   DECIMAL (18, 5) NULL,
    [SalesOrderTotalAmount] DECIMAL (18, 5) NULL,
    [TandC]                 NVARCHAR (512)  NULL,
    [Remarks]               NVARCHAR (512)  NULL,
    [IsActive]              BIT             NULL,
    [TransactionDate]       DATETIME        NULL,
    [RequestId]             NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mSalesOrderHeader] PRIMARY KEY CLUSTERED ([SalesOrderId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mSalesType]...';


GO
CREATE TABLE [SingleIB].[mSalesType] (
    [SalesTypeId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [SalesTypeName]   NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mSalesType] PRIMARY KEY CLUSTERED ([SalesTypeId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mShipmentDetail]...';


GO
CREATE TABLE [SingleIB].[mShipmentDetail] (
    [ShipmentDetailId]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [ShipmentId]         BIGINT          NULL,
    [ShipmentNumber]     NVARCHAR (16)   NULL,
    [SalesOrderDetailId] BIGINT          NULL,
    [ProductId]          BIGINT          NULL,
    [ShippedQuantity]    DECIMAL (18, 5) NULL,
    [StockDetailId]      BIGINT          NULL,
    [IsPerishable]       BIT             NULL,
    [ExpiryDate]         DATETIME        NULL,
    [BatchNo]            NVARCHAR (256)  NULL,
    [Description]        NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [TransactionDate]    DATETIME        NULL,
    [RequestId]          NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mShipmentDetail] PRIMARY KEY CLUSTERED ([ShipmentDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mShipmentHeader]...';


GO
CREATE TABLE [SingleIB].[mShipmentHeader] (
    [ShipmentId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [ShipmentTypeId]       BIGINT         NULL,
    [ShipmentDate]         DATETIME       NULL,
    [ShipmentNumber]       NVARCHAR (16)  NULL,
    [IsDirect]             BIT            NULL,
    [IsFullShipped]        BIT            NULL,
    [SalesOrderId]         BIGINT         NULL,
    [SalesOrderNumber]     NVARCHAR (16)  NULL,
    [WareHouseId]          BIGINT         NULL,
    [VehicleNumber]        NVARCHAR (16)  NULL,
    [HandOverPerson]       NVARCHAR (64)  NULL,
    [HandOverPersonMobile] NVARCHAR (16)  NULL,
    [Remarks]              NVARCHAR (512) NULL,
    [IsActive]             BIT            NULL,
    [TransactionDate]      DATETIME       NULL,
    [RequestId]            NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mShipmentHeader] PRIMARY KEY CLUSTERED ([ShipmentId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mShipmentType]...';


GO
CREATE TABLE [SingleIB].[mShipmentType] (
    [ShipmentTypeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [ShipmentTypeName] NVARCHAR (128) NULL,
    [Description]      NVARCHAR (256) NULL,
    [IsActive]         BIT            NULL,
    [TransactionDate]  DATETIME       NULL,
    [RequestId]        NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mShipmentType] PRIMARY KEY CLUSTERED ([ShipmentTypeId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mUnitOfMeasure]...';


GO
CREATE TABLE [SingleIB].[mUnitOfMeasure] (
    [UnitOfMeasureId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [UnitOfMeasureName] NVARCHAR (8)   NULL,
    [Description]       NVARCHAR (256) NULL,
    [IsActive]          BIT            NULL,
    [TransactionDate]   DATETIME       NULL,
    [RequestId]         NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mUnitOfMeasure] PRIMARY KEY CLUSTERED ([UnitOfMeasureId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mVendor]...';


GO
CREATE TABLE [SingleIB].[mVendor] (
    [VendorId]        BIGINT          IDENTITY (1, 1) NOT NULL,
    [VendorTypeId]    BIGINT          NULL,
    [CompanyName]     NVARCHAR (1204) NULL,
    [CompanyType]     NVARCHAR (64)   NULL,
    [StateCode]       NVARCHAR (2)    NULL,
    [IsGSTRegistered] BIT             NULL,
    [GSTStateCode]    NVARCHAR (2)    NULL,
    [GSTNumber]       NVARCHAR (16)   NULL,
    [PANNumber]       NVARCHAR (16)   NULL,
    [ContactPerson]   NVARCHAR (64)   NULL,
    [Email]           NVARCHAR (64)   NULL,
    [Mobile]          NVARCHAR (16)   NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mVendor] PRIMARY KEY CLUSTERED ([VendorId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mVendorBank]...';


GO
CREATE TABLE [SingleIB].[mVendorBank] (
    [VendorBankId]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [VendorId]        BIGINT          NULL,
    [AccountName]     NVARCHAR (1024) NULL,
    [AccountNumber]   NVARCHAR (64)   NULL,
    [BankName]        NVARCHAR (128)  NULL,
    [IFSC]            NVARCHAR (16)   NULL,
    [MICR]            NVARCHAR (16)   NULL,
    [BranchName]      NVARCHAR (64)   NULL,
    [BranchAddress]   NVARCHAR (128)  NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mVendorBank] PRIMARY KEY CLUSTERED ([VendorBankId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mVendorType]...';


GO
CREATE TABLE [SingleIB].[mVendorType] (
    [VendorTypeId]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [VendorTypeName]  NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mVendorType] PRIMARY KEY CLUSTERED ([VendorTypeId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[mWareHouse]...';


GO
CREATE TABLE [SingleIB].[mWareHouse] (
    [WareHouseId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [BranchId]        BIGINT         NULL,
    [WareHouseName]   NVARCHAR (64)  NULL,
    [IsRetailCounter] BIT            NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mWareHouse] PRIMARY KEY CLUSTERED ([WareHouseId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[sStockAdjustReason]...';


GO
CREATE TABLE [SingleIB].[sStockAdjustReason] (
    [AdjustReasonId]  BIGINT        IDENTITY (101, 1) NOT NULL,
    [AdjustReason]    NVARCHAR (64) NULL,
    [IsDefault]       BIT           NULL,
    [IsActive]        BIT           NULL,
    [TransactionDate] DATETIME      NULL,
    [RequestId]       NVARCHAR (64) NULL,
    CONSTRAINT [PK_sStockAdjustReason] PRIMARY KEY CLUSTERED ([AdjustReasonId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[tAmendmentTrack]...';


GO
CREATE TABLE [SingleIB].[tAmendmentTrack] (
    [AmendmentTrackId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [AmendmentOf]        NVARCHAR (4)  NULL,
    [AmendmentObjectId]  NVARCHAR (16) NULL,
    [OldAmendmentNumber] NVARCHAR (4)  NULL,
    [AmendmentNumber]    NVARCHAR (4)  NULL,
    [HeaderJson]         TEXT          NULL,
    [DetailJson]         TEXT          NULL,
    [IsActive]           BIT           NULL,
    [TransactionDate]    DATETIME      NULL,
    [RequestId]          NVARCHAR (64) NULL,
    CONSTRAINT [PK_mPurchaseOrderAmendment] PRIMARY KEY CLUSTERED ([AmendmentTrackId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[tPassword]...';


GO
CREATE TABLE [SingleIB].[tPassword] (
    [PasswordId]      INT             IDENTITY (1, 1) NOT NULL,
    [UserId]          INT             NULL,
    [PasswordHash]    NVARCHAR (2048) NULL,
    [PasswordSalt]    NVARCHAR (2048) NULL,
    [IsActive]        BIT             NULL,
    [IsChangeable]    BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       VARCHAR (32)    NULL,
    CONSTRAINT [PK_tPassword] PRIMARY KEY CLUSTERED ([PasswordId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[tSmsTrack]...';


GO
CREATE TABLE [SingleIB].[tSmsTrack] (
    [SmsSendId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]            BIGINT         NULL,
    [UserName]          NVARCHAR (64)  NULL,
    [ComponentIdentity] NVARCHAR (64)  NULL,
    [SendTime]          DATETIME       NULL,
    [UrlLink]           NVARCHAR (128) NULL,
    [SmsContent]        NVARCHAR (512) NULL,
    [MobileNo]          NVARCHAR (16)  NULL,
    [IsMobileNoChanged] BIT            NULL,
    [IsResend]          BIT            NULL,
    [SmsFor]            VARCHAR (64)   NULL,
    [IsActive]          BIT            NULL,
    [TransactionDate]   DATETIME       NULL,
    [RequestId]         NVARCHAR (64)  NULL,
    CONSTRAINT [PK_tSmsTrack] PRIMARY KEY CLUSTERED ([SmsSendId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[tStock]...';


GO
CREATE TABLE [SingleIB].[tStock] (
    [StockId]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [ProductId]         BIGINT          NULL,
    [WareHouseId]       BIGINT          NULL,
    [CurrentStock]      DECIMAL (18, 5) NULL,
    [Remarks]           NVARCHAR (512)  NULL,
    [GRNOrShipmentId]   BIGINT          NULL,
    [GRNOrShipmentDate] DATETIME        NULL,
    [StockType]         SMALLINT        NULL,
    [IsActive]          BIT             NULL,
    [TransactionDate]   DATETIME        NULL,
    [RequestId]         NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tStock] PRIMARY KEY CLUSTERED ([StockId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[tStockAdjustDetail]...';


GO
CREATE TABLE [SingleIB].[tStockAdjustDetail] (
    [StockAdjustDetailId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [StockAdjustId]       BIGINT          NULL,
    [StockAdjustNumber]   NVARCHAR (16)   NULL,
    [ProductId]           BIGINT          NULL,
    [IsStockIncrease]     BIT             NULL,
    [AvailableQuantity]   DECIMAL (18, 5) NULL,
    [AdjustQuantity]      DECIMAL (18, 5) NULL,
    [StockDetailId]       BIGINT          NULL,
    [GRNDate]             DATETIME        NULL,
    [IsPerishable]        BIT             NULL,
    [ExpiryDate]          DATETIME        NULL,
    [BatchNo]             NVARCHAR (256)  NULL,
    [Description]         NVARCHAR (256)  NULL,
    [IsActive]            BIT             NULL,
    [TransactionDate]     DATETIME        NULL,
    [RequestId]           NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tStockAdjustDetail] PRIMARY KEY CLUSTERED ([StockAdjustDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[tStockAdjustHeader]...';


GO
CREATE TABLE [SingleIB].[tStockAdjustHeader] (
    [StockAdjustId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [WareHouseId]       BIGINT         NULL,
    [StockAdjustNumber] NVARCHAR (16)  NULL,
    [StockAdjustDate]   DATETIME       NULL,
    [IsStockIncrease]   BIT            NULL,
    [AdjustReasonId]    BIGINT         NULL,
    [AdjustReason]      NVARCHAR (64)  NULL,
    [Remarks]           NVARCHAR (512) NULL,
    [IsActive]          BIT            NULL,
    [TransactionDate]   DATETIME       NULL,
    [RequestId]         NVARCHAR (64)  NULL,
    CONSTRAINT [PK_tStockAdjustHeader] PRIMARY KEY CLUSTERED ([StockAdjustId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[tStockDetail]...';


GO
CREATE TABLE [SingleIB].[tStockDetail] (
    [StockDetailId]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [StockId]         BIGINT          NULL,
    [ProductId]       BIGINT          NULL,
    [WareHouseId]     BIGINT          NULL,
    [Stock]           DECIMAL (18, 5) NULL,
    [IsPerishable]    BIT             NULL,
    [ExpiryDate]      DATETIME        NULL,
    [BatchNo]         NVARCHAR (256)  NULL,
    [Remarks]         NVARCHAR (512)  NULL,
    [GRNDate]         DATETIME        NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tStockDetail] PRIMARY KEY CLUSTERED ([StockDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[tStockTransferDetail]...';


GO
CREATE TABLE [SingleIB].[tStockTransferDetail] (
    [StockTransferDetailId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [StockTransferId]       BIGINT          NULL,
    [StockTransferNumber]   NVARCHAR (16)   NULL,
    [ProductId]             BIGINT          NULL,
    [AvailableQuantity]     DECIMAL (18, 5) NULL,
    [TransferQuantity]      DECIMAL (18, 5) NULL,
    [StockDetailId]         BIGINT          NULL,
    [IsPerishable]          BIT             NULL,
    [ExpiryDate]            DATETIME        NULL,
    [BatchNo]               NVARCHAR (256)  NULL,
    [Description]           NVARCHAR (256)  NULL,
    [IsActive]              BIT             NULL,
    [TransactionDate]       DATETIME        NULL,
    [RequestId]             NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tStockTransferDetail] PRIMARY KEY CLUSTERED ([StockTransferDetailId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[tStockTransferHeader]...';


GO
CREATE TABLE [SingleIB].[tStockTransferHeader] (
    [StockTransferId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [StockTransferNumber] NVARCHAR (16)  NULL,
    [StockTransferDate]   DATETIME       NULL,
    [FromWareHouseId]     BIGINT         NULL,
    [ToWareHouseId]       BIGINT         NULL,
    [Remarks]             NVARCHAR (512) NULL,
    [IsActive]            BIT            NULL,
    [TransactionDate]     DATETIME       NULL,
    [RequestId]           NVARCHAR (64)  NULL,
    CONSTRAINT [PK_tStockTransferHeader] PRIMARY KEY CLUSTERED ([StockTransferId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[tTDSPay]...';


GO
CREATE TABLE [SingleIB].[tTDSPay] (
    [TDSPayId]        BIGINT          IDENTITY (1, 1) NOT NULL,
    [PaymentOutId]    BIGINT          NULL,
    [VendorId]        BIGINT          NULL,
    [PANNumber]       NVARCHAR (16)   NULL,
    [TDSAmount]       DECIMAL (18, 5) NULL,
    [IsPaid]          BIT             NULL,
    [PayDate]         DATETIME        NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tTDSPay] PRIMARY KEY CLUSTERED ([TDSPayId] ASC)
);


GO
PRINT N'Creating Table [SingleIB].[tTDSReceive]...';


GO
CREATE TABLE [SingleIB].[tTDSReceive] (
    [TDSReceiveId]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [PaymentInId]     BIGINT          NULL,
    [CustomerId]      BIGINT          NULL,
    [PANNumber]       NVARCHAR (16)   NULL,
    [TDSAmount]       DECIMAL (18, 5) NULL,
    [IsReceived]      BIT             NULL,
    [ReceivedDate]    DATETIME        NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tTDSReceive] PRIMARY KEY CLUSTERED ([TDSReceiveId] ASC)
);


GO
PRINT N'Creating Function [SingleIB].[IsFullBilled]...';


GO
CREATE FUNCTION [SingleIB].[IsFullBilled]
(
	@PurchaseOrderId bigint
)  
RETURNS bit   
AS  
BEGIN 
	DECLARE @Flag bit;
	SET @Flag = 0;
	IF((SELECT SUM(Quantity) from mPurchaseOrderDetail where PurchaseOrderId = @PurchaseOrderId) <= 
		(SELECT ISNULL(SUM(BilledQuantity), 0) from mBillDetail where PurchaseOrderDetailId IN 
			(SELECT PurchaseOrderDetailId from mPurchaseOrderDetail where PurchaseOrderId = @PurchaseOrderId)))
	BEGIN
		SET @Flag = 1;
	END
	RETURN @Flag;
END;
GO
PRINT N'Creating Function [SingleIB].[IsFullInvoiced]...';


GO
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
GO
PRINT N'Creating Function [SingleIB].[IsFullReceived]...';


GO
CREATE FUNCTION [SingleIB].[IsFullReceived]
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
GO
PRINT N'Creating Function [SingleIB].[IsFullShipped]...';


GO
CREATE FUNCTION [SingleIB].[IsFullShipped]
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
GO
PRINT N'Creating Function [SingleIB].[ISTNow]...';


GO
CREATE FUNCTION [SingleIB].[ISTNow]()  
RETURNS DATETIME   
AS   
-- Returns the stock level for the product.  
BEGIN 
    RETURN DATEADD(MINUTE, 330, SYSUTCDATETIME());
END;
GO
PRINT N'Creating Procedure [SingleIB].[spDsMaster]...';


GO
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetBill]...';


GO



CREATE PROCEDURE [SingleIB].[spGetBill]
(
	@PurchaseOrderId	bigint = 0,
	@VendorId			bigint = 0,
	@BillId				bigint = 0,

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
				SELECT bil.*, vod.CompanyName,
				poh.PurchaseOrderNumber,
				CASE 
					WHEN poh.PurchaseOrderNumber IS NULL THEN (SELECT grh.GRNNumber FROM SingleIB.mGRNHeader as grh 
							WHERE grh.PurchaseOrderId = bil.PurchaseOrderId
								AND grh.IsDirect = 1)
					ELSE NULL
				END AS GRNNumber,
				CAST(
					(CASE 
						WHEN EXISTS (SELECT * FROM [SingleIB].[mPaymentOutDetail] AS pod WHERE  bil. BillId = pod.BillId
									AND pod.IsFullPaid = 1)
							THEN 1
						WHEN EXISTS (SELECT * FROM [SingleIB].[mPaymentOutDetail] AS pod WHERE  bil. BillId = pod.BillId
								AND pod.IsFullPaid <> 1)
						THEN 0
						ELSE null
					END) AS BIT) AS PaymentStatus
				FROM SingleIB.mBillHeader as bil
				INNER JOIN SingleIB.mVendor as vod
					ON bil.VendorId = vod.VendorId
				INNER JOIN SingleIB.mPurchaseOrderHeader as poh
					ON bil.PurchaseOrderId = poh.PurchaseOrderId
				ORDER BY bil.BillDate DESC, bil.BillId DESC;
			END
			ELSE IF (@QueryType ='SINGLE')
			BEGIN
				SELECT bil.*, vod.CompanyName,
					poh.PurchaseOrderNumber,
				CASE 
					WHEN poh.PurchaseOrderNumber IS NULL THEN (SELECT grh.GRNNumber FROM SingleIB.mGRNHeader as grh 
							WHERE grh.PurchaseOrderId = bil.PurchaseOrderId
								AND grh.IsDirect = 1)
					ELSE NULL
				END AS GRNNumber,
				CAST(
					(CASE 
						WHEN EXISTS (SELECT * FROM [SingleIB].[mPaymentOutDetail] AS pod WHERE  bil. BillId = pod.BillId
									AND pod.IsFullPaid = 1)
							THEN 1
						WHEN EXISTS (SELECT * FROM [SingleIB].[mPaymentOutDetail] AS pod WHERE  bil. BillId = pod.BillId
								AND pod.IsFullPaid <> 1)
						THEN 0
						ELSE null
					END) AS BIT) AS PaymentStatus
				FROM SingleIB.mBillHeader as bil
				INNER JOIN SingleIB.mVendor as vod
					ON bil.VendorId = vod.VendorId
				INNER JOIN SingleIB.mPurchaseOrderHeader as poh
					ON bil.PurchaseOrderId = poh.PurchaseOrderId
				WHERE bil.BillId = @BillId
				ORDER BY bil.BillDate DESC, bil.BillId DESC;
			END
			ELSE IF (@QueryType = 'COMULTATIVE')
			BEGIN
				SELECT
					0 AS BillDetailId,
					0 AS BillId,
					NULL AS BillNumber,
					billd.PurchaseOrderDetailId,
					0 AS ProductId,
					SUM(billd.BilledQuantity) AS BilledQuantity,
					NULL AS Description,
					CAST(1 AS BIT) AS IsActive,
					NULL AS TransactionDate,
					NULL AS RequestId
				FROM SingleIB.mBillDetail as billd
				WHERE billd.BillId IN 
						(SELECT billh.BillId
						FROM SingleIB.mBillHeader as billh
						WHERE billh.PurchaseOrderId = @PurchaseOrderId)
				GROUP BY billd.PurchaseOrderDetailId;
			END
			ELSE IF (@QueryType = 'ENTRY')
			BEGIN
				-- PO
				SELECT poh.PurchaseOrderId, 
					poh.PurchaseOrderNumber
				FROM SingleIB.mPurchaseOrderHeader poh
				WHERE poh.PurchaseOrderNumber IS NOT NULL
					AND poh.IsActive = 1
					AND poh.PurchaseOrderId NOT IN (SELECT bilh.PurchaseOrderId
						FROM SingleIB.mBillHeader AS bilh
						WHERE bilh.IsFullBilled = 1)
				UNION
				-- GRN
				SELECT grnh.PurchaseOrderId, 
					grnh.GRNNumber AS PurchaseOrderNumber
				FROM SingleIB.mGRNHeader grnh
				WHERE grnh.IsDirect = 1
					AND grnh.PurchaseOrderId NOT IN (SELECT bilh.PurchaseOrderId
						FROM SingleIB.mBillHeader AS bilh
						WHERE bilh.IsFullBilled = 1);
			END
			ELSE IF (@QueryType = 'ENTRYSINGLE')
			BEGIN
				IF NOT EXISTS (
					SELECT bilh.PurchaseOrderId
						FROM SingleIB.mBillHeader AS bilh
						WHERE bilh.IsFullBilled = 1 
							AND bilh.PurchaseOrderId = @PurchaseOrderId)
				BEGIN
					SELECT @PurchaseOrderId as PurchaseOrderId
				END
			END
			ELSE IF (@QueryType = 'PAYMENT')
			BEGIN
				SELECT blh.*
				FROM SingleIB.mBillHeader as blh
				WHERE blh.VendorId = @VendorId
					AND blh.BillId NOT IN 
							(SELECT outdtl.BillId
							FROM SingleIB.mPaymentOutDetail as outdtl
							WHERE outdtl.IsFullPaid = 1);
			END
			ELSE IF (@QueryType = 'DETAIL')
			BEGIN
				SELECT bil.*, --vod.*,
				poh.PurchaseOrderNumber,
				CASE 
					WHEN poh.PurchaseOrderNumber IS NULL THEN (SELECT grh.GRNNumber FROM SingleIB.mGRNHeader as grh 
							WHERE grh.PurchaseOrderId = bil.PurchaseOrderId
								AND grh.IsDirect = 1)
					ELSE NULL
				END AS GRNNumber
				FROM SingleIB.mBillHeader as bil
				--INNER JOIN SingleIB.mVendor as vod
				--	ON bil.VendorId = vod.VendorId
				INNER JOIN SingleIB.mPurchaseOrderHeader as poh
					ON bil.PurchaseOrderId = poh.PurchaseOrderId
				WHERE bil.BillId = @BillId;

				SELECT bld.*
				FROM mBillDetail as bld
				WHERE bld.BillId = @BillId;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetBillType]...';


GO


CREATE PROCEDURE [SingleIB].[spGetBillType]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mBillType;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetBranch]...';


GO
CREATE PROCEDURE [SingleIB].[spGetBranch]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mBranch
				ORDER BY IsActive DESC, BranchId DESC;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetCompanyBank]...';


GO
CREATE PROCEDURE [SingleIB].[spGetCompanyBank]
(
	@QueryType		varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mCompanyBank
				ORDER BY IsActive Desc;
			END
			ELSE IF (@QueryType ='PRINTABLE')
			BEGIN
				SELECT *
				FROM SingleIB.mCompanyBank
				WHERE IsActive = 1
					AND IsPrintable = 1;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetCustomer]...';


GO

CREATE PROCEDURE [SingleIB].[spGetCustomer]
(
	@CustomerId		bigint = 0,

	@QueryType		varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mCustomer c
					LEFT JOIN SingleIB.mCustomerType as ct
				ON c.CustomerTypeId = ct.CustomerTypeId
				ORDER BY c.IsActive DESC, c.CustomerId DESC;
			END
			ELSE IF (@QueryType ='SINGLE')
			BEGIN
				SELECT *
				FROM SingleIB.mCustomer c
					LEFT JOIN SingleIB.mCustomerType as ct
				ON c.CustomerTypeId = ct.CustomerTypeId
				WHERE c.CustomerId = @CustomerId;
			END
			ELSE IF (@QueryType ='NONRETAILALL')
			BEGIN
				SELECT *
				FROM SingleIB.mCustomer c
					LEFT JOIN SingleIB.mCustomerType as ct
				ON c.CustomerTypeId = ct.CustomerTypeId
				WHERE c.IsRetailCustomer = 0					
				ORDER BY c.IsActive DESC, c.CustomerId DESC;
			END
			ELSE IF (@QueryType ='RETAILALL')
			BEGIN
				SELECT *
				FROM SingleIB.mCustomer c
					LEFT JOIN SingleIB.mCustomerType as ct
				ON c.CustomerTypeId = ct.CustomerTypeId
				WHERE c.IsRetailCustomer = 1
				ORDER BY c.IsActive DESC, c.CustomerId DESC;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetCustomerType]...';


GO

CREATE PROCEDURE [SingleIB].[spGetCustomerType]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mCustomerType;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetGRN]...';


GO
CREATE PROCEDURE [SingleIB].[spGetGRN]
(
	@PurchaseOrderId	bigint = 0,
	@GRNId				bigint = 0,

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
				SELECT grh.*, who.*,
				CAST(
					(CASE 
						WHEN grh.IsDirect = 1
							THEN 
							(CASE 
								WHEN EXISTS (SELECT * FROM [SingleIB].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = grh.PurchaseOrderId
											AND bil.IsFullBilled = 1)
									THEN 1
								WHEN EXISTS (SELECT * FROM [SingleIB].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = grh.PurchaseOrderId
										AND bil.IsFullBilled <> 1)
								THEN 0
								ELSE null
							END) 
						ELSE null
					END) 
				 AS BIT) AS BilledStatus
				FROM SingleIB.mGRNHeader as grh
					INNER JOIN SingleIB.mWareHouse as who
					ON grh.WareHouseId = who.WareHouseId
				ORDER BY grh.GRNDate DESC, grh.GRNId DESC;
			END
			ELSE IF (@QueryType = 'COMULTATIVE')
			BEGIN
				SELECT
					0 AS GRNDetailId,
					0 AS GRNId,
					NULL AS GRNNumber,
					grnd.PurchaseOrderDetailId,
					0 AS ProductId,	
					SUM(grnd.ReceivedQuantity) AS ReceivedQuantity,
					NULL AS Description,
					CAST(1 AS BIT) AS IsActive,
					NULL AS TransactionDate,
					NULL AS RequestId
				FROM SingleIB.mGRNDetail as grnd
				WHERE grnd.GRNId IN 
						(SELECT grnh.GRNId
						FROM SingleIB.mGRNHeader as grnh
						WHERE grnh.PurchaseOrderId = @PurchaseOrderId)
				GROUP BY grnd.PurchaseOrderDetailId;
			END
			ELSE IF (@QueryType ='ENTRY')
			BEGIN
				SELECT poh.*
				FROM SingleIB.mPurchaseOrderHeader poh
				WHERE poh.IsActive = 1
					AND poh.PurchaseOrderId NOT IN (SELECT PurchaseOrderId
						FROM SingleIB.mGRNHeader
						WHERE IsFullReceived = 1);
			END
			ELSE IF (@QueryType = 'ENTRYSINGLE')
			BEGIN
				IF NOT EXISTS (
					SELECT PurchaseOrderId
						FROM SingleIB.mGRNHeader
						WHERE IsFullReceived = 1 
							AND PurchaseOrderId = @PurchaseOrderId)
				BEGIN
					SELECT @PurchaseOrderId as PurchaseOrderId
				END
			END
			ELSE IF (@QueryType = 'DETAIL')
			BEGIN
				SELECT grh.*, who.*
				FROM SingleIB.mGRNHeader as grh
					INNER JOIN SingleIB.mWareHouse as who
				ON grh.WareHouseId = who.WareHouseId
				WHERE grh.GRNId = @GRNId;

				SELECT grd.*		
				FROM SingleIB.mGRNDetail as grd					
				WHERE grd.GRNId = @GRNId;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetInvoice]...';


GO



CREATE PROCEDURE [SingleIB].[spGetInvoice]
(
	@SalesOrderId		bigint = 0,
	@CustomerId			bigint = 0,
	@InvoiceId			bigint = 0,

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
				SELECT inv.*, cus.*,
					soh.SalesOrderNumber,
				CASE 
					WHEN soh.SalesOrderNumber IS NULL THEN (SELECT sph.ShipmentNumber FROM SingleIB.mShipmentHeader as sph 
							WHERE sph.SalesOrderId = inv.SalesOrderId
								AND sph.IsDirect = 1)
					ELSE NULL
				END AS ShipmentNumber,
				CAST(
					(CASE 
						WHEN EXISTS (SELECT * FROM [SingleIB].[mPaymentInDetail] AS pod WHERE  inv.InvoiceId = pod.InvoiceId
									AND pod.IsFullReceived = 1)
							THEN 1
						WHEN EXISTS (SELECT * FROM [SingleIB].[mPaymentInDetail] AS pod WHERE  inv.InvoiceId = pod.InvoiceId
								AND pod.IsFullReceived <> 1)
						THEN 0
						ELSE null
					END) AS BIT) AS PaymentStatus
				FROM SingleIB.mInvoiceHeader as inv
				INNER JOIN SingleIB.mCustomer as cus
					ON inv.CustomerId = cus.CustomerId
				INNER JOIN SingleIB.mSalesOrderHeader as soh
					ON inv.SalesOrderId = soh.SalesOrderId
				ORDER BY inv.InvoiceDate DESC, inv.InvoiceId DESC;
			END
			ELSE IF (@QueryType ='RETAIL')
			BEGIN
				SELECT inv.*, cus.*,
					soh.SalesOrderNumber,
				CASE 
					WHEN soh.SalesOrderNumber IS NULL THEN (SELECT sph.ShipmentNumber FROM SingleIB.mShipmentHeader as sph 
							WHERE sph.SalesOrderId = inv.SalesOrderId
								AND sph.IsDirect = 1)
					ELSE NULL
				END AS ShipmentNumber,
				CAST(
					(CASE 
						WHEN EXISTS (SELECT * FROM [SingleIB].[mPaymentInDetail] AS pod WHERE  inv.InvoiceId = pod.InvoiceId
									AND pod.IsFullReceived = 1)
							THEN 1
						WHEN EXISTS (SELECT * FROM [SingleIB].[mPaymentInDetail] AS pod WHERE  inv.InvoiceId = pod.InvoiceId
								AND pod.IsFullReceived <> 1)
						THEN 0
						ELSE null
					END) AS BIT) AS PaymentStatus
				FROM SingleIB.mInvoiceHeader as inv
				INNER JOIN SingleIB.mCustomer as cus
					ON inv.CustomerId = cus.CustomerId
				INNER JOIN SingleIB.mSalesOrderHeader as soh
					ON inv.SalesOrderId = soh.SalesOrderId
				WHERE cus.IsRetailCustomer = 1
				ORDER BY inv.InvoiceDate DESC, inv.InvoiceId DESC;
			END
			ELSE IF (@QueryType = 'COMULTATIVE')
			BEGIN
				SELECT
					0 AS InvoiceDetailId,
					0 AS InvoiceId,
					NULL AS InvoiceNumber,
					invd.SalesOrderDetailId,
					0 AS ProductId,
					SUM(invd.InvoicedQuantity) AS InvoicedQuantity,
					NULL AS Description,
					CAST(1 AS BIT) AS IsActive,
					NULL AS TransactionDate,
					NULL AS RequestId
				FROM SingleIB.mInvoiceDetail as invd
				WHERE invd.InvoiceId IN 
						(SELECT invh.InvoiceId
						FROM SingleIB.mInvoiceHeader as invh
						WHERE invh.SalesOrderId = @SalesOrderId)
				GROUP BY invd.SalesOrderDetailId;
			END
			ELSE IF (@QueryType = 'ENTRY')
			BEGIN
				-- SO
				SELECT soh.SalesOrderId, 
					soh.SalesOrderNumber
				FROM SingleIB.mSalesOrderHeader soh
				WHERE soh.SalesOrderNumber IS NOT NULL
					AND soh.IsActive = 1
					AND soh.SalesOrderId NOT IN (SELECT inv.SalesOrderId
						FROM SingleIB.mInvoiceHeader AS inv
						WHERE inv.IsFullInvoiced = 1)
				UNION
				-- Ship
				SELECT grnh.SalesOrderId, 
					grnh.ShipmentNumber AS SalesOrderNumber
				FROM SingleIB.mShipmentHeader grnh
				WHERE grnh.IsDirect = 1
					AND grnh.SalesOrderId NOT IN (SELECT inv.SalesOrderId
						FROM SingleIB.mInvoiceHeader AS inv
						WHERE inv.IsFullInvoiced = 1);
			END
			ELSE IF (@QueryType = 'PAYMENT')
			BEGIN
				SELECT invH.*
				FROM SingleIB.mInvoiceHeader as invH
				WHERE invH.CustomerId = @CustomerId
					AND invH.InvoiceId NOT IN (SELECT indtl.InvoiceId
						FROM SingleIB.mPaymentInDetail as indtl
						WHERE indtl.IsFullReceived = 1);
			END
			ELSE IF (@QueryType = 'DETAIL')
			BEGIN
				SELECT inv.*, --cus.*,
				poh.SalesOrderNumber,
				CASE 
					WHEN poh.SalesOrderNumber IS NULL THEN (SELECT sph.ShipmentNumber FROM SingleIB.mShipmentHeader as sph 
							WHERE sph.SalesOrderId = inv.SalesOrderId
								AND sph.IsDirect = 1)
					ELSE NULL
				END AS ShipmentNumber
				FROM SingleIB.mInvoiceHeader as inv
				--INNER JOIN SingleIB.mCustomer as cus
				--	ON inv.CustomerId = cus.CustomerId
				INNER JOIN SingleIB.mSalesOrderHeader as poh
					ON inv.SalesOrderId = poh.SalesOrderId
				WHERE inv.InvoiceId = @InvoiceId;

				SELECT ind.*
				FROM mInvoiceDetail as ind
				WHERE ind.InvoiceId = @InvoiceId;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetInvoiceType]...';


GO


CREATE PROCEDURE [SingleIB].[spGetInvoiceType]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mInvoiceType;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetPaymentIn]...';


GO



CREATE PROCEDURE [SingleIB].[spGetPaymentIn]
(
	@CustomerId	bigint = 0,

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
				SELECT pih.*, cus.*
				FROM SingleIB.mPaymentInHeader as pih
					INNER JOIN SingleIB.mCustomer as cus
				ON pih.CustomerId = cus.CustomerId
				ORDER BY pih.PaymentDate DESC, pih.PaymentInId DESC;
			END
			ELSE IF (@QueryType = 'COMULTATIVE')
			BEGIN
				SELECT SUM(pitdl.Amount) AS Amount, 
						pitdl.InvoiceId				 
				FROM mPaymentInDetail as pitdl
				WHERE pitdl.PaymentInId IN 
						(SELECT pitdh.PaymentInId
						FROM SingleIB.mPaymentInHeader as pitdh
						WHERE pitdh.CustomerId = @CustomerId)
				GROUP BY pitdl.InvoiceId;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetPaymentOut]...';


GO



CREATE PROCEDURE [SingleIB].[spGetPaymentOut]
(
	@VendorId	bigint = 0,

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
				SELECT poh.*, vod.*
				FROM SingleIB.mPaymentOutHeader as poh
					INNER JOIN SingleIB.mVendor as vod
				ON poh.VendorId = vod.VendorId
				ORDER BY poh.PaymentDate DESC, poh.PaymentOutId DESC;
			END
			ELSE IF (@QueryType = 'COMULTATIVE')
			BEGIN
				SELECT SUM(potdl.Amount) AS Amount, 
						potdl.BillId				 
				FROM mPaymentOutDetail as potdl
				WHERE potdl.PaymentOutId IN 
						(SELECT potdh.PaymentOutId
						FROM SingleIB.mPaymentOutHeader as potdh
						WHERE potdh.VendorId = @VendorId)
				GROUP BY potdl.BillId;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetPaymentType]...';


GO


CREATE PROCEDURE [SingleIB].[spGetPaymentType]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mPaymentType;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetProduct]...';


GO
CREATE PROCEDURE [SingleIB].[spGetProduct]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT p.*, t.ProductTypeName
				FROM SingleIB.mProduct as p
					INNER JOIN SingleIB.mProductType as t
				ON p.ProductTypeId = t.ProductTypeId
				ORDER BY p.IsActive DESC, p.ProductId DESC;
			END
			IF (@QueryType ='INTIME')
			BEGIN
				SELECT p.*, t.ProductTypeName
				FROM SingleIB.mProduct as p
					INNER JOIN SingleIB.mProductType as t
				ON p.ProductTypeId = t.ProductTypeId					
				WHERE p.IsActive = 1
					AND p.ProductInOut IN (0, 1);
			END
			IF (@QueryType ='OUTTIME')
			BEGIN
				SELECT p.*, t.ProductTypeName
				FROM SingleIB.mProduct as p
					INNER JOIN SingleIB.mProductType as t
				ON p.ProductTypeId = t.ProductTypeId					
				WHERE p.IsActive = 1
					AND p.ProductInOut IN (0, 2);
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetProductType]...';


GO
CREATE PROCEDURE [SingleIB].[spGetProductType]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mProductType;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetPurchaseOrder]...';


GO



CREATE PROCEDURE [SingleIB].[spGetPurchaseOrder]
(
	@PurchaseOrderId int = 0,

	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ENTRY')
			BEGIN
				SELECT *
				FROM SingleIB.mPurchaseType
				WHERE IsActive = 1;				
				SELECT *
				FROM SingleIB.mBranch
				WHERE IsActive = 1;
				SELECT *
				FROM SingleIB.mVendor
				WHERE IsActive = 1;
				SELECT p.*
				FROM SingleIB.mProduct as p					
				WHERE p.IsActive = 1
					AND (p.ProductInOut = 0 OR p.ProductInOut = 1);
			END
			ELSE IF (@QueryType ='DETAIL')
			BEGIN
				-- Header
				SELECT 
					CASE 
						WHEN poh.PurchaseOrderNumber IS NULL THEN (SELECT grh.GRNNumber FROM SingleIB.mGRNHeader as grh 
								WHERE grh.PurchaseOrderId = @PurchaseOrderId
									AND grh.IsDirect = 1)
						ELSE NULL
					END AS GRNNumber,
					poh.*,
					-- GRN & Bill Status
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mGRNHeader] AS grn WHERE grn.PurchaseOrderId = @PurchaseOrderId
										AND grn.IsFullReceived = 1 AND grn.IsActive = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mGRNHeader] AS grn WHERE grn.PurchaseOrderId = @PurchaseOrderId
									AND grn.IsFullReceived <> 1 AND grn.IsActive = 1)
							THEN 0
							ELSE null
						END) AS BIT) AS GoodsReceivedStatus,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = @PurchaseOrderId
										AND bil.IsFullBilled = 1 AND bil.IsActive = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = @PurchaseOrderId
									AND bil.IsFullBilled <> 1 AND bil.IsActive = 1)
							THEN 0
							ELSE null
						END) AS BIT) AS BilledStatus,
					-- Status
					bra.BranchName, bra.BranchCode,
					vnd.CompanyName, vnd.CompanyType, vnd.ContactPerson, 
						vnd.GSTNumber, vnd.GSTStateCode, vnd.StateCode, vnd.IsGSTRegistered, vnd.PANNumber
				FROM SingleIB.mPurchaseOrderHeader AS poh
					INNER JOIN SingleIB.mBranch as bra
					ON poh.BranchId = bra.BranchId
					INNER JOIN SingleIB.mVendor as vnd
					ON poh.VendorId = vnd.VendorId
				WHERE poh.PurchaseOrderId = @PurchaseOrderId					
					AND poh.IsActive = 1;

				-- Detail
				SELECT pro.Brand, pro.ProductName, pro.ProductCode, pro.ProductId, 
					pro.ProductImageUrl, 
					pro.ProductInOut,
					pro.UnitOfMeasureName, 
					pod.*
				FROM SingleIB.mPurchaseOrderDetail as pod
					INNER JOIN SingleIB.mProduct as pro
					ON pod.ProductId = pro.ProductId
				WHERE pod.PurchaseOrderId = @PurchaseOrderId 
					AND pod.IsActive = 1;
			END
			ELSE IF(@QueryType ='LIST')
			BEGIN
				SELECT bra.BranchName, 
					bra.BranchCode, 
					vdr.CompanyName,
					vdr.IsGSTRegistered, 
					vdr.GSTStateCode,
					vdr.GSTNumber, 
					vdr.PANNumber, 
					poh.*,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mGRNHeader] AS grn WHERE grn.PurchaseOrderId = poh.PurchaseOrderId
										AND grn.IsFullReceived = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mGRNHeader] AS grn WHERE grn.PurchaseOrderId = poh.PurchaseOrderId
									AND grn.IsFullReceived <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS GoodsReceivedStatus,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = poh.PurchaseOrderId
										AND bil.IsFullBilled = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mBillHeader] AS bil WHERE bil.PurchaseOrderId = poh.PurchaseOrderId
									AND bil.IsFullBilled <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS BilledStatus
				FROM SingleIB.mPurchaseOrderHeader AS poh
					INNER JOIN SingleIB.mBranch AS bra
				ON poh.BranchId = bra.BranchId 
					INNER JOIN SingleIB.mVendor AS vdr
				ON poh.VendorId = vdr.VendorId
				WHERE poh.PurchaseOrderNumber IS NOT NULL
					AND poh.IsActive = 1
				ORDER BY poh.PurchaseOrderDate DESC, poh.PurchaseOrderId DESC;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetPurchaseType]...';


GO
CREATE PROCEDURE [SingleIB].[spGetPurchaseType]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mPurchaseType;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetQuotation]...';


GO

CREATE PROCEDURE [SingleIB].[spGetQuotation]
(
	@QuotationId int = 0,

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
				SELECT cmr.CompanyName,
					cmr.IsGSTRegistered, 
					cmr.GSTStateCode,
					cmr.GSTNumber, 
					cmr.PANNumber, 
					qh.*,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mSalesOrderHeader] AS sph 
										WHERE sph.QuotationId = qh.QuotationId AND sph.IsActive = 1)
							   THEN 1
							ELSE 0
						END) AS BIT) AS SOGenerated
				FROM SingleIB.mQuotationHeader AS qh					
					INNER JOIN SingleIB.mCustomer AS cmr
				ON qh.CustomerId = cmr.CustomerId
				WHERE qh.IsActive = 1
				ORDER BY qh.QuotationDate DESC, qh.QuotationId DESC;
			END
			ELSE IF (@QueryType ='DETAIL')
			BEGIN
				-- Table 0
				SELECT 					
					soh.*, 
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mSalesOrderHeader] AS sph 
										WHERE sph.QuotationId = soh.QuotationId AND sph.IsActive = 1)
							   THEN 1
							ELSE null
						END) AS BIT) AS SOGenerated,
					cus.CompanyName, cus.CompanyType, cus.ContactPerson, cus.StateCode,
						cus.GSTNumber, cus.GSTStateCode, cus.IsGSTRegistered, cus.PANNumber
				FROM SingleIB.mQuotationHeader AS soh
				INNER JOIN SingleIB.mCustomer as cus
					ON soh.CustomerId = cus.CustomerId
				WHERE soh.QuotationId = @QuotationId
					AND soh.IsActive = 1; 

				-- Table 1
				SELECT pro.Brand, pro.ProductName, pro.ProductCode, 
					pro.ProductId, 
					pro.ProductImageUrl, 
					pro.ProductInOut,					
					pro.UnitOfMeasureName,
					pro.IsGoods,
					sod.*
				FROM SingleIB.mQuotationDetail as sod
					INNER JOIN SingleIB.mProduct as pro
					ON sod.ProductId = pro.ProductId
				WHERE sod.QuotationId = @QuotationId;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetSalesOrder]...';


GO

CREATE PROCEDURE [SingleIB].[spGetSalesOrder]
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
							WHEN EXISTS (SELECT * FROM [SingleIB].[mShipmentHeader] AS sph 
										WHERE sph.SalesOrderId = soh.SalesOrderId AND sph.IsFullShipped = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mShipmentHeader] AS grn 
										WHERE grn.SalesOrderId = soh.SalesOrderId AND grn.IsFullShipped <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS ShippedStatus,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mInvoiceHeader] AS inv 
										WHERE inv. SalesOrderId = soh.SalesOrderId AND inv.IsFullInvoiced = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mInvoiceHeader] AS inv 
									WHERE inv.SalesOrderId = soh.SalesOrderId AND inv.IsFullInvoiced <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS InvoicedStatus
				FROM SingleIB.mSalesOrderHeader AS soh
					INNER JOIN SingleIB.mBranch AS bra
				ON soh.BranchId = bra.BranchId 
					INNER JOIN SingleIB.mCustomer AS cmr
				ON soh.CustomerId = cmr.CustomerId
				WHERE soh.SalesOrderNumber IS NOT NULL
					AND soh.IsActive = 1
				ORDER BY soh.SalesOrderDate DESC, soh.SalesOrderId DESC;
			END
			IF (@QueryType ='ENTRY')
			BEGIN
				SELECT *
				FROM SingleIB.mSalesType
				WHERE IsActive = 1;				
				SELECT *
				FROM SingleIB.mBranch
				WHERE IsActive = 1;
				SELECT *
				FROM SingleIB.mCustomer
				WHERE IsRetailCustomer = 0
					AND IsActive = 1;
				SELECT p.*
				FROM SingleIB.mProduct as p					
				WHERE p.IsActive = 1
					AND (p.ProductInOut = 0 OR p.ProductInOut = 1);
			END
			ELSE IF (@QueryType ='DETAIL')
			BEGIN
				-- Table 0
				SELECT 
					CASE 
						WHEN soh.SalesOrderNumber IS NULL THEN (SELECT grh.ShipmentNumber FROM SingleIB.mShipmentHeader as grh 
								WHERE grh.SalesOrderId = @SalesOrderId
									AND grh.IsDirect = 1)
						ELSE NULL
					END AS ShipmentNumber,
					soh.*, 
						(SELECT TOP 1 qh.QuotationNumber FROM SingleIB.mQuotationHeader as qh 
							WHERE qh.QuotationId = soh.QuotationId
								AND qh.IsActive = 1) 
					as QuotationNumber,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mShipmentHeader] AS sph WHERE sph.SalesOrderId = @SalesOrderId
										AND sph.SalesOrderId = soh.SalesOrderId AND sph.IsFullShipped = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mShipmentHeader] AS sph WHERE sph.SalesOrderId = @SalesOrderId
										AND sph.SalesOrderId = soh.SalesOrderId AND sph.IsFullShipped <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS ShippedStatus,
					CAST(
						(CASE 
							WHEN EXISTS (SELECT * FROM [SingleIB].[mInvoiceHeader] AS inv WHERE inv.SalesOrderId = @SalesOrderId
										AND inv. SalesOrderId = soh.SalesOrderId AND inv.IsFullInvoiced = 1)
							   THEN 1
							WHEN EXISTS (SELECT * FROM [SingleIB].[mInvoiceHeader] AS inv WHERE inv.SalesOrderId = @SalesOrderId
										AND inv.SalesOrderId = soh.SalesOrderId AND inv.IsFullInvoiced <> 1)
							THEN 0
							ELSE null
						END) AS BIT) AS InvoicedStatus,
					bra.BranchName, bra.BranchCode,
					cus.CompanyName, cus.CompanyType, cus.ContactPerson, cus.StateCode,
						cus.GSTNumber, cus.GSTStateCode, cus.IsGSTRegistered, cus.PANNumber
				FROM SingleIB.mSalesOrderHeader AS soh
				INNER JOIN SingleIB.mBranch as bra
					ON soh.BranchId = bra.BranchId
				INNER JOIN SingleIB.mCustomer as cus
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
				FROM SingleIB.mSalesOrderDetail as sod
					INNER JOIN SingleIB.mProduct as pro
					ON sod.ProductId = pro.ProductId
				WHERE sod.SalesOrderId = @SalesOrderId;
			END
			ELSE IF (@QueryType ='SHIPENTRY')
			BEGIN
				SELECT soh.*
				FROM SingleIB.mSalesOrderHeader soh
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
						FROM SingleIB.mShipmentHeader
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetSalesType]...';


GO
CREATE PROCEDURE [SingleIB].[spGetSalesType]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mSalesType;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetShipment]...';


GO

CREATE PROCEDURE [SingleIB].[spGetShipment]
(
	@SalesOrderId	bigint = 0,
	@ShipmentId		bigint = 0,

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
				SELECT sph.*, who.*, spt.ShipmentTypeName,
				CAST(
					(CASE 
						WHEN sph.IsDirect = 1
							THEN 
							(CASE 
								WHEN EXISTS (SELECT * FROM [SingleIB].[mInvoiceHeader] AS inv WHERE inv.SalesOrderId = sph.SalesOrderId
											AND inv.IsFullInvoiced = 1)
									THEN 1
								WHEN EXISTS (SELECT * FROM [SingleIB].[mInvoiceHeader] AS inv WHERE inv.SalesOrderId = sph.SalesOrderId
										AND inv.IsFullInvoiced <> 1)
								THEN 0
								ELSE null
							END) 
						ELSE null
					END) 
				 AS BIT) AS InvoicedStatus
				FROM SingleIB.mShipmentHeader as sph
					INNER JOIN SingleIB.mWareHouse as who
					ON sph.WareHouseId = who.WareHouseId
					INNER JOIN SingleIB.mShipmentType as spt
					ON sph.ShipmentTypeId = spt.ShipmentTypeId
				ORDER BY sph.ShipmentDate DESC, sph.ShipmentId DESC;
			END
			ELSE IF (@QueryType = 'COMULTATIVE')
			BEGIN
				SELECT
					0 AS ShipmentDetailId,
					NULL AS ShipmentId,
					NULL AS ShipmentNumber,
					shpd.SalesOrderDetailId,
					0 as ProductId,
					SUM(shpd.ShippedQuantity) AS ShippedQuantity,
					NULL AS Description,
					CAST(1 AS BIT) AS IsActive,
					NULL AS TransactionDate,
					NULL AS RequestId
				FROM SingleIB.mShipmentDetail as shpd
				WHERE shpd.ShipmentId IN (
					SELECT shph.ShipmentId
					FROM SingleIB.mShipmentHeader as shph
					WHERE shph.SalesOrderId = @SalesOrderId
				)
				GROUP BY shpd.SalesOrderDetailId;
			END
			ELSE IF (@QueryType = 'DETAIL')
			BEGIN
				SELECT sph.*, who.*
				FROM SingleIB.mShipmentHeader as sph
					INNER JOIN SingleIB.mWareHouse as who
				ON sph.WareHouseId = who.WareHouseId
				WHERE sph.ShipmentId = @ShipmentId;

				SELECT spd.*		
				FROM SingleIB.mShipmentDetail as spd					
				WHERE spd.ShipmentId = @ShipmentId;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetShipmentType]...';


GO


CREATE PROCEDURE [SingleIB].[spGetShipmentType]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mShipmentType;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetSmsTrack]...';


GO
CREATE PROCEDURE [SingleIB].[spGetSmsTrack]
(
	@ComponentIdentity	nvarchar(64) = NULL,
	@SmsFor				varchar(64) = NULL,

	@QueryType		varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='LINKWISE')
			BEGIN
				SELECT TOP 1 *
				FROM SingleIB.tSmsTrack 
				WHERE ComponentIdentity = @ComponentIdentity
					AND SmsFor = @SmsFor
					AND IsActive = 1;
			END
			ELSE IF (@QueryType ='SINGLE')
			BEGIN
				SELECT TOP 1 *
				FROM SingleIB.tSmsTrack 
				WHERE ComponentIdentity = @ComponentIdentity
					AND SmsFor = @SmsFor
					AND IsActive = 1;
			END
			ELSE IF (@QueryType ='ALL')
			BEGIN
				SELECT st.* 
				FROM SingleIB.tSmsTrack as st
				Order By st.SmsSendId DESC;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetStock]...';


GO
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetStockAdjustReason]...';


GO
CREATE PROCEDURE [SingleIB].[spGetStockAdjustReason]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.sStockAdjustReason
				WHERE IsDefault = 1
					AND IsActive = 1
				UNION
				SELECT *
				FROM SingleIB.sStockAdjustReason
					WHERE IsDefault = 0
					AND IsActive = 1;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetTDSPay]...';


GO
CREATE PROCEDURE [SingleIB].[spGetTDSPay]
(
	@QueryType	varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='UNPAID')
			BEGIN
				SELECT tp.*, 
					poh.TDSPercentage, poh.TDSAmount, poh.PayAmount, poh.TotalAmount, poh.PaymentDate,
					vdr.CompanyName, vdr.CompanyType, vdr.StateCode, 
					vdr.IsGSTRegistered, vdr.GSTStateCode, vdr.GSTNumber
				FROM tTDSPay as tp
					INNER JOIN SingleIB.mPaymentOutHeader as poh
				ON tp.PaymentOutId = poh.PaymentOutId
					INNER JOIN SingleIB.mVendor as vdr
				ON vdr.VendorId = tp.VendorId
				WHERE tp.IsPaid = 0
					AND poh.IsTDSApplicable = 1
				ORDER BY poh.PaymentDate DESC, tp.TDSPayId DESC;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetTDSReceive]...';


GO
CREATE PROCEDURE [SingleIB].[spGetTDSReceive]
(
	@QueryType	varchar(16)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='UNRECEIVED')
			BEGIN
				SELECT tr.*, 
					pih.TDSPercentage, pih.TDSAmount, pih.ReceiveAmount, pih.TotalAmount, pih.PaymentDate,
					cus.CompanyName, cus.CompanyType, cus.StateCode, 
					cus.IsGSTRegistered, cus.GSTStateCode, cus.GSTNumber
				FROM tTDSReceive as tr
					INNER JOIN SingleIB.mPaymentInHeader as pih
				ON tr.PaymentInId = pih.PaymentInId
					INNER JOIN SingleIB.mCustomer as cus
				ON cus.CustomerId = tr.CustomerId
				WHERE tr.IsReceived = 0
					AND pih.IsTDSApplicable = 1
				ORDER BY pih.PaymentDate DESC, tr.TDSReceiveId DESC;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetUnitOfMeasure]...';


GO
CREATE PROCEDURE [SingleIB].[spGetUnitOfMeasure]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mUnitOfMeasure;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetVendor]...';


GO
CREATE PROCEDURE [SingleIB].[spGetVendor]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mVendor v
					INNER JOIN SingleIB.mVendorType as vt
				ON v.VendorTypeId = vt.VendorTypeId;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetVendorBank]...';


GO
CREATE PROCEDURE [SingleIB].[spGetVendorBank]
(
	@VendorId			bigint = 0,

	@QueryType		varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mVendorBank
				WHERE VendorId = @VendorId;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetVendorType]...';


GO
CREATE PROCEDURE [SingleIB].[spGetVendorType]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT *
				FROM SingleIB.mVendorType;
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
GO
PRINT N'Creating Procedure [SingleIB].[spGetWareHouse]...';


GO
CREATE PROCEDURE [SingleIB].[spGetWareHouse]
(
	@QueryType	varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Select';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ALL')
			BEGIN
				SELECT who.*, bra.*
				FROM SingleIB.mWareHouse as who
					INNER JOIN SingleIB.mBranch as bra
					ON who.BranchId = bra.BranchId
				ORDER BY who.IsActive DESC, who.WareHouseId DESC;
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
GO
PRINT N'Creating Procedure [SingleIB].[spRpGST]...';


GO
CREATE PROCEDURE [SingleIB].[spRpGST]
(
	@StartDate			datetime = null,
	@EndDate			datetime = null,

	@PaymentStatus		bit = null,

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
			IF (@QueryType ='OUTPUTGST')
			BEGIN
				SET @SQL = 'SELECT inv.* 
						FROM SingleIB.mInvoiceHeader as inv
						WHERE (inv.InvoiceDate BETWEEN CONVERT(DATE, ''' + CAST(YEAR(@StartDate) as VARCHAR) + '-' 
															+ CAST(MONTH(@StartDate) as VARCHAR) + '-' 
															+ CAST(DAY(@StartDate) as VARCHAR) + ''') 
								AND CONVERT(DATE, ''' + CAST(YEAR(@EndDate) as VARCHAR) + '-' 
										+ CAST(MONTH(@EndDate) as VARCHAR) + '-' 
										+ CAST(DAY(@EndDate) as VARCHAR) + '''))';
				
				IF @PaymentStatus IS NOT NULL
				BEGIN
					SET @WhereClause = ' (EXISTS (SELECT * FROM [SingleIB].[mPaymentInDetail] AS pod 
								WHERE  inv.InvoiceId = pod.InvoiceId
									AND pod.IsFullReceived = ' + CAST(@PaymentStatus as VARCHAR) + ')) ' ;
				END
				IF @WhereClause IS NOT NULL
				BEGIN
					SET @SQL = @SQL + ' AND ' + @WhereClause 
				END

				--PRINT(@WhereClause);
				--PRINT (@SQL);
				EXECUTE(@SQL);
			END

			ELSE IF (@QueryType ='INPUTGST')
			BEGIN
				SET @SQL = 'SELECT bil.* 
						FROM SingleIB.mBillHeader as bil
						WHERE (bil.BillDate BETWEEN CONVERT(DATE, ''' + CAST(YEAR(@StartDate) as VARCHAR) + '-' 
															+ CAST(MONTH(@StartDate) as VARCHAR) + '-' 
															+ CAST(DAY(@StartDate) as VARCHAR) + ''') 
								AND CONVERT(DATE, ''' + CAST(YEAR(@EndDate) as VARCHAR) + '-' 
										+ CAST(MONTH(@EndDate) as VARCHAR) + '-' 
										+ CAST(DAY(@EndDate) as VARCHAR) + '''))';
				
				IF @PaymentStatus IS NOT NULL
				BEGIN
					SET @WhereClause = ' (EXISTS (SELECT * FROM [SingleIB].[mPaymentOutDetail] AS pod 
									WHERE  bil. BillId = pod.BillId
									AND pod.IsFullPaid = ' + CAST(@PaymentStatus as VARCHAR) + ')) ' ;
				END
				IF @WhereClause IS NOT NULL
				BEGIN
					SET @SQL = @SQL + ' AND ' + @WhereClause 
				END

				--PRINT(@WhereClause);
				PRINT (@SQL);
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
GO
PRINT N'Creating Procedure [SingleIB].[spRpPurchase]...';


GO
CREATE PROCEDURE [SingleIB].[spRpPurchase]
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
						FROM SingleIB.mBillHeader as bilh
						WHERE (bilh.BillDate BETWEEN CONVERT(DATE, ''' + CAST(YEAR(@StartDate) as VARCHAR) + '-' 
															+ CAST(MONTH(@StartDate) as VARCHAR) + '-' 
															+ CAST(DAY(@StartDate) as VARCHAR) + ''') 
								AND CONVERT(DATE, ''' + CAST(YEAR(@EndDate) as VARCHAR) + '-' 
										+ CAST(MONTH(@EndDate) as VARCHAR) + '-' 
										+ CAST(DAY(@EndDate) as VARCHAR) + '''))
							GROUP BY bilh.VendorId) 
					AS inrtab,
					SingleIB.mVendor as ven 
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
						FROM SingleIB.mBillDetail as billd
						LEFT JOIN SingleIB.mBillHeader as billh
							ON billd.BillId = billh.BillId
						WHERE (billh.BillDate BETWEEN CONVERT(DATE, ''' + CAST(YEAR(@StartDate) as VARCHAR) + '-' 
														+ CAST(MONTH(@StartDate) as VARCHAR) + '-' 
														+ CAST(DAY(@StartDate) as VARCHAR) + ''') 
								AND CONVERT(DATE, ''' + CAST(YEAR(@EndDate) as VARCHAR) + '-' 
									+ CAST(MONTH(@EndDate) as VARCHAR) + '-' 
									+ CAST(DAY(@EndDate) as VARCHAR) + ''')) 
						GROUP BY billd.ProductId) AS inrtab,
						SingleIB.mProduct as pro 
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
GO
PRINT N'Creating Procedure [SingleIB].[spRpSales]...';


GO
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetBillType]...';


GO



CREATE PROCEDURE [SingleIB].[spSetBillType]
(
	@BillTypeId			bigint = 0,
	@BillTypeName		nvarchar(128) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(10),
	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.mBillType
					(BillTypeName,
					[Description],
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@BillTypeName, 
					@Description,
					1,
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mBillType WHERE BillTypeId = @BillTypeId)
				BEGIN
					UPDATE SingleIB.mBillType
					SET BillTypeName = @BillTypeName, 
						[Description] = @Description,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE BillTypeId = @BillTypeId;
					SET @OutParam = @BillTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mBillType WHERE BillTypeId = @BillTypeId)
				BEGIN
					UPDATE SingleIB.mBillType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE BillTypeId = @BillTypeId;
					SET @OutParam = @BillTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetBranch]...';


GO
CREATE PROCEDURE [SingleIB].[spSetBranch]
(
	@BranchId			int = 0,

	@BranchName			nvarchar(64) = NULL,
	@BranchCode			nvarchar(64) = NULL,
	@Address			nvarchar(128) = NULL,
	@City				nvarchar(32) = NULL,
	@State				nvarchar(2) = NULL,
	@PostalCode			nvarchar(8) = NULL,
	@ContactPerson		nvarchar(64) = NULL,
	@Email				nvarchar(64) = NULL,
	@Phone				nvarchar(16) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'SET';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN				
				INSERT INTO SingleIB.mBranch
					(BranchName,
					BranchCode,
					Address,
					City,
					State,
					PostalCode,
					ContactPerson,
					Email,
					Phone,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@BranchName, 
					@BranchCode, 
					@Address, 
					@City, 
					@State, 
					@PostalCode, 
					@ContactPerson, 
					@Email, 
					@Phone, 
					@Description, 
					1,
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mBranch WHERE BranchId = @BranchId)
				BEGIN
					UPDATE SingleIB.mBranch
					SET BranchName = @BranchName, 
						BranchCode = @BranchCode, 
						Address = @Address, 
						City = @City, 
						State = @State, 
						PostalCode = @PostalCode, 
						ContactPerson = @ContactPerson, 
						Email = @Email, 
						Phone = @Phone, 
						Description = @Description,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE BranchId = @BranchId;
					SET @OutParam = @BranchId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mBranch WHERE BranchId = @BranchId)
				BEGIN
					UPDATE SingleIB.mBranch
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE BranchId = @BranchId;
					SET @OutParam = @BranchId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetCompanyBank]...';


GO
CREATE PROCEDURE [SingleIB].[spSetCompanyBank]
(
	@CompanyBankId		bigint = 0,
	
	@AccountName		nvarchar(1204) = NULL,
	@AccountNumber		nvarchar(64) = NULL,
	@BankName			nvarchar(128) = NULL,
	@IFSC				nvarchar(16) = NULL,
	@MICR				nvarchar(16) = NULL,
	@BranchName			nvarchar(64) = NULL,
	@BranchAddress		nvarchar(128) = NULL,
	@IsPrintable		bit = 0,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(32),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.mCompanyBank
					(AccountName,
					AccountNumber,
					BankName,
					IFSC,
					MICR,
					BranchName,
					BranchAddress,
					IsPrintable,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@AccountName, 
					@AccountNumber, 
					@BankName, 
					@IFSC, 
					@MICR, 
					@BranchName, 
					@BranchAddress, 
					@IsPrintable, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mCompanyBank WHERE CompanyBankId = @CompanyBankId)
				BEGIN
					UPDATE SingleIB.mCompanyBank
					SET AccountName = @AccountName, 
						AccountNumber = @AccountNumber, 
						BankName = @BankName, 
						IFSC = @IFSC, 
						MICR = @MICR, 
						BranchName = @BranchName, 
						BranchAddress = @BranchAddress, 
						IsActive = 1, 
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE CompanyBankId = @CompanyBankId ;
					SET @OutParam = @CompanyBankId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mCompanyBank WHERE CompanyBankId = @CompanyBankId)
				BEGIN
					UPDATE SingleIB.mCompanyBank
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE CompanyBankId = @CompanyBankId ;
					SET @OutParam = @CompanyBankId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='PRINTABILITY')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mCompanyBank WHERE CompanyBankId = @CompanyBankId)
				BEGIN
					UPDATE SingleIB.mCompanyBank
					SET IsPrintable = 0;

					UPDATE SingleIB.mCompanyBank
					SET IsPrintable = @IsPrintable, 
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE CompanyBankId = @CompanyBankId ;
					SET @OutParam = @CompanyBankId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetCustomer]...';


GO

CREATE PROCEDURE [SingleIB].[spSetCustomer]
(
	@CustomerId			bigint = 0,
	@CustomerTypeId		bigint = 0,
	
	@ContactPerson		nvarchar(64) = NULL,
	@Email				nvarchar(64) = NULL,
	@Mobile				nvarchar(16) = NULL,
	@IsRetailCustomer	bit = 0,

	@CompanyName		nvarchar(1204) = NULL,
	@CompanyType		nvarchar(64) = NULL,
	@StateCode			nvarchar(2) = null,
	@IsGSTRegistered	bit = 0,
	@GSTStateCode		nvarchar(2) = NULL,
	@GSTNumber			nvarchar(16) = NULL,
	@PANNumber			nvarchar(16) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.mCustomer
					(CustomerTypeId,
					CompanyName,
					CompanyType,
					StateCode,
					IsGSTRegistered,
					GSTStateCode,
					GSTNumber,
					PANNumber,
					ContactPerson,
					Email,
					Mobile,
					IsRetailCustomer,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@CustomerTypeId, 
					@CompanyName, 
					@CompanyType, 
					@StateCode,
					@IsGSTRegistered,
					@GSTStateCode,
					@GSTNumber, 
					@PANNumber,
					@ContactPerson,
					@Email,
					@Mobile, 
					@IsRetailCustomer,
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mCustomer WHERE CustomerId = @CustomerId)
				BEGIN
					UPDATE SingleIB.mCustomer
					SET CustomerTypeId = @CustomerTypeId, 
						CompanyName = @CompanyName, 
						CompanyType = @CompanyType, 
						StateCode = @StateCode,
						IsGSTRegistered = @IsGSTRegistered, 
						GSTStateCode = @GSTStateCode,
						GSTNumber = @GSTNumber, 
						PANNumber = @PANNumber, 
						ContactPerson = @ContactPerson,
						Email = @Email,
						Mobile = @Mobile,
						IsActive = 1, 
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE CustomerId = @CustomerId;
					SET @OutParam = @CustomerId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mCustomer WHERE CustomerId = @CustomerId)
				BEGIN
					UPDATE SingleIB.mCustomer
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE CustomerId = @CustomerId;
					SET @OutParam = @CustomerId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF (@QueryType ='RETAILINSERT')
			BEGIN
				INSERT INTO SingleIB.mCustomer
					(CustomerTypeId,
					CompanyName,
					CompanyType,
					StateCode,
					IsGSTRegistered,
					GSTStateCode,
					GSTNumber,
					PANNumber,
					ContactPerson,
					Email,
					Mobile,
					IsRetailCustomer,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@CustomerTypeId, 
					@CompanyName, 
					@CompanyType, 
					@StateCode,
					@IsGSTRegistered,
					@GSTStateCode,
					@GSTNumber, 
					@PANNumber,
					@ContactPerson,
					@Email,
					@Mobile, 
					@IsRetailCustomer,
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetCustomerType]...';


GO

CREATE PROCEDURE [SingleIB].[spSetCustomerType]
(
	@CustomerTypeId		bigint = 0,
	@CustomerTypeName		nvarchar(128) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.mCustomerType
					(CustomerTypeName,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@CustomerTypeName, 
					@Description, 
					1,
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mCustomerType WHERE CustomerTypeId = @CustomerTypeId)
				BEGIN
					UPDATE SingleIB.mCustomerType
					SET CustomerTypeName = @CustomerTypeName, 
						Description = @Description, 
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE CustomerTypeId = @CustomerTypeId;
					SET @OutParam = @CustomerTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mCustomerType WHERE CustomerTypeId = @CustomerTypeId)
				BEGIN
					UPDATE SingleIB.mCustomerType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE CustomerTypeId = @CustomerTypeId;
					SET @OutParam = @CustomerTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetInvoice]...';


GO

CREATE PROCEDURE [SingleIB].[spSetInvoice]
(
	@InvoiceTypeId		bigint = 0,
	@InvoiceDate		datetime = NULL,
	@InvoiceDueDate		datetime = NULL,
	@InvoiceNumber		nvarchar(16) = NULL,
	@CustomerId			bigint = 0,
	@SalesOrderId		bigint = 0,

    @VehicleNumber			nvarchar(16) = NULL,
    @HandOverPerson			nvarchar(64) = NULL,
	@HandOverPersonMobile	nvarchar(16) = null,

	@Remarks				nvarchar(512) = NULL,    

	@IsGstApplicable		bit = 0,
	@GSTNumber			nvarchar(16) = NULL,
	@GSTStateCode		nvarchar(2) = NULL,
	@GSTType			smallint = NULL,	
    @InvoicedAmount		decimal(18,5) = 0,
	@TaxAmount			decimal(18,5) = 0,
	@TotalAmount		decimal(18,5) = 0,
	
	@IsFullInvoiced		bit = 0,

	@InvoiceDetail [SingleIB].[InvoiceDetailType] READONLY, 
	@RequestId			nvarchar(64),
	@QueryType			varchar(16),
	@OutParam			nvarchar(512) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@InvoiceId bigint,		
		@NextInvoiceNo bigint;

	SELECT @T1 = 'InvoiceInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** Invoice Number Generate **/
				SELECT @NextInvoiceNo = (COUNT(*) + 1) FROM SingleIB.mInvoiceHeader;

				SET @InvoiceNumber = @InvoiceNumber + RIGHT('000000' + CAST(@NextInvoiceNo as varchar(16)), 6);

				/** INSERT INTO mInvoiceHeader **/
				INSERT INTO SingleIB.mInvoiceHeader
					(InvoiceTypeId,
					InvoiceDate,
					InvoiceDueDate,
					InvoiceNumber,
					IsFullInvoiced,
					SalesOrderId,
					CustomerId,
					IsGstApplicable,
					GSTNumber,
					GSTStateCode,
					GSTType,
					InvoicedAmount,
					TaxAmount,
					TotalAmount,
					VehicleNumber,
					HandOverPerson,
					HandOverPersonMobile,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@InvoiceTypeId, 
					@InvoiceDate, 
					@InvoiceDueDate, 
					@InvoiceNumber, 
					@IsFullInvoiced, 
					@SalesOrderId, 
					@CustomerId, 
					@IsGstApplicable, 
					@GSTNumber, 
					@GSTStateCode, 
					@GSTType, 
					@InvoicedAmount, 
					@TaxAmount, 
					@TotalAmount, 
					@VehicleNumber, 
					@HandOverPerson, 
					@HandOverPersonMobile, 
					@Remarks, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId)

				SET @InvoiceId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO mInvoiceDetail **/
				INSERT INTO SingleIB.mInvoiceDetail
					(InvoiceId,
					InvoiceNumber,
					SalesOrderDetailId,
					ProductId,
					HSNSAC,
					Price,
					InvoicedQuantity,
					SubTotal,
					TaxPercentage,
					CGSTAmount,
					SGSTAmount,
					IGSTAmount,
					Total,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@InvoiceId as InvoiceId,
					@InvoiceNumber as InvoiceNumber,
					SalesOrderDetailId,
					ProductId,
					HSNSAC,
					Price,
					InvoicedQuantity,
					SubTotal,
					TaxPercentage,
					CGSTAmount,
					SGSTAmount,
					IGSTAmount,
					Total,
					Description,
					1 AS IsActive,
					SingleIB.ISTNow() as  TransactionDate,
					@RequestId as RequestId
				FROM @InvoiceDetail;

				/* Check Full Invoiced or not */
				UPDATE SingleIB.mInvoiceHeader
				SET IsFullInvoiced = SingleIB.IsFullInvoiced(@SalesOrderId)
				WHERE SalesOrderId = @SalesOrderId
					AND InvoiceId = @InvoiceId;
				
				SET @OutParam = '{ "InvoiceId" : ' +
							CAST(@InvoiceId as VARCHAR(64)) +
							', "InvoiceNumber" : "' +
							CAST(@InvoiceNumber AS VARCHAR(16)) +
							'" }';
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetInvoiceType]...';


GO



CREATE PROCEDURE [SingleIB].[spSetInvoiceType]
(
	@InvoiceTypeId		bigint = 0,
	@InvoiceTypeName	nvarchar(128) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(10),
	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.mInvoiceType
					(InvoiceTypeName,
					[Description],
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@InvoiceTypeName, 
					@Description,
					1,
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mInvoiceType WHERE InvoiceTypeId = @InvoiceTypeId)
				BEGIN
					UPDATE SingleIB.mInvoiceType
					SET InvoiceTypeName = @InvoiceTypeName, 
						[Description] = @Description,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE InvoiceTypeId = @InvoiceTypeId;
					SET @OutParam = @InvoiceTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mInvoiceType WHERE InvoiceTypeId = @InvoiceTypeId)
				BEGIN
					UPDATE SingleIB.mInvoiceType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE InvoiceTypeId = @InvoiceTypeId;
					SET @OutParam = @InvoiceTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetPaymentType]...';


GO
CREATE PROCEDURE [SingleIB].[spSetPaymentType]
(
	@PaymentTypeId		bigint = 0,
	@PaymentTypeName	nvarchar(128) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(10),
	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.mPaymentType
					(PaymentTypeName,
					[Description],
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@PaymentTypeName, 
					@Description,
					1,
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mPaymentType WHERE PaymentTypeId = @PaymentTypeId)
				BEGIN
					UPDATE SingleIB.mPaymentType
					SET PaymentTypeName = @PaymentTypeName, 
						[Description] = @Description,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE PaymentTypeId = @PaymentTypeId;
					SET @OutParam = @PaymentTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mPaymentType WHERE PaymentTypeId = @PaymentTypeId)
				BEGIN
					UPDATE SingleIB.mPaymentType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE PaymentTypeId = @PaymentTypeId;
					SET @OutParam = @PaymentTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetProduct]...';


GO
CREATE PROCEDURE [SingleIB].[spSetProduct]
(
	@ProductId			bigint = 0,

	@ProductTypeId		bigint = 0,
	@ProductInOut		smallint = 0,
	@Brand				nvarchar(256) = NULL,
	@ProductName		nvarchar(512) = NULL,
	@ProductCode		nvarchar(256) = NULL,
	@IsGoods			bit = 1,
	@SKU				nvarchar(128) = NULL,
	@PurchasePrice		decimal(18, 5) = 0.0,
	@SalePrice			decimal(18, 5) = 0.0,
	@HSNSAC				nvarchar(8) = NULL,
	@TaxPercentage		decimal(18, 5) = 0.0,
	@Description		nvarchar(1024) = NULL,
	@IsPerishableProduct	bit = 0,
	@HasExpirationDate		bit = 0,
	@HasBatchNo				bit = 0,
	@ProductImageUrl		nvarchar(256) = NULL,
	@UnitOfMeasureId		bigint = 0,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	DECLARE @UnitOfMeasureName nvarchar(8);

	-- /* Get UnitOfMeasureName From mUnitOfMeasure Table */
	SELECT @UnitOfMeasureName = UnitOfMeasureName 
	FROM SingleIB.mUnitOfMeasure 
	WHERE UnitOfMeasureId = @UnitOfMeasureId ;
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN				
				INSERT INTO SingleIB.mProduct
					(ProductTypeId,
					ProductInOut,
					Brand,
					ProductName,
					ProductCode,
					IsGoods,
					SKU,
					PurchasePrice,
					SalePrice,
					HSNSAC,
					TaxPercentage,
					Description,
					IsPerishableProduct,
					HasExpirationDate,
					HasBatchNo,
					ProductImageUrl,
					UnitOfMeasureId,
					UnitOfMeasureName,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductTypeId, 
					@ProductInOut, 
					@Brand,
					@ProductName, 
					@ProductCode, 
					@IsGoods,
					@SKU,
					@PurchasePrice,
					@SalePrice,
					@HSNSAC,
					@TaxPercentage,
					@Description, 
					@IsPerishableProduct, 
					@HasExpirationDate,
					@HasBatchNo,
					@ProductImageUrl,
					@UnitOfMeasureId, 
					@UnitOfMeasureName,
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mProduct WHERE ProductId = @ProductId)
				BEGIN
					IF (@ProductImageUrl IS NULL OR @ProductImageUrl = '')
					BEGIN
						SELECT @ProductImageUrl = ProductImageUrl 
						FROM SingleIB.mProduct 
						WHERE ProductId = @ProductId;
					END
					UPDATE SingleIB.mProduct
					SET ProductTypeId = @ProductTypeId,
						ProductInOut = @ProductInOut, 
						Brand = @Brand,
						ProductName = @ProductName, 
						ProductCode = @ProductCode,
						IsGoods = @IsGoods,
						SKU = @SKU,
						PurchasePrice = @PurchasePrice,
						SalePrice = @SalePrice,
						HSNSAC = @HSNSAC,
						TaxPercentage = @TaxPercentage,
						Description = @Description, 
						-- IsPerishableProduct = @IsPerishableProduct,  -- Not Editable
						-- HasExpirationDate = @HasExpirationDate,  -- Not Editable
						-- HasBatchNo = @HasBatchNo,  -- Not Editable
						ProductImageUrl = @ProductImageUrl, 
						-- UnitOfMeasureId = @UnitOfMeasureId, -- Not Editable
						-- UnitOfMeasureName = @UnitOfMeasureName, -- Not Editable
						IsActive = 1,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE ProductId = @ProductId;
					SET @OutParam = @ProductId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='DEACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mProduct WHERE ProductId = @ProductId)
				BEGIN
					UPDATE SingleIB.mProduct
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE ProductId = @ProductId;
					SET @OutParam = @ProductId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetProductType]...';


GO
CREATE PROCEDURE [SingleIB].[spSetProductType]
(
	@ProductTypeId		bigint = 0,

	@ProductTypeName	nvarchar(128) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN				
				INSERT INTO SingleIB.mProductType
					(ProductTypeName,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductTypeName, 
					@Description, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mProductType WHERE ProductTypeId = @ProductTypeId)
				BEGIN
					UPDATE SingleIB.mProductType
					SET ProductTypeName = @ProductTypeName,
						Description = @Description,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE ProductTypeId = @ProductTypeId;
					SET @OutParam = @ProductTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='DEACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mProductType WHERE ProductTypeId = @ProductTypeId)
				BEGIN
					UPDATE SingleIB.mProductType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE ProductTypeId = @ProductTypeId;
					SET @OutParam = @ProductTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetPurchaseOrder]...';


GO
CREATE PROCEDURE [SingleIB].[spSetPurchaseOrder]
(
	@PurchaseOrderId	bigint = 0,

	@PurchaseTypeId		bigint = 0,	
	@BranchId			bigint = 0,
	@VendorId			bigint = 0,

	@PurchaseOrderDate		datetime = NULL,
	@PurchaseOrderNumber	nvarchar(16) = NULL,
	@AmendmentNumber		nvarchar(4) = NULL,
	@AmendmentDate			datetime = NULL,

	@DeliveryDate			datetime = NULL,

	@PurchaseOrderAmount		decimal(18,5) = 0.0,
	@PurchaseOrderTaxAmount		decimal(18,5) = 0.0,
	@PurchaseOrderTotalAmount	decimal(18,5) = 0.0,


	@TandC		nvarchar(512) = NULL,
	@Remarks	nvarchar(512) = NULL,

	@PurchaseOrderDetails		[SingleIB].[PurchaseOrderDetailType] READONLY,
	
	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),

	@OutParam			varchar(512) OUTPUT,

	/* Reqired For GRN Direct - value of inserted PurchaseOrderId */
	@IsGRNDirect		bit = 0,
	@Id					nvarchar(18) = NULL OUTPUT	
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	DECLARE @IsGstAllowed bit;
	SELECT @T1 = 'PO-ENTRY';	
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ENTRY')
			BEGIN
				/** PO Number Generate **/
				IF(@IsGRNDirect = 0)
				BEGIN
					DECLARE @NextPoNo bigint;
					SELECT @NextPoNo = (COUNT(*) + 1) FROM SingleIB.mPurchaseOrderHeader
					WHERE PurchaseOrderNumber IS NOT NULL; -- PO no will be null if Goods recev without PO
					 
					SET @PurchaseOrderNumber = @PurchaseOrderNumber + RIGHT('000000' + CAST(@NextPoNo as varchar(16)), 6);
				END
				/* Get GST Eligibility*/
				SELECT @IsGstAllowed = IsGSTRegistered 
				FROM SingleIB.mVendor
				WHERE VendorId = @VendorId;

				SET @AmendmentNumber = null;
				INSERT INTO SingleIB.mPurchaseOrderHeader
					(PurchaseTypeId,
					BranchId,
					VendorId,
					IsGstAllowed,
					PurchaseOrderDate,
					PurchaseOrderNumber,
					AmendmentNumber,
					AmendmentDate,
					DeliveryDate,
					PurchaseOrderAmount,
					PurchaseOrderTaxAmount,
					PurchaseOrderTotalAmount,
					TandC,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@PurchaseTypeId, 
					@BranchId, 
					@VendorId, 
					@IsGstAllowed,
					@PurchaseOrderDate, 
					@PurchaseOrderNumber, 
					@AmendmentNumber, 
					NULL, 
					@DeliveryDate, 
					@PurchaseOrderAmount, 
					@PurchaseOrderTaxAmount, 
					@PurchaseOrderTotalAmount, 
					@TandC, 
					@Remarks, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @PurchaseOrderId = CAST(SCOPE_IDENTITY() AS bigint);

				/* For GRN Direct */
				SET @Id = CAST(@PurchaseOrderId AS nvarchar(18));

				INSERT INTO SingleIB.mPurchaseOrderDetail
					(PurchaseOrderId,
					PurchaseOrderNumber,
					AmendmentNumber,
					ProductId,
					HSNSAC,
					Amount,
					DiscountPercentage,
					DiscountAmount,
					Price,
					Quantity,
					SubTotal,
					TaxPercentage,
					TaxAmount,
					Total,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT @PurchaseOrderId,
					@PurchaseOrderNumber,
					@AmendmentNumber,
					ProductId,
					HSNSAC,
					Amount,
					DiscountPercentage,
					DiscountAmount,
					Price,
					Quantity,
					SubTotal,
					TaxPercentage,
					TaxAmount,
					Total,
					Description,
					1 AS IsActive,
					SingleIB.ISTNow() AS TransactionDate,
					@RequestId
				FROM @PurchaseOrderDetails

				SET @OutParam = '{ "PurchaseOrderId" : ' +
							CAST(@PurchaseOrderId as VARCHAR) +
							', "PurchaseOrderNumber" : "' +
							CAST(@PurchaseOrderNumber AS VARCHAR) +
							'" }';
			END
			ELSE IF (@QueryType ='DELETE')
			BEGIN
				IF (NOT EXISTS(SELECT * FROM [SingleIB].[mGRNHeader] WHERE PurchaseOrderId = @PurchaseOrderId AND IsActive = 1)
					AND NOT EXISTS(SELECT * FROM [SingleIB].[mBillHeader] WHERE PurchaseOrderId = @PurchaseOrderId AND IsActive = 1))
				BEGIN
					UPDATE SingleIB.mPurchaseOrderHeader
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE PurchaseOrderId = @PurchaseOrderId;

					UPDATE SingleIB.mPurchaseOrderDetail
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE PurchaseOrderId = @PurchaseOrderId;
					SET @OutParam = @PurchaseOrderId;
				END
				ELSE
				BEGIN
					SET @OutParam = '-1';
				END
			END
			ELSE IF (@QueryType ='AMENDMENT')
			BEGIN
				-- AmendmentNumber Preparation
				DECLARE @NextAmNo bigint;
				SELECT @NextAmNo = (ISNULL(AmendmentNumber, 0) + 1) FROM SingleIB.mPurchaseOrderHeader
				WHERE PurchaseOrderId = @PurchaseOrderId;					
				SET @AmendmentNumber = @AmendmentNumber + CAST(@NextAmNo as varchar(16));
				
				INSERT tAmendmentTrack
					(AmendmentOf,
					AmendmentObjectId,
					OldAmendmentNumber,
					AmendmentNumber,
					HeaderJson,
					DetailJson,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					('PO',
					@PurchaseOrderId,
					(SELECT AmendmentNumber as OldAmendmentNumber FROM mPurchaseOrderHeader WHERE PurchaseOrderId = @PurchaseOrderId),
					@AmendmentNumber,
					(SELECT (SELECT * FROM mPurchaseOrderHeader WHERE PurchaseOrderId = @PurchaseOrderId FOR JSON PATH) as HeaderJson),
					(SELECT (SELECT * FROM mPurchaseOrderDetail WHERE PurchaseOrderId = @PurchaseOrderId FOR JSON PATH) as DetailJson),
					1,
					SingleIB.ISTNow(),
					@RequestId);

				-- Update Header
				UPDATE SingleIB.mPurchaseOrderHeader
					SET AmendmentNumber = @AmendmentNumber,
						AmendmentDate = @AmendmentDate,
						PurchaseOrderAmount = @PurchaseOrderAmount,
						PurchaseOrderTaxAmount = @PurchaseOrderTaxAmount,
						PurchaseOrderTotalAmount = @PurchaseOrderTotalAmount,
						TandC = @TandC,
						Remarks = @Remarks,
						IsActive = 1,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE PurchaseOrderId = @PurchaseOrderId;

				-- Update Detail
				DECLARE @Counter int, @MaxId int;
				SELECT @Counter = min(Id) , @MaxId = max(Id) FROM @PurchaseOrderDetails;
				WHILE(@Counter <= @MaxId)
				BEGIN
					UPDATE mPurchaseOrderDetail
					SET AmendmentNumber = @AmendmentNumber,
						Amount = (SELECT TOP 1 Amount FROM @PurchaseOrderDetails WHERE Id = @Counter),
						DiscountPercentage = (SELECT TOP 1 DiscountPercentage FROM @PurchaseOrderDetails WHERE Id = @Counter),
						DiscountAmount = (SELECT TOP 1 DiscountAmount FROM @PurchaseOrderDetails WHERE Id = @Counter),
						Price = (SELECT TOP 1 Price FROM @PurchaseOrderDetails WHERE Id = @Counter),
						Quantity = (SELECT TOP 1 Quantity FROM @PurchaseOrderDetails WHERE Id = @Counter),
						SubTotal = (SELECT TOP 1 SubTotal FROM @PurchaseOrderDetails WHERE Id = @Counter),
						TaxPercentage = (SELECT TOP 1 TaxPercentage FROM @PurchaseOrderDetails WHERE Id = @Counter),
						TaxAmount = (SELECT TOP 1 TaxAmount FROM @PurchaseOrderDetails WHERE Id = @Counter),
						Total = (SELECT TOP 1 Total FROM @PurchaseOrderDetails WHERE Id = @Counter),
						IsActive = 1,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE PurchaseOrderId = @PurchaseOrderId
						AND PurchaseOrderDetailId = (SELECT TOP 1 PurchaseOrderDetailId FROM @PurchaseOrderDetails WHERE Id = @Counter);
									
					SET @Counter  = @Counter  + 1;
				END
				-- Check Full Received or not
				IF(SingleIB.IsFullReceived(@PurchaseOrderId) <> 1)
				BEGIN
					UPDATE SingleIB.mGRNHeader
					SET IsFullReceived = 0
					WHERE PurchaseOrderId = @PurchaseOrderId;
				END
				-- Check Full Billed or not
				IF(SingleIB.IsFullBilled(@PurchaseOrderId) <> 1)
				BEGIN
					UPDATE SingleIB.mBillHeader
					SET IsFullBilled = 0
					WHERE PurchaseOrderId = @PurchaseOrderId;
				END
				--Return
				SET @OutParam = '{ "PurchaseOrderId" : ' + 
							CAST(@PurchaseOrderId as VARCHAR) +
							', "AmendmentNumber" : "' +
							CAST(@AmendmentNumber AS VARCHAR) +
							'" }';
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetPurchaseType]...';


GO
CREATE PROCEDURE [SingleIB].[spSetPurchaseType]
(
	@PurchaseTypeId		int = 0,

	@PurchaseTypeName	nvarchar(128) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN				
				INSERT INTO SingleIB.mPurchaseType
					(PurchaseTypeName,
					[Description],
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@PurchaseTypeName, 
					@Description, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mPurchaseType WHERE PurchaseTypeId = @PurchaseTypeId)
				BEGIN
					UPDATE SingleIB.mPurchaseType
					SET PurchaseTypeName = @PurchaseTypeName,
						Description = @Description,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE PurchaseTypeId = @PurchaseTypeId;
					SET @OutParam = @PurchaseTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mPurchaseType WHERE PurchaseTypeId = @PurchaseTypeId)
				BEGIN
					UPDATE SingleIB.mPurchaseType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE PurchaseTypeId = @PurchaseTypeId;
					SET @OutParam = @PurchaseTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetQuotation]...';


GO
CREATE PROCEDURE [SingleIB].[spSetQuotation]
(	
	@QuotationId		bigint = 0,

	@ComapnyIsGSTRegistered		bit = 0,

	@CustomerId			bigint = 0,	

	@QuotationDate			datetime = NULL,
	@QuotationNumber		nvarchar(16) = NULL,

	@QuotationAmount		decimal(18,5) = 0.0,
	@QuotationTaxAmount		decimal(18,5) = 0.0,
	@QuotationTotalAmount	decimal(18,5) = 0.0,


	@TandC		text = NULL,
	@Remarks	nvarchar(512) = NULL,

	@QuotationDetails		[SingleIB].[QuotationDetailType] READONLY,
	
	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(512) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Quotation';
	DECLARE @IsGstAllowed bit;
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ENTRY')
			BEGIN
				DECLARE @NextQoNo bigint;
				SELECT @NextQoNo = (COUNT(*) + 1) FROM SingleIB.mQuotationHeader; 

				SET @QuotationNumber = @QuotationNumber + RIGHT('000000' + CAST(@NextQoNo as varchar(16)), 6);
				
				/* Get GST Eligibility*/
				SET @IsGstAllowed = @ComapnyIsGSTRegistered;

				INSERT INTO SingleIB.mQuotationHeader
					(CustomerId,
					IsGstAllowed,
					QuotationDate,
					QuotationNumber,
					QuotationAmount,
					QuotationTaxAmount,
					QuotationTotalAmount,
					TandC,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@CustomerId, 
					@IsGstAllowed,
					@QuotationDate, 
					@QuotationNumber, 
					@QuotationAmount, 
					@QuotationTaxAmount, 
					@QuotationTotalAmount, 
					@TandC, 
					@Remarks, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @QuotationId = CAST(SCOPE_IDENTITY() AS bigint);
								
				INSERT INTO SingleIB.mQuotationDetail
					(QuotationId,
					QuotationNumber,
					ProductId,
					HSNSAC,
					Amount,
					DiscountPercentage,
					DiscountAmount,
					Price,
					Quantity,
					SubTotal,
					TaxPercentage,
					TaxAmount,
					Total,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT @QuotationId,
					@QuotationNumber,
					ProductId,
					HSNSAC,
					Amount,
					DiscountPercentage,
					DiscountAmount,
					Price,
					Quantity,
					SubTotal,
					TaxPercentage,
					TaxAmount,
					Total,
					Description,
					1 AS IsActive,
					SingleIB.ISTNow() AS TransactionDate,
					@RequestId
				FROM @QuotationDetails 
				SET @OutParam = '{ "QuotationId" : ' +
							CAST(@QuotationId as VARCHAR) +
							', "QuotationNumber" : "' +
							CAST(@QuotationNumber AS VARCHAR) +
							'" }';
			END
			ELSE IF (@QueryType ='DELETE')
			BEGIN
				IF (NOT EXISTS(SELECT * FROM [SingleIB].[mSalesOrderHeader] 
								WHERE QuotationId = @QuotationId AND IsActive = 1))
				BEGIN
					UPDATE SingleIB.mQuotationHeader
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE QuotationId = @QuotationId;

					UPDATE SingleIB.mQuotationDetail
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE QuotationId = @QuotationId;
					SET @OutParam = @QuotationId;
				END
				ELSE
				BEGIN
					SET @OutParam = '-1';
				END
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetSalesOrder]...';


GO
CREATE PROCEDURE [SingleIB].[spSetSalesOrder]
(	
	@SalesOrderId		bigint = 0,

	@ComapnyIsGSTRegistered		bit = 0,

	@QuotationId		bigint = 0,
	@SalesTypeId		bigint = 0,	
	@BranchId			bigint = 0,
	@CustomerId			bigint = 0,

	@SalesOrderDate		datetime = NULL,
	@SalesOrderNumber	nvarchar(16) = NULL,
	@AmendmentNumber		nvarchar(4) = NULL,
	@AmendmentDate			datetime = NULL,

	@CustomerPONumber		nvarchar(128) = NULL,
	@CustomerPODate			datetime = NULL,
	@DeliveryDate			datetime = NULL,

	@SalesOrderAmount		decimal(18,5) = 0.0,
	@SalesOrderTaxAmount	decimal(18,5) = 0.0,
	@SalesOrderTotalAmount	decimal(18,5) = 0.0,


	@TandC		nvarchar(512) = NULL,
	@Remarks	nvarchar(512) = NULL,

	@SalesOrderDetails	[SingleIB].[SalesOrderDetailType] READONLY,
	
	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(512) OUTPUT,

	/* Reqired For Shipment Direct - value of inserted SalesOrderId */
	@IsShipmentDirect		bit = 0,
	@Id						nvarchar(18) = NULL OUTPUT	
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	DECLARE @IsGstAllowed bit;
	SELECT @T1 = 'SO';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='ENTRY')
			BEGIN
				IF(@IsShipmentDirect = 0)
				BEGIN
					DECLARE @NextSoNo bigint;
					SELECT @NextSoNo = (COUNT(*) + 1) FROM SingleIB.mSalesOrderHeader
					WHERE SalesOrderNumber IS NOT NULL; -- SO no will be null if Goods ship without SO

					SET @SalesOrderNumber = @SalesOrderNumber + RIGHT('000000' + CAST(@NextSoNo as varchar(16)), 6);
				END
				
				/* Get GST Eligibility*/
				SET @IsGstAllowed = @ComapnyIsGSTRegistered;

				SET @AmendmentNumber = null;
				INSERT INTO SingleIB.mSalesOrderHeader
					(QuotationId,
					SalesTypeId,					
					BranchId,
					CustomerId,
					IsGstAllowed,
					SalesOrderDate,
					SalesOrderNumber,
					AmendmentNumber,
					AmendmentDate,
					CustomerPONumber, 
					CustomerPODate, 
					DeliveryDate,
					SalesOrderAmount,
					SalesOrderTaxAmount,
					SalesOrderTotalAmount,
					TandC,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@QuotationId,
					@SalesTypeId,
					@BranchId, 
					@CustomerId, 
					@IsGstAllowed,
					@SalesOrderDate, 
					@SalesOrderNumber, 
					@AmendmentNumber, 
					NULL, 
					@CustomerPONumber, 
					@CustomerPODate, 
					@DeliveryDate, 
					@SalesOrderAmount, 
					@SalesOrderTaxAmount, 
					@SalesOrderTotalAmount, 
					@TandC, 
					@Remarks, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @SalesOrderId = CAST(SCOPE_IDENTITY() AS bigint);

				/* For Shipment Direct */
				SET @Id = CAST(@SalesOrderId AS nvarchar(18));

				INSERT INTO SingleIB.mSalesOrderDetail
					(SalesOrderId,
					SalesOrderNumber,
					AmendmentNumber,
					ProductId,
					HSNSAC,
					Amount,
					DiscountPercentage,
					DiscountAmount,
					Price,
					Quantity,
					SubTotal,
					TaxPercentage,
					TaxAmount,
					Total,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT @SalesOrderId,
					@SalesOrderNumber,
					@AmendmentNumber,
					ProductId,
					HSNSAC,
					Amount,
					DiscountPercentage,
					DiscountAmount,
					Price,
					Quantity,
					SubTotal,
					TaxPercentage,
					TaxAmount,
					Total,
					Description,
					1 AS IsActive,
					SingleIB.ISTNow() AS TransactionDate,
					@RequestId
				FROM @SalesOrderDetails 
				SET @OutParam = '{ "SalesOrderId" : ' +
							CAST(@SalesOrderId as VARCHAR) +
							', "SalesOrderNumber" : "' +
							CAST(@SalesOrderNumber AS VARCHAR) +
							'" }';
			END
			ELSE IF (@QueryType ='DELETE')
			BEGIN
				IF (NOT EXISTS(SELECT * FROM [SingleIB].[mShipmentHeader] WHERE SalesOrderId = @SalesOrderId AND IsActive = 1)
					AND NOT EXISTS(SELECT * FROM [SingleIB].[mInvoiceHeader] WHERE SalesOrderId = @SalesOrderId AND IsActive = 1))
				BEGIN
					UPDATE SingleIB.mSalesOrderHeader
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE SalesOrderId = @SalesOrderId;

					UPDATE SingleIB.mSalesOrderDetail
					SET IsActive = 0,
						RequestId = @RequestId
					WHERE SalesOrderId = @SalesOrderId;
					SET @OutParam = @SalesOrderId;
				END
				ELSE
				BEGIN
					SET @OutParam = '-1';
				END
			END
			ELSE IF (@QueryType ='AMENDMENT')
			BEGIN
				-- AmendmentNumber Preparation
				DECLARE @NextAmNo bigint;
				SELECT @NextAmNo = (ISNULL(AmendmentNumber, 0) + 1) FROM SingleIB.mSalesOrderHeader
				WHERE SalesOrderId = @SalesOrderId;					
				SET @AmendmentNumber = @AmendmentNumber + CAST(@NextAmNo as varchar(16));
				
				INSERT tAmendmentTrack
					(AmendmentOf,
					AmendmentObjectId,
					OldAmendmentNumber,
					AmendmentNumber,
					HeaderJson,
					DetailJson,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					('SO',
					@SalesOrderId,
					(SELECT AmendmentNumber as OldAmendmentNumber FROM mSalesOrderHeader WHERE SalesOrderId = @SalesOrderId),
					@AmendmentNumber,
					(SELECT (SELECT * FROM mSalesOrderHeader WHERE SalesOrderId = @SalesOrderId FOR JSON PATH) as HeaderJson),
					(SELECT (SELECT * FROM mSalesOrderDetail WHERE SalesOrderId = @SalesOrderId FOR JSON PATH) as DetailJson),
					1,
					SingleIB.ISTNow(),
					@RequestId);
					
				-- Update Header
				UPDATE SingleIB.mSalesOrderHeader
					SET AmendmentNumber = @AmendmentNumber,
						AmendmentDate = @AmendmentDate,
						SalesOrderAmount = @SalesOrderAmount,
						SalesOrderTaxAmount = @SalesOrderTaxAmount,
						SalesOrderTotalAmount = @SalesOrderTotalAmount,
						TandC = @TandC,
						Remarks = @Remarks,
						IsActive = 1,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE SalesOrderId = @SalesOrderId;			

				-- Update Detail
				DECLARE @Counter int, @MaxId int;
				SELECT @Counter = min(Id) , @MaxId = max(Id) FROM @SalesOrderDetails;
				WHILE(@Counter <= @MaxId)
				BEGIN
					UPDATE mSalesOrderDetail
					SET AmendmentNumber = @AmendmentNumber,
						Amount = (SELECT TOP 1 Amount FROM @SalesOrderDetails WHERE Id = @Counter),
						DiscountPercentage = (SELECT TOP 1 DiscountPercentage FROM @SalesOrderDetails WHERE Id = @Counter),
						DiscountAmount = (SELECT TOP 1 DiscountAmount FROM @SalesOrderDetails WHERE Id = @Counter),
						Price = (SELECT TOP 1 Price FROM @SalesOrderDetails WHERE Id = @Counter),
						Quantity = (SELECT TOP 1 Quantity FROM @SalesOrderDetails WHERE Id = @Counter),
						SubTotal = (SELECT TOP 1 SubTotal FROM @SalesOrderDetails WHERE Id = @Counter),
						TaxPercentage = (SELECT TOP 1 TaxPercentage FROM @SalesOrderDetails WHERE Id = @Counter),
						TaxAmount = (SELECT TOP 1 TaxAmount FROM @SalesOrderDetails WHERE Id = @Counter),
						Total = (SELECT TOP 1 Total FROM @SalesOrderDetails WHERE Id = @Counter),
						IsActive = 1,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE SalesOrderId = @SalesOrderId
						AND SalesOrderDetailId = (SELECT TOP 1 SalesOrderDetailId FROM @SalesOrderDetails WHERE Id = @Counter);
									
					SET @Counter  = @Counter  + 1;
				END

				-- Check Full Shipped or not
				IF(SingleIB.IsFullShipped(@SalesOrderId) <> 1)
				BEGIN
					UPDATE SingleIB.mShipmentHeader
					SET IsFullShipped = 0
					WHERE SalesOrderId = @SalesOrderId;
				END
				-- Check Full Invoiced or not
				IF(SingleIB.IsFullInvoiced(@SalesOrderId) <> 1)
				BEGIN
					UPDATE SingleIB.mInvoiceHeader
					SET IsFullInvoiced = 0
					WHERE SalesOrderId = @SalesOrderId;
				END
				SET @OutParam = '{ "SalesOrderId" : ' + 
							CAST(@SalesOrderId as VARCHAR) +
							', "AmendmentNumber" : "' +
							CAST(@AmendmentNumber AS VARCHAR) +
							'" }';
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetSalesType]...';


GO
CREATE PROCEDURE [SingleIB].[spSetSalesType]
(
	@SalesTypeId		int = 0,

	@SalesTypeName		nvarchar(128) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN				
				INSERT INTO SingleIB.mSalesType
					(SalesTypeName,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@SalesTypeName, 
					@Description, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mSalesType WHERE SalesTypeId = @SalesTypeId)
				BEGIN
					UPDATE SingleIB.mSalesType
					SET SalesTypeName = @SalesTypeName,
						Description = @Description,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE SalesTypeId = @SalesTypeId;
					SET @OutParam = @SalesTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mSalesType WHERE SalesTypeId = @SalesTypeId)
				BEGIN
					UPDATE SingleIB.mSalesType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE SalesTypeId = @SalesTypeId;
					SET @OutParam = @SalesTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetShipmentType]...';


GO
CREATE PROCEDURE [SingleIB].[spSetShipmentType]
(
	@ShipmentTypeId		bigint = 0,
	@ShipmentTypeName	nvarchar(128) = NULL,
	@Description		nvarchar(256) = NULL,
	
	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(10),
	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.mShipmentType
					(ShipmentTypeName,
					[Description],
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ShipmentTypeName, 
					@Description,
					1,
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mShipmentType WHERE ShipmentTypeId = @ShipmentTypeId)
				BEGIN
					UPDATE SingleIB.mShipmentType
					SET ShipmentTypeName = @ShipmentTypeName, 
						[Description] = @Description,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE ShipmentTypeId = @ShipmentTypeId;
					SET @OutParam = @ShipmentTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mShipmentType WHERE ShipmentTypeId = @ShipmentTypeId)
				BEGIN
					UPDATE SingleIB.mShipmentType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE ShipmentTypeId = @ShipmentTypeId;
					SET @OutParam = @ShipmentTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetSmsTrack]...';


GO
CREATE PROCEDURE [SingleIB].[spSetSmsTrack]
(
	@UserEmail			nvarchar(64) = NULL,

	@ComponentIdentity	nvarchar(64) = NULL,	
	@UrlLink			nvarchar(128) = NULL,
	@SmsContent			nvarchar(512) = NULL,
	@MobileNo			nvarchar(16) = NULL,
	@IsMobileNoChanged	bit = 0,
	@IsResend			bit = 0,
	@SmsFor				varchar(64) = NULL,

	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(16),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'sms';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				UPDATE SingleIB.tSmsTrack
				SET IsActive = 0
				WHERE ComponentIdentity = @ComponentIdentity
					AND SmsFor = @SmsFor
					AND IsActive = 1;

				INSERT INTO SingleIB.tSmsTrack
					(UserName,
					ComponentIdentity,
					SendTime,
					UrlLink,
					SmsContent,
					MobileNo,
					IsMobileNoChanged,
					IsResend,
					SmsFor,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@UserEmail, 
					@ComponentIdentity, 
					SingleIB.IstNow(), 
					@UrlLink, 
					@SmsContent, 
					@MobileNo, 
					@IsMobileNoChanged, 
					@IsResend, 
					@SmsFor, 
					1, 
					SingleIB.IstNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetStock]...';


GO

CREATE PROCEDURE [SingleIB].[spSetStock]
(
	@ProductId			bigint = 0,
	@WareHouseId		bigint = 0,
	@FromWareHouseId	bigint = 0,
	@Quantity			decimal(18, 5) = 0.0,
	@Remarks			nvarchar(512) = NULL,
	@GRNOrShipmentDate	datetime = null,
	@GRNOrShipmentId	bigint = 0,

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),

	@OutParam			nvarchar(32) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	DECLARE @CurrentStock decimal(18, 5) = 0.0;
	SELECT @T1 = 'SET';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='GRNADD')
			BEGIN
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM SingleIB.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock + @Quantity;

				-- Update Old Row
				UPDATE  SingleIB.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO SingleIB.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					1,
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='SHIPSUB')
			BEGIN
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM SingleIB.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock - @Quantity;

				-- Update Old Row
				UPDATE  SingleIB.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO SingleIB.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					-1,
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='SALESRTN')
			BEGIN
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM SingleIB.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock + @Quantity;

				-- Update Old Row
				UPDATE  SingleIB.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO SingleIB.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					2,
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='PURCHRTN')
			BEGIN
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM SingleIB.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock - @Quantity;

				-- Update Old Row
				UPDATE  SingleIB.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO SingleIB.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					-2,
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF (@QueryType ='TRNSFR')
			BEGIN
				-- Calculate Stock of FromWareHouse
				SELECT @CurrentStock = CurrentStock
				FROM SingleIB.tStock as stk
				WHERE stk.WareHouseId = @FromWareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock - @Quantity;

				-- Update Old Row of FromWareHouse
				UPDATE  SingleIB.tStock SET IsActive = 0
				WHERE WareHouseId = @FromWareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row of FromWareHouse
				INSERT INTO SingleIB.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@FromWareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					0,
					1, 
					SingleIB.ISTNow(), 
					@RequestId)

				-- Calculate Stock of FromWareHouse
				SET @CurrentStock = 0.0;
				SELECT @CurrentStock = CurrentStock
				FROM SingleIB.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock + @Quantity;

				-- Update Old Row of FromWareHouse
				UPDATE  SingleIB.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row of FromWareHouse
				INSERT INTO SingleIB.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					0,
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF (@QueryType ='OPENING')
			BEGIN
				-- SAME WITH GRNADD, HERE GRNOrShipmentId = NULL & StockType = 10 (OPENING)
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM SingleIB.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock + @Quantity;

				-- Update Old Row
				UPDATE  SingleIB.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO SingleIB.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					NULL,
					10,
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF (@QueryType ='ADJUSTADD')
			BEGIN
				-- SAME WITH GRNADD, HERE GRNOrShipmentId = NULL & StockType = 20 (AdjustIncrease)
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM SingleIB.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock + @Quantity;

				-- Update Old Row
				UPDATE  SingleIB.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO SingleIB.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					20,
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF (@QueryType ='ADJUSTSUB')
			BEGIN
				-- SAME WITH SHIPSUB, HERE GRNOrShipmentId = NULL & StockType = -20 (AdjustDecrease)
				-- Calculate Stock
				SELECT @CurrentStock = CurrentStock
				FROM SingleIB.tStock as stk
				WHERE stk.WareHouseId = @WareHouseId
					AND stk.ProductId = @ProductId
					AND stk.IsActive = 1;
				IF(@CurrentStock IS NULL)
				BEGIN
					SET @CurrentStock = 0.0;
				END
				SET @CurrentStock = @CurrentStock - @Quantity;

				-- Update Old Row
				UPDATE  SingleIB.tStock SET IsActive = 0
				WHERE WareHouseId = @WareHouseId
					AND ProductId = @ProductId
					AND IsActive = 1;

				-- Insert New Row
				INSERT INTO SingleIB.tStock
					(ProductId,
					WareHouseId,
					CurrentStock,
					Remarks,
					GRNOrShipmentDate,
					GRNOrShipmentId,
					StockType,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@ProductId, 
					@WareHouseId, 
					@CurrentStock, 
					@Remarks,
					@GRNOrShipmentDate,
					@GRNOrShipmentId,
					-20,
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @OutParam = SCOPE_IDENTITY();
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetStockAdjustReason]...';


GO
CREATE PROCEDURE [SingleIB].[spSetStockAdjustReason]
(
	@AdjustReason		nvarchar(64) = NULL,

	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(10),
	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Insert';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.sStockAdjustReason
					(AdjustReason,
					IsDefault,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@AdjustReason, 
					0, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId)

				SET @OutParam = SCOPE_IDENTITY();
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetStockDetail]...';


GO


CREATE PROCEDURE [SingleIB].[spSetStockDetail]
(
	@StockDetailId		bigint = 0,		-- Pass value for decrease and trnsfr Stock

	@StockId			bigint = 0,
	@ProductId			bigint = 0,
	@WareHouseId		bigint = 0,
	@Stock				decimal(18,5) = 0.0,
	@IsPerishable		bit = 0,
	@ExpiryDate			datetime = null,
	@BatchNo			nvarchar(256) = null,
	@Remarks			nvarchar(512) = NULL,
	@GRNDate			datetime = null,

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),

	@OutParam			nvarchar(32) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'SET';
	DECLARE @CurrentStock decimal(18, 5) = 0.0;
	DECLARE @IsActive bit = 1;
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@Stock IS NOT NULL AND @Stock <> 0)
			BEGIN
				PRINT(1);
				IF (@QueryType ='ADD')
				BEGIN
					PRINT(2);
					-- (IF) Product Either Perishable or has Batch No or has Expiration Date
					IF EXISTS(SELECT * FROM SingleIB.mProduct 
									WHERE (ProductId = @ProductId )
										AND (IsPerishableProduct = 1
											OR HasBatchNo = 1
											OR HasExpirationDate = 1))
					BEGIN
						PRINT(3);
						/* -- This block is to find Same BatchNo and/or ExpiryDate of a Product is EXISTS or not
						-- Need to recheck 
						DECLARE @PrevExpiryDate datetime = null,
								@PrevBatchNo nvarchar(256) = null;
						SELECT @StockDetailId = StockDetailId, 
								@CurrentStock = Stock,
								@PrevExpiryDate = ExpiryDate,
								@PrevBatchNo = BatchNo
						FROM SingleIB.tStockDetail
						WHERE WareHouseId = @WareHouseId 
							AND ProductId = @ProductId;
						-- Add New Row for every Perishable Products
						IF((SELECT IsPerishableProduct FROM SingleIB.mProduct WHERE ProductId = @ProductId) = 1)
						BEGIN
							PRINT(3.1);
							SET @StockDetailId = 0.0;
						END
						-- Add New Row if ExpiryDate differs from Previous ExpiryDate
						IF(@PrevExpiryDate <> @ExpiryDate)
						BEGIN
							PRINT(3.2);
							SET @StockDetailId = 0.0;
						END
						-- Add New Row if ExpiryDate differs from Previous ExpiryDate
						IF(@PrevBatchNo <> @BatchNo)
						BEGIN
							PRINT(3.3);
							SET @StockDetailId = 0.0;
						END
						*/

						-- (IF) Stocked Product is Perishable or does not have same BatchNo and/or ExpiryDate - INSERT NEW ROW
						IF(@StockDetailId IS NULL OR @StockDetailId = 0)
						BEGIN
							PRINT(4);
							SET @CurrentStock = 0.0;
							SET @Stock = @Stock + @CurrentStock;
							INSERT INTO SingleIB.tStockDetail
								(StockId,
								ProductId,
								WareHouseId,
								Stock,
								IsPerishable,
								ExpiryDate,
								BatchNo,
								Remarks,
								GRNDate,
								IsActive,
								TransactionDate,
								RequestId)
							VALUES
								(@StockId,@ProductId, @WareHouseId, @Stock, @IsPerishable,
								@ExpiryDate, @BatchNo, @Remarks, @GRNDate, 1, SingleIB.ISTNow(), @RequestId);
							SET @OutParam = SCOPE_IDENTITY();
						END
						-- (ELSE) Stocked Product is Not Perishable or does have same BatchNo and/or ExpiryDate - UPDATE ROW
						-- According to this script's logic this else block will never execute. Need to do it later
						ELSE
						BEGIN
							PRINT(5);
							SET @Stock = @Stock + @CurrentStock;
							UPDATE SingleIB.tStockDetail
							SET Stock = @Stock,
								GRNDate = @GRNDate,
								StockId = @StockId,
								Remarks = @Remarks,
								IsActive = 1,
								TransactionDate = SingleIB.ISTNow(),
								RequestId = @RequestId
							WHERE StockDetailId = @StockDetailId
							SET @OutParam = @StockDetailId;
						END
					END
					-- (ELSE) Product Nither Perishable nor has Batch No nor has Expiration Date
					ELSE
					BEGIN
						PRINT(6);
						SELECT @StockDetailId = StockDetailId, 
								@CurrentStock = Stock
						FROM SingleIB.tStockDetail
						WHERE WareHouseId = @WareHouseId
							AND ProductId = @ProductId;

						-- (IF) Stocked Product is not available - INSERT NEW ROW
						PRINT(@StockDetailId);
						IF(@StockDetailId IS NULL OR @StockDetailId = 0)
						BEGIN
							PRINT(7);
							SET @CurrentStock = 0.0;
							SET @Stock = @Stock + @CurrentStock;
							INSERT INTO SingleIB.tStockDetail
								(StockId,
								ProductId,
								WareHouseId,
								Stock,
								IsPerishable,
								ExpiryDate,
								BatchNo,
								Remarks,
								GRNDate,
								IsActive,
								TransactionDate,
								RequestId)
							VALUES
								(@StockId,@ProductId, @WareHouseId, @Stock, @IsPerishable,
								@ExpiryDate, @BatchNo, @Remarks, @GRNDate, 1, SingleIB.ISTNow(), @RequestId);
							SET @OutParam = SCOPE_IDENTITY();
						END
						-- ELSE) Stocked Product is available - UPDATE ROW
						ELSE
						BEGIN
							PRINT(8);
							SET @Stock = @Stock + @CurrentStock;
							UPDATE SingleIB.tStockDetail
							SET Stock = @Stock,
								GRNDate = @GRNDate,
								StockId = @StockId,
								Remarks = @Remarks,
								IsActive = 1,
								TransactionDate = SingleIB.ISTNow(),
								RequestId = @RequestId
							WHERE StockDetailId = @StockDetailId
							SET @OutParam = @StockDetailId;
						END
					END
				END
				ELSE IF(@QueryType = 'SUB')
				BEGIN
					SELECT @CurrentStock = Stock
					FROM SingleIB.tStockDetail
					WHERE StockDetailId = @StockDetailId;
				
					SET @Stock = @CurrentStock - @Stock;
					IF(@Stock = 0.0)
					BEGIN
						SET @IsActive = 0;
					END
					IF(@Stock < 0.0)
					BEGIN
						-- Negative stock can't be inserted, 
						-- Throw an error to roll back the entire transction
						SELECT 1 / 0 AS Error; 
					END

					UPDATE SingleIB.tStockDetail
					SET Stock = @Stock,
						Remarks = @Remarks,
						IsActive = @IsActive,
						StockId = @StockId,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE StockDetailId = @StockDetailId
					SET @OutParam = @StockDetailId;
				END
				ELSE IF(@QueryType = 'TRNSFR')
				BEGIN
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = @StockDetailId,
						@StockId = @StockId,
						@Stock = @Stock,
						@Remarks = @Remarks,
						@RequestId = @RequestId,
						@QueryType = 'SUB',
						@OutParam = @OutParam OUTPUT;

					SELECT @GRNDate = GRNDate FROM SingleIB.tStockDetail WHERE StockDetailId = @StockDetailId;
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = 0,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Stock = @Stock,
						@IsPerishable = @IsPerishable,
						@ExpiryDate = @ExpiryDate,
						@BatchNo = @BatchNo,
						@Remarks = @Remarks,
						@GRNDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = 'ADD',
						@OutParam = @OutParam OUTPUT;						
				END
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetStockTransfer]...';


GO

CREATE PROCEDURE [SingleIB].[spSetStockTransfer]
(
	@FromWareHouseId		bigint = 0,
	@ToWareHouseId			bigint = 0,
	@StockTransferNumber	nvarchar(16) = NULL,
	@StockTransferDate		datetime = NULL,
	@Remarks			nvarchar(512) = NULL,
	        
	@STDetail [SingleIB].[StockTransferDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@StockTransferId		bigint,
		@NextSTNo				bigint;

	SELECT @T1 = 'STKTRNSFR';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** STK Number Generate **/
				SELECT @NextSTNo = (COUNT(*) + 1) FROM SingleIB.tStockTransferHeader;

				SET @StockTransferNumber = @StockTransferNumber + RIGHT('000000' + CAST(@NextSTNo as varchar(16)), 6);				

				/** INSERT INTO tStockTransferHeader **/
				INSERT INTO SingleIB.tStockTransferHeader
					(StockTransferNumber,
					StockTransferDate,
					FromWareHouseId,
					ToWareHouseId,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@StockTransferNumber, 
					@StockTransferDate, 
					@FromWareHouseId, 
					@ToWareHouseId, 
					@Remarks, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @StockTransferId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO tStockTransferDetail **/
				INSERT INTO SingleIB.tStockTransferDetail
					(StockTransferId,
					StockTransferNumber,
					ProductId,
					AvailableQuantity,
					TransferQuantity,
					StockDetailId,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@StockTransferId,
					@StockTransferNumber,
					ProductId,
					AvailableQuantity,
					TransferQuantity,
					StockDetailId,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					1,
					SingleIB.ISTNow(),
					@RequestId
				FROM @STDetail;

				/** UPDATE STOCK **/
				DECLARE @Counter INT , @MaxId INT;
				DECLARE @StockId bigint;

				SELECT @Counter = min(Id) , @MaxId = max(Id) 
				FROM @STDetail
				DECLARE @ProductId bigint, 
					@AvailableQuantity decimal(18, 5),
					@TransferQuantity decimal(18, 5), 
					@Description nvarchar(512),
					@StockDetailId bigint,
					@IsPerishable bit,
					@ExpiryDate datetime,
					@BatchNo nvarchar(256);

				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN					
					SELECT @ProductId = ProductId, 
						@AvailableQuantity = AvailableQuantity, 
						@TransferQuantity = TransferQuantity,
						@Description = Description,
						@StockDetailId = StockDetailId,
						@IsPerishable = IsPerishable,
						@ExpiryDate = ExpiryDate,
						@BatchNo = BatchNo
					FROM @STDetail WHERE Id = @Counter;

					-- Stock
					EXEC SingleIB.spSetStock						
						@ProductId = @ProductId,
						@FromWareHouseId = @FromWareHouseId,
						@WareHouseId = @ToWareHouseId,
						@Quantity = @TransferQuantity,
						@Remarks = @Description,
						@GRNOrShipmentId = @StockTransferId,
						@GRNOrShipmentDate = @StockTransferDate,
						@RequestId = @RequestId,
						@QueryType = 'TRNSFR',
						@OutParam = @StockId OUTPUT;

					-- Stock Detail
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = @StockDetailId,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @ToWareHouseId,
						@Stock = @TransferQuantity,
						@IsPerishable = @IsPerishable,
						@ExpiryDate = @ExpiryDate,
						@BatchNo = @BatchNo,
						@Remarks = @Description,
						@GRNDate = NULL,
						@RequestId = @RequestId,
						@QueryType = 'TRNSFR',
						@OutParam = @OutParam OUTPUT;

				   SET @Counter  = @Counter  + 1        
				END
				SET @OutParam = '{ "StockTransferId" : ' +
							CAST(@StockTransferId as VARCHAR) +
							', "StockTransferNumber" : "' +
							CAST(@StockTransferNumber AS VARCHAR) +
							'" }';
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetTDSPay]...';


GO

CREATE PROCEDURE [SingleIB].[spSetTDSPay]
(
	@PaymentOutId		bigint = 0,
	@VendorId			bigint = 0,
	
	@TDSAmount			decimal(18, 5) = NULL,

	-- When This amout will pay to IncomeTax againest TAN
	@TDSPayId			bigint = 0,
	@PayDate			datetime = null,
	-- End

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				DECLARE @PANNumber nvarchar(16);
				SELECT @PANNumber = PANNumber 
				FROM SingleIB.mVendor 
				WHERE VendorId = @VendorId;
				IF(@TDSAmount IS NOT NULL AND @TDSAmount > 0)
				BEGIN
					INSERT INTO SingleIB.tTDSPay
						(PaymentOutId,
						VendorId,
						PANNumber,
						TDSAmount,
						IsPaid,
						PayDate,
						IsActive,
						TransactionDate,
						RequestId)
					VALUES
						(@PaymentOutId,
						@VendorId,
						@PANNumber, 
						@TDSAmount, 
						0, 
						NULL, 
						1, 
						SingleIB.ISTNow(), 
						@RequestId)
					SET @OutParam = SCOPE_IDENTITY();
				END
				SET @OutParam = '0';
			END
			ELSE IF(@QueryType ='PAY')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.tTDSPay WHERE TDSPayId = @TDSPayId)
				BEGIN
					UPDATE SingleIB.tTDSPay
					SET PayDate = @PayDate,
						IsPaid = 1,
						RequestId = @RequestId,
						TransactionDate = SingleIB.ISTNow()
					WHERE TDSPayId = @TDSPayId;
					SET @OutParam = @TDSPayId;
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetTDSReceive]...';


GO

CREATE PROCEDURE [SingleIB].[spSetTDSReceive]
(
	@PaymentInId		bigint = 0,
	@CustomerId			bigint = 0,	
	@TDSAmount			decimal(18, 5) = NULL,

	-- When This amout will Received at IncomeTax againest company's TAN
	@TDSReceiveId		bigint = 0,
	@ReceivedDate		datetime = null,
	-- End

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				DECLARE @PANNumber nvarchar(16);
				SELECT @PANNumber = PANNumber 
				FROM SingleIB.mCustomer 
				WHERE CustomerId = @CustomerId;
				IF(@TDSAmount IS NOT NULL AND @TDSAmount > 0)
				BEGIN
					INSERT INTO SingleIB.tTDSReceive
						(PaymentInId,
						CustomerId,
						PANNumber,
						TDSAmount,
						IsReceived,
						ReceivedDate,
						IsActive,
						TransactionDate,
						RequestId)
					VALUES
						(@PaymentInId,
						@CustomerId,
						@PANNumber, 
						@TDSAmount, 
						0, 
						NULL, 
						1, 
						SingleIB.ISTNow(), 
						@RequestId)
					SET @OutParam = SCOPE_IDENTITY();
				END
				SET @OutParam = '0';
			END
			ELSE IF(@QueryType ='RECEIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.tTDSReceive WHERE TDSReceiveId = @TDSReceiveId)
				BEGIN
					UPDATE SingleIB.tTDSReceive
					SET ReceivedDate = @ReceivedDate,
						IsReceived = 1,
						RequestId = @RequestId,
						TransactionDate = SingleIB.ISTNow()
					WHERE TDSReceiveId = @TDSReceiveId;
					SET @OutParam = @TDSReceiveId;
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetUnitOfMeasure]...';


GO
CREATE PROCEDURE [SingleIB].[spSetUnitOfMeasure]
(
	@UnitOfMeasureId	int = 0,

	@UnitOfMeasureName	nvarchar(8) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN				
				INSERT INTO SingleIB.mUnitOfMeasure
					(UnitOfMeasureName,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@UnitOfMeasureName, 
					@Description, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mUnitOfMeasure WHERE UnitOfMeasureId = @UnitOfMeasureId)
				BEGIN
					UPDATE SingleIB.mUnitOfMeasure
					SET UnitOfMeasureName = @UnitOfMeasureName,
						Description = @Description,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE UnitOfMeasureId = @UnitOfMeasureId;

					-- /* Update UnitOfMeasureName of Product Table */
					UPDATE SingleIB.mProduct 
					SET UnitOfMeasureName = @UnitOfMeasureName
					WHERE UnitOfMeasureId = @UnitOfMeasureId;
					 
					SET @OutParam = @UnitOfMeasureId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='DEACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mUnitOfMeasure WHERE UnitOfMeasureId = @UnitOfMeasureId)
				BEGIN
					UPDATE SingleIB.mUnitOfMeasure
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE UnitOfMeasureId = @UnitOfMeasureId;
					SET @OutParam = @UnitOfMeasureId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetVendor]...';


GO
CREATE PROCEDURE [SingleIB].[spSetVendor]
(
	@VendorId			bigint = 0,
	@VendorTypeId		bigint = 0,
	
	@ContactPerson		nvarchar(64) = NULL,
	@Email				nvarchar(64) = NULL,
	@Mobile				nvarchar(16) = NULL,

	@CompanyName		nvarchar(1204) = NULL,
	@CompanyType		nvarchar(64) = NULL,
	@StateCode			nvarchar(2) = NULL,
	@IsGSTRegistered		bit = 0,
	@GSTStateCode		nvarchar(2) = NULL,
	@GSTNumber			nvarchar(16) = NULL,
	@PANNumber			nvarchar(16) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.mVendor
					(VendorTypeId,					
					CompanyName,
					CompanyType,
					StateCode,
					IsGSTRegistered,
					GSTStateCode,
					GSTNumber,
					PANNumber,
					ContactPerson,
					Email,
					Mobile,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@VendorTypeId, 
					@CompanyName, 
					@CompanyType, 
					@StateCode,
					@IsGSTRegistered,
					@GSTStateCode,
					@GSTNumber, 
					@PANNumber, 
					@ContactPerson,
					@Email,
					@Mobile,
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendor WHERE VendorId = @VendorId)
				BEGIN
					UPDATE SingleIB.mVendor
					SET VendorTypeId = @VendorTypeId, 						
						CompanyName = @CompanyName, 
						CompanyType = @CompanyType, 
						StateCode = @StateCode,
						IsGSTRegistered = @IsGSTRegistered, 
						GSTStateCode = @GSTStateCode,
						GSTNumber = @GSTNumber, 
						PANNumber = @PANNumber, 
						ContactPerson = @ContactPerson,
						Email = @Email,
						Mobile = @Mobile,
						IsActive = 1, 
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE VendorId = @VendorId;
					SET @OutParam = @VendorId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendor WHERE VendorId = @VendorId)
				BEGIN
					UPDATE SingleIB.mVendor
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE VendorId = @VendorId;
					SET @OutParam = @VendorId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetVendorBank]...';


GO
CREATE PROCEDURE [SingleIB].[spSetVendorBank]
(
	@VendorBankId		bigint = 0,
	@VendorId			bigint = 0,
	
	@AccountName		nvarchar(1204) = NULL,
	@AccountNumber		nvarchar(64) = NULL,
	@BankName			nvarchar(128) = NULL,
	@IFSC				nvarchar(16) = NULL,
	@MICR				nvarchar(16) = NULL,
	@BranchName			nvarchar(64) = NULL,
	@BranchAddress		nvarchar(128) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.mVendorBank
					(VendorId,					
					AccountName,
					AccountNumber,
					BankName,
					IFSC,
					MICR,
					BranchName,
					BranchAddress,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@VendorId, 					
					@AccountName, 
					@AccountNumber, 
					@BankName, 
					@IFSC, 
					@MICR, 
					@BranchName, 
					@BranchAddress, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendorBank WHERE VendorBankId = @VendorBankId AND VendorId = @VendorId)
				BEGIN
					UPDATE SingleIB.mVendorBank
					SET AccountName = @AccountName, 
						AccountNumber = @AccountNumber, 
						BankName = @BankName, 
						IFSC = @IFSC, 
						MICR = @MICR, 
						BranchName = @BranchName, 
						BranchAddress = @BranchAddress, 
						IsActive = 1, 
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE VendorBankId = @VendorBankId 
						AND VendorId = @VendorId;
					SET @OutParam = @VendorBankId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendorBank WHERE VendorBankId = @VendorBankId AND VendorId = @VendorId)
				BEGIN
					UPDATE SingleIB.mVendorBank
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE VendorBankId = @VendorBankId 
						AND VendorId = @VendorId;
					SET @OutParam = @VendorBankId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetVendorType]...';


GO
CREATE PROCEDURE [SingleIB].[spSetVendorType]
(
	@VendorTypeId		bigint = 0,
	@VendorTypeName		nvarchar(128) = NULL,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'Login';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.mVendorType
					(VendorTypeName,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@VendorTypeName, 
					@Description, 
					1,
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendorType WHERE VendorTypeId = @VendorTypeId)
				BEGIN
					UPDATE SingleIB.mVendorType
					SET VendorTypeName = @VendorTypeName, 
						Description = @Description, 
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE VendorTypeId = @VendorTypeId;
					SET @OutParam = @VendorTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mVendorType WHERE VendorTypeId = @VendorTypeId)
				BEGIN
					UPDATE SingleIB.mVendorType
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE VendorTypeId = @VendorTypeId;
					SET @OutParam = @VendorTypeId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetWareHouse]...';


GO
CREATE PROCEDURE [SingleIB].[spSetWareHouse]
(
	@WareHouseId		bigint = 0,

	@BranchId			bigint = 0,
	@WareHouseName		nvarchar(64) = NULL,
	@IsRetailCounter	bit = 0,
	@Description		nvarchar(256) = NULL,

	@RequestId			nvarchar(64) = NULL,

	@QueryType	varchar(10),

	@OutParam			varchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @T1 VARCHAR(30);
	SELECT @T1 = 'SET';
	
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				INSERT INTO SingleIB.mWareHouse
					(BranchId,
					WareHouseName,
					IsRetailCounter,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@BranchId, 
					@WareHouseName, 
					@IsRetailCounter,
					@Description, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId);
				SET @OutParam = SCOPE_IDENTITY();
			END
			ELSE IF(@QueryType ='UPDATE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mWareHouse WHERE WareHouseId = @WareHouseId)
				BEGIN
					UPDATE SingleIB.mWareHouse
					SET BranchId = @BranchId,
						WareHouseName = @WareHouseName, 
						IsRetailCounter = @IsRetailCounter,
						Description = @Description,
						TransactionDate = SingleIB.ISTNow(), 
						RequestId = @RequestId
					WHERE WareHouseId = @WareHouseId;
					SET @OutParam = @WareHouseId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
			ELSE IF(@QueryType ='INACTIVE')
			BEGIN
				IF EXISTS (SELECT * FROM SingleIB.mWareHouse WHERE WareHouseId = @WareHouseId)
				BEGIN
					UPDATE SingleIB.mWareHouse
					SET IsActive = 0,
						TransactionDate = SingleIB.ISTNow(),
						RequestId = @RequestId
					WHERE WareHouseId = @WareHouseId;
					SET @OutParam = @WareHouseId;
				END
				ELSE
				BEGIN
					SET @OutParam = '0';
				END
			END
		COMMIT TRANSACTION @T1;
		RETURN @OutParam;
	END TRY
	BEGIN CATCH
		PRINT(ERROR_MESSAGE())
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION @T1
		RETURN ERROR_MESSAGE()
	END CATCH
END
GO
PRINT N'Creating Procedure [SingleIB].[spSetGRN]...';


GO



CREATE PROCEDURE [SingleIB].[spSetGRN]
(
	@PurchaseOrderId		bigint = 0,
	@WareHouseId		bigint = 0,
	@GRNNumber			nvarchar(16) = NULL,
	@GRNDate			datetime = NULL,
	@Remarks			nvarchar(512) = NULL,

	@IsDirect			bit = 0,
    @IsFullReceived		bit = 0,

    @VendorDONumber		nvarchar(64) = NULL,
    @VendorInvoiceNumber nvarchar(64) = NULL,
    @VendorInvoiceDate	datetime = NULL,
   
    
	@GRNDetail [SingleIB].[GRNDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@PurchaseOrderNumber nvarchar(16),
		@GRNId bigint,
		@NextGRNNo bigint;

	SELECT @T1 = 'GRNInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** GRN Number Generate **/
				SELECT @NextGRNNo = (COUNT(*) + 1) FROM SingleIB.mGRNHeader;

				SET @GRNNumber = @GRNNumber + RIGHT('000000' + CAST(@NextGRNNo as varchar(16)), 6);

				/** Get PO Number **/
				SELECT @PurchaseOrderNumber = PurchaseOrderNumber 
				FROM [SingleIB].[mPurchaseOrderHeader] 
				WHERE PurchaseOrderId = @PurchaseOrderId;

				/** UPDATE mGRNHeader **/
				--UPDATE SingleIB.mGRNHeader SET IsActive = 0 WHERE PurchaseOrderId = @PurchaseOrderId;

				/** INSERT INTO mGRNHeader **/
				INSERT INTO SingleIB.mGRNHeader
					(GRNDate,
					GRNNumber,
					IsDirect,
					IsFullReceived,
					PurchaseOrderId,
					PurchaseOrderNumber,
					VendorDONumber,
					VendorInvoiceNumber,
					VendorInvoiceDate,
					WareHouseId,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES(@GRNDate, @GRNNumber, @IsDirect, @IsFullReceived, @PurchaseOrderId, @PurchaseOrderNumber,
					@VendorDONumber, @VendorInvoiceNumber, @VendorInvoiceDate, @WareHouseId, @Remarks, 1, SingleIB.ISTNow(), @RequestId);
				SET @GRNId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO mGRNDetail **/
				INSERT INTO SingleIB.mGRNDetail
					(GRNId,
					GRNNumber,
					PurchaseOrderDetailId,
					ProductId,
					ReceivedQuantity,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@GRNId AS GRNId,
					@GRNNumber AS GRNNumber, PurchaseOrderDetailId, ProductId,
					ReceivedQuantity, IsPerishable, ExpiryDate,
					BatchNo, [Description], 1 as IsActive,
					SingleIB.ISTNow() as TransactionDate, @RequestId as RequestId
				FROM @GRNDetail

				/* Check Full Received or not */
				UPDATE SingleIB.mGRNHeader
				SET IsFullReceived = SingleIB.IsFullReceived(@PurchaseOrderId)
				WHERE PurchaseOrderId = @PurchaseOrderId
					AND GRNId = @GRNId;
				
				/** UPDATE STOCK **/
				DECLARE @Counter INT, @MaxId INT, @StockId bigint,
					@ProductId bigint, @ReceivedQuantity decimal(18, 5), @Description nvarchar(256),
					@IsPerishable bit, @ExpiryDate datetime, @BatchNo nvarchar(256);

				SELECT @Counter = min(Id) , @MaxId = max(Id) 
				FROM @GRNDetail;				

				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN
					SELECT @ProductId = ProductId, @ReceivedQuantity = ReceivedQuantity, @Description = Description,
						@IsPerishable = IsPerishable, @ExpiryDate = ExpiryDate, @BatchNo = BatchNo
					FROM @GRNDetail WHERE Id = @Counter;

					-- Stock
					EXEC [SingleIB].[spSetStock]
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Quantity = @ReceivedQuantity,
						@Remarks = @Description,
						@GRNOrShipmentId = @GRNId,
						@GRNOrShipmentDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = 'GRNADD',
						@OutParam = @StockId OUTPUT;
					
					-- Stock Detail
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = 0,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Stock = @ReceivedQuantity,
						@IsPerishable = @IsPerishable,
						@ExpiryDate = @ExpiryDate,
						@BatchNo = @BatchNo,
						@Remarks = @Description,
						@GRNDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = 'ADD',
						@OutParam = @OutParam OUTPUT;

					SET @Counter  = @Counter  + 1;
				END
				SET @OutParam = '{ "GRNId" : ' +
							CAST(@GRNId as VARCHAR) +
							', "GRNNumber" : "' +
							CAST(@GRNNumber AS VARCHAR) +
							'" }';
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetGRNDirect]...';


GO

CREATE PROCEDURE [SingleIB].[spSetGRNDirect]
(
	@PurchaseTypeId		bigint = 0,	
	@VendorId			bigint = 0,
	@PurchaseOrderDate		datetime = NULL,
	@PurchaseOrderAmount		decimal(18,5) = 0.0,
	@PurchaseOrderTaxAmount		decimal(18,5) = 0.0,
	@PurchaseOrderTotalAmount	decimal(18,5) = 0.0,
	@Remarks					nvarchar(512) = NULL,
	@PurchaseOrderDetails		[SingleIB].[PurchaseOrderDetailType] READONLY,

	-- GRN
	@WareHouseId		bigint = 0,
	@GRNNumber			nvarchar(16) = NULL,
	@GRNDate			datetime = NULL,
	@IsDirect			bit = 0,
    @IsFullReceived		bit = 0,
    @VendorDONumber		nvarchar(64) = NULL,
    @VendorInvoiceNumber nvarchar(64) = NULL,
    @VendorInvoiceDate	datetime = NULL,       
	@GRNDetail [SingleIB].[GRNDetailType] READONLY, 

	@IsBillGenerated	bit = 0,
	@IsPaid				bit = 0,

	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@GRNId bigint,
		@NextGRNNo bigint;

	SELECT @T1 = 'GRNInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				DECLARE	@OutParamPO varchar(512),
						@PurchaseOrderId nvarchar(16), 
						@BranchId bigint;
				SELECT @BranchId = BranchId From SingleIB.mWarehouse WHERE WarehouseId = @WarehouseId;

				/* PO Entry */
				EXEC [SingleIB].[spSetPurchaseOrder]
					@PurchaseTypeId = @PurchaseTypeId,
					@BranchId = @BranchId,
					@VendorId = @VendorId,
					@PurchaseOrderDate = @PurchaseOrderDate,
					@PurchaseOrderNumber = NULL,
					@PurchaseOrderAmount = @PurchaseOrderAmount,
					@PurchaseOrderTaxAmount = @PurchaseOrderTaxAmount,
					@PurchaseOrderTotalAmount = @PurchaseOrderTotalAmount,
					@TandC = NULL,
					@Remarks = @Remarks,
					@PurchaseOrderDetails = @PurchaseOrderDetails,
					@RequestId = @RequestId,
					@QueryType = 'ENTRY',
					@OutParam = @OutParamPO OUTPUT,
					@IsGRNDirect = 1,
					@Id = @PurchaseOrderId OUTPUT
				
				/** GRN Number Generate **/
				SELECT @NextGRNNo = (COUNT(*) + 1) FROM SingleIB.mGRNHeader;

				SET @GRNNumber = @GRNNumber + RIGHT('000000' + CAST(@NextGRNNo as varchar(16)), 6);
				
				/** INSERT INTO mGRNHeader **/
				INSERT INTO SingleIB.mGRNHeader
					(GRNDate,
					GRNNumber,
					IsDirect,
					IsFullReceived,
					PurchaseOrderId,
					PurchaseOrderNumber,
					VendorDONumber,
					VendorInvoiceNumber,
					VendorInvoiceDate,
					WareHouseId,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES(
					@GRNDate, @GRNNumber, @IsDirect, @IsFullReceived, @PurchaseOrderId, NULL,
					@VendorDONumber, @VendorInvoiceNumber, @VendorInvoiceDate, @WareHouseId, @Remarks, 1, SingleIB.ISTNow(), @RequestId
				 );
				SET @GRNId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO mGRNDetail **/
				INSERT INTO SingleIB.mGRNDetail
					(GRNId,
					GRNNumber,
					PurchaseOrderDetailId,
					ProductId,
					ReceivedQuantity,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@GRNId AS GRNId,
					@GRNNumber AS GRNNumber,
					Id AS PurchaseOrderDetailId,
					ProductId,
					ReceivedQuantity,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					[Description],
					1,
					SingleIB.ISTNow(),
					@RequestId
				FROM @GRNDetail;
				
				/* Set PurchaseOrderDetailId into mGRNDetail
				 * from mPurchaseOrderDetail */
				WITH tempGRNDetail AS
				(
				   SELECT PurchaseOrderDetailId, GRNDetailId,
				   ROW_NUMBER() OVER(ORDER BY GRNDetailId) as rn 
				   FROM mGRNDetail 
				   WHERE GRNId = @GRNId
				),
				 tempPurchaseOrderDetail as 
				 ( 
					SELECT PurchaseOrderDetailId,
					ROW_NUMBER()  OVER(ORDER BY PurchaseOrderDetailId ) as rn 
					FROM mPurchaseOrderDetail
					WHERE PurchaseOrderId = @PurchaseOrderId
				 )
				SELECT tempPurchaseOrderDetail.PurchaseOrderDetailId, tempGRNDetail.GRNDetailId
				INTO #ttPurchaseGRN 
				FROM tempGRNDetail JOIN tempPurchaseOrderDetail 
				ON tempGRNDetail.rn = tempPurchaseOrderDetail.rn;
				--SELECT * FROM #ttPurchaseGRN

				UPDATE mGRNDetail
				SET PurchaseOrderDetailId = (SELECT tt.PurchaseOrderDetailId FROM #ttPurchaseGRN as tt 
									WHERE tt.GRNDetailId = mGRNDetail.GRNDetailId)
				WHERE GRNId = @GRNId;

				DROP TABLE #ttPurchaseGRN;
				/* End PurchaseOrderDetailId set */

				/** UPDATE STOCK **/
				DECLARE @Counter INT , @MaxId INT;
				DECLARE @StockId bigint;

				SELECT @Counter = min(Id) , @MaxId = max(Id) 
				FROM @GRNDetail
				DECLARE @ProductId bigint, @ReceivedQuantity decimal(18, 5), @Description nvarchar(256),
					@IsPerishable bit, @ExpiryDate datetime, @BatchNo nvarchar(256);

				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN
										
					SELECT @ProductId = ProductId, @ReceivedQuantity = ReceivedQuantity, @Description = Description,
						@IsPerishable = IsPerishable, @ExpiryDate = ExpiryDate, @BatchNo = BatchNo
					FROM @GRNDetail WHERE Id = @Counter;

					-- Stock
					EXEC SingleIB.spSetStock
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Quantity = @ReceivedQuantity,
						@Remarks = @Description,
						@GRNOrShipmentId = @GRNId,
						@GRNOrShipmentDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = 'GRNADD',
						@OutParam = @StockId OUTPUT;
					
					-- Stock Detail
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = 0,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Stock = @ReceivedQuantity,
						@IsPerishable = @IsPerishable,
						@ExpiryDate = @ExpiryDate,
						@BatchNo = @BatchNo,
						@Remarks = @Description,
						@GRNDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = 'ADD',
						@OutParam = @OutParam OUTPUT;

				   SET @Counter  = @Counter  + 1;       
				END
				SET @OutParam = '{ "GRNId" : ' +
							CAST(@GRNId as VARCHAR) +
							', "GRNNumber" : "' +
							CAST(@GRNNumber AS VARCHAR) +
							'" }';
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetOpeningStock]...';


GO



CREATE PROCEDURE [SingleIB].[spSetOpeningStock]
(
	@OpeningStockDetail [SingleIB].[OpeningStockDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,
	@QueryType			varchar(16),
	@OutParam			nvarchar(16) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30);

	SELECT @T1 = 'GRNInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN				
				/** UPDATE STOCK **/
				DECLARE @Counter INT , @MaxId INT;
				DECLARE @StockId bigint;

				SELECT @Counter = min(Id) , @MaxId = max(Id) 
				FROM @OpeningStockDetail
				DECLARE @ProductId bigint, 
					@WareHouseId bigint,
					@Quantity decimal(18, 5), 
					@GRNDate datetime,
					@Description nvarchar(256),
					@IsPerishable bit, 
					@ExpiryDate datetime, 
					@BatchNo nvarchar(256);

				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN
					
					SELECT 
						@WareHouseId = WareHouseId,
						@ProductId = ProductId, 
						@Quantity = Quantity, 
						@GRNDate = GRNDate,
						@IsPerishable = IsPerishable, 
						@BatchNo = BatchNo,
						@ExpiryDate = ExpiryDate,
						@Description = Description						
					FROM @OpeningStockDetail WHERE Id = @Counter;

					-- Stock
					EXEC SingleIB.spSetStock
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Quantity = @Quantity,
						@Remarks = @Description,
						@GRNOrShipmentDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = 'OPENING',
						@OutParam = @StockId OUTPUT;
					
					-- Stock Detail
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = 0,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Stock = @Quantity,
						@IsPerishable = @IsPerishable,
						@ExpiryDate = @ExpiryDate,
						@BatchNo = @BatchNo,
						@Remarks = @Description,
						@GRNDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = 'ADD',
						@OutParam = @OutParam OUTPUT;

				   SET @Counter  = @Counter  + 1;       
				END
				SET @OutParam = @MaxId;
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetPaymentIn]...';


GO



CREATE PROCEDURE [SingleIB].[spSetPaymentIn]
(
	@CustomerId			bigint = 0,
	@CompanyBankId		bigint = 0,

	@IsTDSApplicable	bit = 0,
	@TDSPercentage		decimal(18, 5) = NULL,
	@TDSAmount			decimal(18, 5) = NULL,
	@ReceiveAmount		decimal(18, 5) = 0.0,

	@TotalAmount		decimal(18, 5) = 0.0,
	@PaymentDate		datetime = NULL,
	@PaymentMode		smallint = 0,
	@ReferenceNo		nvarchar(64) = NULL,
	@Remarks			nvarchar(512) = NULL,

	@ChequeNo			nvarchar(8) = NULL,
	@ChequeDate			datetime = NULL,
	@ChequeIFSC			nvarchar(16) = NULL,
    
	@PaymentInDetail [SingleIB].[PaymentInDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@SalesOrderNumber nvarchar(16),
		@PaymentInId bigint;

	SELECT @T1 = 'PaymentOutInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				IF(@IsTDSApplicable = 0)
				BEGIN
					SET @TDSPercentage = NULL;
					SET @TDSAmount = NULL;
					SET @ReceiveAmount = @TotalAmount;
				END
				INSERT INTO SingleIB.mPaymentInHeader
				   (CustomerId,
				   CompanyBankId,
				   IsTDSApplicable,
				   TDSPercentage,	
				   TDSAmount,		
				   ReceiveAmount,
				   TotalAmount,
				   PaymentDate,
				   PaymentMode,
				   ReferenceNo,
				   ChequeNo,
				   ChequeDate,
				   ChequeIFSC,
				   Remarks,
				   IsActive,
				   TransactionDate,
				   RequestId)
				VALUES
					(@CustomerId, 
					@CompanyBankId,
					@IsTDSApplicable,
				    @TDSPercentage,	
				    @TDSAmount,		
				    @ReceiveAmount,
					@TotalAmount, 
					@PaymentDate, 
					@PaymentMode,
					@ReferenceNo, 
					@ChequeNo, 
					@ChequeDate, 
					@ChequeIFSC, 
					@Remarks, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @PaymentInId = CAST(SCOPE_IDENTITY() AS bigint);

				INSERT INTO SingleIB.mPaymentInDetail
					(PaymentInId,
					InvoiceId,
					InvoiceNumber,
					Amount,
					IsFullReceived,
					PaymentDate,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@PaymentInId AS PaymentInId,
					InvoiceId,
					InvoiceNumber,
					Amount,
					IsFullReceived,
					PaymentDate,
					Description,
					1 AS IsActive,
					SingleIB.ISTNow() AS TransactionDate,
					@RequestId as RequestId
				FROM @PaymentInDetail;
				IF(@IsTDSApplicable = 1)
				BEGIN
					EXEC [SingleIB].[spSetTDSReceive]
						@PaymentInId = @PaymentInId,
						@CustomerId = @CustomerId,
						@TDSAmount = @TDSAmount,
						@RequestId = @RequestId,
						@QueryType = 'INSERT',
						@OutParam = @OutParam;
				END

				SET @OutParam = @PaymentInId;				
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetPaymentOut]...';


GO



CREATE PROCEDURE [SingleIB].[spSetPaymentOut]
(
	@VendorId			bigint = 0,
	@VendorBankId		bigint = 0,

	@IsTDSApplicable	bit = 0,
	@TDSPercentage		decimal(18, 5) = NULL,
	@TDSAmount			decimal(18, 5) = NULL,
	@PayAmount			decimal(18, 5) = 0.0,

	@TotalAmount		decimal(18, 5) = 0.0,
	@PaymentDate		datetime = NULL,
	@PaymentMode		smallint = 0,
	@ReferenceNo		nvarchar(64) = NULL,
	@Remarks			nvarchar(512) = NULL,

	@ChequeNo			nvarchar(8) = NULL,
	@ChequeDate			datetime = NULL,
	@ChequeIFSC			nvarchar(16) = NULL,
    
	@PaymentOutDetail [SingleIB].[PaymentOutDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@PurchaseOrderNumber nvarchar(16),
		@PaymentOutId bigint;

	SELECT @T1 = 'PaymentOutInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				IF(@IsTDSApplicable = 0)
				BEGIN
					SET @TDSPercentage = NULL;
					SET @TDSAmount = NULL;
					SET @PayAmount = @TotalAmount;
				END
				INSERT INTO SingleIB.mPaymentOutHeader
				   (VendorId,
				   VendorBankId,
				   IsTDSApplicable,
				   TDSPercentage,	
				   TDSAmount, 
				   PayAmount,
				   TotalAmount,
				   PaymentDate,
				   PaymentMode,
				   ReferenceNo,
				   ChequeNo,
				   ChequeDate,
				   ChequeIFSC,
				   Remarks,
				   IsActive,
				   TransactionDate,
				   RequestId)
				VALUES
					(@VendorId, 
					@VendorBankId,
					@IsTDSApplicable,
				    @TDSPercentage,	
				    @TDSAmount,		
				    @PayAmount,
					@TotalAmount, 
					@PaymentDate, 
					@PaymentMode,
					@ReferenceNo, 
					@ChequeNo, 
					@ChequeDate, 
					@ChequeIFSC, 
					@Remarks, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @PaymentOutId = CAST(SCOPE_IDENTITY() AS bigint);

				INSERT INTO SingleIB.mPaymentOutDetail
					(PaymentOutId,
					BillId,
					BillNumber,
					Amount,
					IsFullPaid,
					PaymentDate,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@PaymentOutId AS PaymentOutId,
					BillId,
					BillNumber,
					Amount,
					IsFullPaid,
					PaymentDate,
					Description,
					1 AS IsActive,
					SingleIB.ISTNow() AS TransactionDate,
					@RequestId as RequestId
				FROM @PaymentOutDetail;

				IF(@IsTDSApplicable = 1)
				BEGIN
					EXEC [SingleIB].[spSetTDSPay]
						@PaymentOutId = @PaymentOutId,
						@VendorId = @VendorId,
						@TDSAmount = @TDSAmount,
						@RequestId = @RequestId,
						@QueryType = 'INSERT',
						@OutParam = @OutParam;
				END

				SET @OutParam = @PaymentOutId;
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetShipment]...';


GO

CREATE PROCEDURE [SingleIB].[spSetShipment]
(	
	@ShipmentTypeId		bigint = 0,
	@ShipmentDate		datetime = NULL,
	@ShipmentNumber		nvarchar(16) = NULL,

	@IsDirect			bit = 0,
    @IsFullShipped		bit = 0,

	@SalesOrderId		bigint = 0,
	@WareHouseId		bigint = 0,		
	@Remarks			nvarchar(512) = NULL,
	
    @VehicleNumber		nvarchar(16) = NULL,
    @HandOverPerson		nvarchar(64) = NULL,
    @HandOverPersonMobile	nvarchar(16) = NULL,   
    
	@ShipmentDetail [SingleIB].[ShipmentDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@SalesOrderNumber nvarchar(16),
		@ShipmentId bigint,
		@NextShipmentNo bigint;

	SELECT @T1 = 'ShipmentInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** Shipment Number Generate **/
				SELECT @NextShipmentNo = (COUNT(*) + 1) FROM SingleIB.mShipmentHeader;

				SET @ShipmentNumber = @ShipmentNumber + RIGHT('000000' + CAST(@NextShipmentNo as varchar(16)), 6);

				/** Get PO Number **/
				SELECT @SalesOrderNumber = SalesOrderNumber 
				FROM [SingleIB].[mSalesOrderHeader] 
				WHERE SalesOrderId = @SalesOrderId;

				/** UPDATE mShipmentHeader **/
				--UPDATE SingleIB.mShipmentHeader SET IsActive = 0 WHERE SalesOrderId = @SalesOrderId;

				/** INSERT INTO mShipmentHeader **/
				INSERT INTO SingleIB.mShipmentHeader
					(ShipmentTypeId,
					ShipmentDate,
					ShipmentNumber,
					IsDirect,
					IsFullShipped,
					SalesOrderId,
					SalesOrderNumber,
					VehicleNumber,
					HandOverPerson,
					HandOverPersonMobile,
					WareHouseId,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES(@ShipmentTypeId, @ShipmentDate, @ShipmentNumber, @IsDirect, @IsFullShipped, @SalesOrderId, @SalesOrderNumber,
					@VehicleNumber, @HandOverPerson, @HandOverPersonMobile, @WareHouseId, @Remarks, 1, SingleIB.ISTNow(), @RequestId);

				SET @ShipmentId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO mShipmentDetail **/
				INSERT INTO SingleIB.mShipmentDetail
					(ShipmentId,
					ShipmentNumber,
					SalesOrderDetailId,
					ProductId,
					ShippedQuantity,
					StockDetailId,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@ShipmentId AS ShipmentId,
					@ShipmentNumber AS ShipmentNumber,
					SalesOrderDetailId,
					ProductId,
					ShippedQuantity,
					StockDetailId,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					[Description],
					1 AS IsActive,
					SingleIB.ISTNow() AS TransactionDate,
					@RequestId AS RequestId 
				FROM @ShipmentDetail;

				/* Check Full Shipped or not */
				UPDATE SingleIB.mShipmentHeader
				SET IsFullShipped = SingleIB.IsFullShipped(@SalesOrderId)
				WHERE SalesOrderId = @SalesOrderId
					AND ShipmentId = @ShipmentId;
				
				/** UPDATE STOCK **/
				DECLARE @Counter INT , @MaxId INT;
				DECLARE @StockId bigint;

				SELECT @Counter = min(Id) , @MaxId = max(Id) 
				FROM @ShipmentDetail;
				DECLARE @ProductId bigint, @StockDetailId bigint, 
					@ShippedQuantity decimal(18, 5), @Description nvarchar(256),
					@IsPerishable bit, @ExpiryDate datetime, @BatchNo nvarchar(256);

				-- Loop Start
				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN
					SELECT @ProductId = ProductId, @StockDetailId = StockDetailId,
					@ShippedQuantity = ShippedQuantity, @Description = Description,
						@IsPerishable = IsPerishable, @ExpiryDate = ExpiryDate, @BatchNo = BatchNo
					FROM @ShipmentDetail WHERE Id = @Counter;

					-- Stock
					EXEC SingleIB.spSetStock
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Quantity = @ShippedQuantity,
						@Remarks = @Description,
						@GRNOrShipmentId = @ShipmentId,
						@GRNOrShipmentDate = @ShipmentDate,
						@RequestId = @RequestId,
						@QueryType = 'SHIPSUB',
						@OutParam = @StockId OUTPUT;
					
					-- Stock Detail
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = @StockDetailId,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Stock = @ShippedQuantity,
						@IsPerishable = @IsPerishable,
						@ExpiryDate = @ExpiryDate,
						@BatchNo = @BatchNo,
						@Remarks = @Description,
						@GRNDate = @ShipmentDate,
						@RequestId = @RequestId,
						@QueryType = 'SUB',
						@OutParam = @OutParam OUTPUT;

				   SET @Counter  = @Counter  + 1;       
				END

				SET @OutParam = '{ "ShipmentId" : ' +
							CAST(@ShipmentId as VARCHAR) +
							', "ShipmentNumber" : "' +
							CAST(@ShipmentNumber AS VARCHAR) +
							'" }';
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetShipmentDirect]...';


GO

CREATE PROCEDURE [SingleIB].[spSetShipmentDirect]
(
	@ComapnyIsGSTRegistered		bit = 0,

	-- SO
	@SalesTypeId		bigint = 0,	
	@CustomerId			bigint = 0,
	@SalesOrderDate		datetime = NULL,
	@SalesOrderAmount			decimal(18,5) = 0.0,
	@SalesOrderTaxAmount		decimal(18,5) = 0.0,
	@SalesOrderTotalAmount		decimal(18,5) = 0.0,
	@DeliveryDate				datetime = NULL,		-- extra
	@Remarks					nvarchar(512) = NULL,
	@SalesOrderDetails			[SingleIB].[SalesOrderDetailType] READONLY,

	-- Ship
	@WareHouseId		bigint = 0,
	@ShipmentTypeId		bigint = 0,		-- extra
	@ShipmentNumber		nvarchar(16) = NULL,
	@ShipmentDate		datetime = NULL,
	@IsDirect			bit = 0,
    @IsFullShipped		bit = 0,

    @VehicleNumber		nvarchar(16) = NULL,
    @HandOverPerson		nvarchar(64) = NULL,
    @HandOverPersonMobile	nvarchar(16) = NULL,   
    
	@ShipmentDetails [SingleIB].[ShipmentDetailType] READONLY, 

	@IsInvoiceGenerated		bit = 0,
	@IsReceived				bit = 0,

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT,

	/* Reqired For Retail Sales - value of inserted SalesOrderId */
	@Id						nvarchar(18) = NULL OUTPUT	
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@ShipmentId		bigint,
		@NextShipmentNo bigint;

	SELECT @T1 = 'ShipmentInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				DECLARE	@OutParamSO varchar(512),
						@SalesOrderId nvarchar(16), 
						@BranchId bigint;
				SELECT @BranchId = BranchId From SingleIB.mWarehouse WHERE WarehouseId = @WarehouseId;

				/*SO Entry */
				EXEC [SingleIB].[spSetSalesOrder]
					@ComapnyIsGSTRegistered = @ComapnyIsGSTRegistered,
					@SalesTypeId = @SalesTypeId,
					@BranchId = @BranchId,
					@CustomerId = @CustomerId,
					@SalesOrderDate = @SalesOrderDate,
					@SalesOrderNumber = NULL,
					@DeliveryDate = @DeliveryDate,
					@SalesOrderAmount = @SalesOrderAmount,
					@SalesOrderTaxAmount = @SalesOrderTaxAmount,
					@SalesOrderTotalAmount = @SalesOrderTotalAmount,
					@TandC = NULL,
					@Remarks = @Remarks,
					@SalesOrderDetails = @SalesOrderDetails,
					@RequestId = @RequestId,
					@QueryType = 'ENTRY',
					@OutParam = @OutParamSO OUTPUT,
					@IsShipmentDirect = 1,
					@Id = @SalesOrderId OUTPUT

				/* For Retail Sales */
				SET @Id = CAST(@SalesOrderId AS nvarchar(18));
				
				-- Service is not allowed to receive as Stock
				-- Only Goods Allowed for Stock
				-- Added on 01-09-2021
				IF((SELECT COUNT(*) FROM @ShipmentDetails) > 0)
				BEGIN
					/** Shipment Number Generate **/
					SELECT @NextShipmentNo = (COUNT(*) + 1) FROM SingleIB.mShipmentHeader;

					SET @ShipmentNumber = @ShipmentNumber + RIGHT('000000' + CAST(@NextShipmentNo as varchar(16)), 6);
				
					/** INSERT INTO mShipmentHeader **/
					INSERT INTO SingleIB.mShipmentHeader
						(ShipmentTypeId,
						ShipmentDate,
						ShipmentNumber,
						IsDirect,
						IsFullShipped,
						SalesOrderId,
						SalesOrderNumber,
						VehicleNumber,
						HandOverPerson,
						HandOverPersonMobile,
						WareHouseId,
						Remarks,
						IsActive,
						TransactionDate,
						RequestId)
					VALUES(@ShipmentTypeId, @ShipmentDate, @ShipmentNumber, @IsDirect, @IsFullShipped, @SalesOrderId, NULL,
						@VehicleNumber, @HandOverPerson, @HandOverPersonMobile, @WareHouseId, @Remarks, 1, SingleIB.ISTNow(), @RequestId);

					SET @ShipmentId = CAST(SCOPE_IDENTITY() AS bigint);

					/** INSERT INTO mShipmentDetail **/
					INSERT INTO SingleIB.mShipmentDetail
						(ShipmentId,
						ShipmentNumber,
						SalesOrderDetailId,
						ProductId,
						ShippedQuantity,
						StockDetailId,
						IsPerishable,
						ExpiryDate,
						BatchNo,
						Description,
						IsActive,
						TransactionDate,
						RequestId)
					SELECT
						@ShipmentId AS ShipmentId,
						@ShipmentNumber AS ShipmentNumber,
						Id AS SalesOrderDetailId,
						ProductId,
						ShippedQuantity,
						StockDetailId,
						IsPerishable,
						ExpiryDate,
						BatchNo,
						[Description],
						1 AS IsActive,
						SingleIB.ISTNow() AS TransactionDate,
						@RequestId AS RequestId 
					FROM @ShipmentDetails;

					/* Set SalesOrderDetailId into mShipmentDetail
					 * from mSalesOrderDetail */
					WITH tempShipmentDetail AS
					(
					   SELECT SalesOrderDetailId, ShipmentDetailId,
					   ROW_NUMBER() OVER(ORDER BY ShipmentDetailId) as rn 
					   FROM mShipmentDetail 
					   WHERE ShipmentId = @ShipmentId
					),
					 tempSalesOrderDetail as 
					 ( 
						SELECT SalesOrderDetailId,
						ROW_NUMBER()  OVER(ORDER BY SalesOrderDetailId ) as rn 
						FROM mSalesOrderDetail
						WHERE SalesOrderId = @SalesOrderId
					 )
					SELECT tempSalesOrderDetail.SalesOrderDetailId, tempShipmentDetail.ShipmentDetailId
					INTO #ttSalesShipment 
					FROM tempShipmentDetail JOIN tempSalesOrderDetail 
					ON tempShipmentDetail.rn = tempSalesOrderDetail.rn;
					--SELECT * FROM #ttSalesShipment

					UPDATE mShipmentDetail
					SET SalesOrderDetailId = (SELECT tt.SalesOrderDetailId FROM #ttSalesShipment as tt 
										WHERE tt.ShipmentDetailId = mShipmentDetail.ShipmentDetailId)
					WHERE ShipmentId = @ShipmentId;

					DROP TABLE #ttSalesShipment;
					/* End SalesOrderDetailId set */
				
					/** UPDATE STOCK **/
					DECLARE @Counter INT , @MaxId INT;
					DECLARE @StockId bigint;

					SELECT @Counter = min(Id) , @MaxId = max(Id) 
					FROM @ShipmentDetails;
					DECLARE @ProductId bigint, @StockDetailId bigint, 
						@ShippedQuantity decimal(18, 5), @Description nvarchar(256),
						@IsPerishable bit, @ExpiryDate datetime, @BatchNo nvarchar(256);

					-- Loop Start
					WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
					BEGIN
						SELECT @ProductId = ProductId, @StockDetailId = StockDetailId,
							@ShippedQuantity = ShippedQuantity, @Description = Description,
							@IsPerishable = IsPerishable, @ExpiryDate = ExpiryDate, @BatchNo = BatchNo
						FROM @ShipmentDetails WHERE Id = @Counter;

						-- Stock
						EXEC SingleIB.spSetStock
							@ProductId = @ProductId,
							@WareHouseId = @WareHouseId,
							@Quantity = @ShippedQuantity,
							@Remarks = @Description,
							@GRNOrShipmentId = @ShipmentId,
							@GRNOrShipmentDate = @ShipmentDate,
							@RequestId = @RequestId,
							@QueryType = 'SHIPSUB',
							@OutParam = @StockId OUTPUT;
					
						-- Stock Detail
						EXEC [SingleIB].[spSetStockDetail]
							@StockDetailId = @StockDetailId,
							@StockId = @StockId,
							@ProductId = @ProductId,
							@WareHouseId = @WareHouseId,
							@Stock = @ShippedQuantity,
							@IsPerishable = @IsPerishable,
							@ExpiryDate = @ExpiryDate,
							@BatchNo = @BatchNo,
							@Remarks = @Description,
							@GRNDate = @ShipmentDate,
							@RequestId = @RequestId,
							@QueryType = 'SUB',
							@OutParam = @OutParam OUTPUT;

					   SET @Counter  = @Counter  + 1;       
					END
				END

				SET @OutParam = '{ "ShipmentId" : ' +
							CAST(@ShipmentId as VARCHAR) +
							', "ShipmentNumber" : "' +
							CAST(@ShipmentNumber AS VARCHAR) +
							'" }';
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetStockAdjust]...';


GO

CREATE PROCEDURE [SingleIB].[spSetStockAdjust]
(
	@StockAdjustNumber		nvarchar(16) = NULL,
	@StockAdjustDate		datetime = NULL,
	@Remarks				nvarchar(512) = NULL,
	@WareHouseId			bigint = 0,
	@IsStockIncrease		bit = null, -- Can't set 0, here 0 means Stock Decrease
	@AdjustReasonId			bigint =  NULL,
	@AdjustReason			nvarchar(64) = null,
	        
	@STDetail [SingleIB].[StockAdjustDetailType] READONLY, 

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@StockAdjustId		bigint,
		@NextSTNo				bigint;

	SELECT @T1 = 'StockAdjust';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** SKA Number Generate **/
				SELECT @NextSTNo = (COUNT(*) + 1) FROM SingleIB.tStockAdjustHeader;

				SET @StockAdjustNumber = @StockAdjustNumber + RIGHT('000000' + CAST(@NextSTNo as varchar(16)), 6);

				-- AdjustReason Logic
				IF (@AdjustReasonId IS NULL OR @AdjustReasonId = 0)
				BEGIN
					-- Validation in SP, if AdjustReason is null set a value
					IF (@AdjustReason IS NULL OR @AdjustReason = '')
					BEGIN
						SET @AdjustReason = 'Other reason by user';
					END	

					-- INSERT ROW to sStockAdjustReason
					EXEC [SingleIB].[spSetStockAdjustReason]
						@AdjustReason = @AdjustReason,
						@RequestId = @RequestId,
						@QueryType = 'INSERT',
						@OutParam = @AdjustReasonId OUTPUT
				END
				ELSE
				BEGIN
					SELECT @AdjustReason = ssar.AdjustReason FROM SingleIB.sStockAdjustReason AS ssar
					WHERE ssar.AdjustReasonId = @AdjustReasonId;
				END

				/** INSERT INTO tStockAdjustHeader **/
				INSERT INTO SingleIB.tStockAdjustHeader
					(WareHouseId,
					StockAdjustNumber,
					StockAdjustDate,					
					IsStockIncrease,
					AdjustReasonId,
					AdjustReason,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@WareHouseId, 
					@StockAdjustNumber, 
					@StockAdjustDate, 
					@IsStockIncrease,
					@AdjustReasonId,
					@AdjustReason,
					@Remarks, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId)
				SET @StockAdjustId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO tStockAdjustDetail **/
				INSERT INTO SingleIB.tStockAdjustDetail
					(StockAdjustId,
					StockAdjustNumber,
					ProductId,
					IsStockIncrease,
					AvailableQuantity,
					AdjustQuantity,
					StockDetailId,
					GRNDate,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@StockAdjustId,
					@StockAdjustNumber,
					ProductId,
					@IsStockIncrease,
					AvailableQuantity,
					AdjustQuantity,
					StockDetailId,
					GRNDate,
					IsPerishable,
					ExpiryDate,
					BatchNo,
					Description,
					1,
					SingleIB.ISTNow(),
					@RequestId
				FROM @STDetail;

				/** UPDATE STOCK **/
				DECLARE @Counter INT, 
					@MaxId INT,
					@StockId bigint, 				
					@ProductId bigint, 
					@AvailableQuantity decimal(18, 5),
					@AdjustQuantity decimal(18, 5), 
					@Description nvarchar(512),
					@StockDetailId bigint,
					@GRNDate datetime,
					@IsPerishable bit,
					@ExpiryDate datetime,
					@BatchNo nvarchar(256),
					@SetStockParam nvarchar(16);

				SELECT @Counter = min(Id), @MaxId = max(Id) 
				FROM @STDetail;

				WHILE(@Counter IS NOT NULL AND @Counter <= @MaxId)
				BEGIN
					SELECT @ProductId = ProductId, 
						@AvailableQuantity = AvailableQuantity, 
						@AdjustQuantity = AdjustQuantity,
						@Description = Description,
						@StockDetailId = StockDetailId,
						@GRNDate = GRNDate,
						@IsPerishable = IsPerishable,
						@ExpiryDate = ExpiryDate,
						@BatchNo = BatchNo						
					FROM @STDetail WHERE Id = @Counter;

					-- @OutParam Set depends on Stock Increase or Decrease
					SELECT @SetStockParam = IIF(@IsStockIncrease = 1, 'ADJUSTADD', 'ADJUSTSUB');

					-- Stock
					EXEC SingleIB.spSetStock						
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Quantity = @AdjustQuantity,
						@Remarks = @Description,
						@GRNOrShipmentId = @StockAdjustId,
						@GRNOrShipmentDate = @StockAdjustDate,
						@RequestId = @RequestId,
						@QueryType = @SetStockParam,
						@OutParam = @StockId OUTPUT;

					-- @OutParam Set depends on Stock Increase or Decrease
					SELECT @SetStockParam = IIF(@IsStockIncrease = 1, 'ADD', 'SUB');
						
					-- Stock Detail
					EXEC [SingleIB].[spSetStockDetail]
						@StockDetailId = @StockDetailId,
						@StockId = @StockId,
						@ProductId = @ProductId,
						@WareHouseId = @WareHouseId,
						@Stock = @AdjustQuantity,
						@IsPerishable = @IsPerishable,
						@ExpiryDate = @ExpiryDate,
						@BatchNo = @BatchNo,
						@Remarks = @Description,
						@GRNDate = @GRNDate,
						@RequestId = @RequestId,
						@QueryType = @SetStockParam,
						@OutParam = @OutParam OUTPUT;

				   SET @Counter  = @Counter  + 1        
				END
				SET @OutParam = '{ "StockAdjustId" : ' +
							CAST(@StockAdjustId as VARCHAR) +
							', "StockAdjustNumber" : "' +
							CAST(@StockAdjustNumber AS VARCHAR) +
							'" }';
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
GO
PRINT N'Creating Procedure [SingleIB].[spSetRetailSales]...';


GO

CREATE PROCEDURE [SingleIB].[spSetRetailSales]
(
	-- Customer
	@ContactPerson		nvarchar(64) = NULL,
	@Email				nvarchar(64) = NULL,
	@Mobile				nvarchar(16) = NULL,
	@CompanyName		nvarchar(1204) = NULL,
	@CompanyType		nvarchar(64) = NULL,
	@StateCode			nvarchar(2) = null,
	@IsGSTRegistered	bit = 0,
	@GSTStateCode		nvarchar(2) = NULL,
	@GSTNumber			nvarchar(16) = NULL,
	@PANNumber			nvarchar(16) = NULL,

	-- SO
	@CustomerId				bigint = 0,
	@SalesOrderDate			datetime = NULL,
	@SalesOrderAmount			decimal(18,5) = 0.0,
	@SalesOrderTaxAmount		decimal(18,5) = 0.0,
	@SalesOrderTotalAmount		decimal(18,5) = 0.0,
	@DeliveryDate				datetime = NULL,		-- extra
	@Remarks					nvarchar(512) = NULL,
	@SalesOrderDetails			[SingleIB].[SalesOrderDetailType] READONLY,

	-- Ship
	@WareHouseId			bigint = 0,
	@ShipmentNumber			nvarchar(16) = NULL,
	@ShipmentDate			datetime = NULL,
	@IsDirect				bit = 0,
    @IsFullShipped			bit = 0,

    @VehicleNumber			nvarchar(16) = NULL,
    @HandOverPerson			nvarchar(64) = NULL,
    @HandOverPersonMobile	nvarchar(16) = NULL,   
    
	@ShipmentDetails		[SingleIB].[ShipmentDetailType] READONLY, 

	@IsInvoiceGenerated		bit = 0,
	@IsReceived				bit = 0,

	@RequestId			nvarchar(64) = NULL,

	@QueryType			varchar(16),
	@OutParam			nvarchar(max) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@ShipmentId		bigint,
		@NextShipmentNo bigint;

	SELECT @T1 = 'ShipmentInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				DECLARE	@SalesOrderId nvarchar(16);

				/* Customer Entry */
				EXEC [SingleIB].[spSetCustomer]
					@CustomerTypeId = NULL,
					@ContactPerson = @ContactPerson,
					@Email = @Email,
					@Mobile = @Mobile,
					@IsRetailCustomer = 1,
					@CompanyName = @CompanyName,
					@CompanyType = @CompanyType,
					@StateCode = @StateCode,
					@IsGSTRegistered = @IsGSTRegistered,
					@GSTStateCode = @GSTStateCode,
					@GSTNumber = @GSTNumber,
					@PANNumber = @PANNumber,					
					@RequestId = @RequestId,
					@QueryType = 'RETAILINSERT',
					@OutParam = @CustomerId OUTPUT

				/* Shipment Direct Entry */
				EXEC [SingleIB].[spSetShipmentDirect]
					@SalesTypeId = NULL,
					@CustomerId = @CustomerId,
					@SalesOrderDate = @SalesOrderDate,
					@SalesOrderAmount = @SalesOrderAmount,
					@SalesOrderTaxAmount = @SalesOrderTaxAmount,
					@SalesOrderTotalAmount= @SalesOrderTotalAmount,
					@DeliveryDate = @DeliveryDate,
					@Remarks = @Remarks,
					@SalesOrderDetails = @SalesOrderDetails,
					@WareHouseId = @WareHouseId,
					@ShipmentTypeId = NULL,
					@ShipmentNumber = @ShipmentNumber,
					@ShipmentDate = @ShipmentDate,
					@IsDirect = @IsDirect,
					@IsFullShipped = @IsFullShipped,					
					@VehicleNumber = @VehicleNumber,
					@HandOverPerson	= @HandOverPerson,
					@HandOverPersonMobile = @HandOverPersonMobile,
					@ShipmentDetails = @ShipmentDetails,					
					@IsInvoiceGenerated	= @IsInvoiceGenerated,
					@IsReceived	= @IsReceived,					
					@RequestId = @RequestId,					
					@QueryType = 'INSERT',	
					@OutParam = @OutParam OUTPUT,
					@Id = @SalesOrderId OUTPUT

				SET @OutParam = @SalesOrderId;
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
GO

GO
PRINT N'Creating Procedure [SingleIB].[spSetBill]...';

GO

CREATE PROCEDURE [SingleIB].[spSetBill]
(
	@BillTypeId			bigint = 0,
	@BillDate			datetime = NULL,
	@BillDueDate		datetime = NULL,
	@BillNumber			nvarchar(16) = NULL,
	@VendorId			bigint = 0,
	@PurchaseOrderId	bigint = 0,
    @VendorDONumber		nvarchar(64) = NULL,
    @VendorInvoiceNumber nvarchar(64) = NULL,
	@VendorInvoiceDate	datetime = null,
	@Remarks			nvarchar(512) = NULL,    

	@IsGstApplicable		bit = 0,
	@GSTNumber			nvarchar(16) = NULL,
	@GSTStateCode		nvarchar(2) = NULL,
	@GSTType			smallint = NULL,	
    @BilledAmount		decimal(18,5) = 0,
	@TaxAmount			decimal(18,5) = 0,
	@TotalAmount		decimal(18,5) = 0,
	
	@IsFullBilled		bit = 0,

	@BillDetail [SingleIB].[BillDetailType] READONLY, 
	@RequestId			nvarchar(64),
	@QueryType			varchar(16),
	@OutParam			nvarchar(512) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 
		@T1 VARCHAR(30),
		@BillId bigint,		
		@NextBillNo bigint;

	SELECT @T1 = 'BillInsert';
		
    BEGIN TRY
		BEGIN TRANSACTION @T1
			IF (@QueryType ='INSERT')
			BEGIN
				/** Bill Number Generate **/
				SELECT @NextBillNo = (COUNT(*) + 1) FROM SingleIB.mBillHeader;

				SET @BillNumber = @BillNumber + RIGHT('000000' + CAST(@NextBillNo as varchar(16)), 6);

				/** INSERT INTO mBillHeader **/
				INSERT INTO SingleIB.mBillHeader
					(BillTypeId,
					BillDate,
					BillDueDate,
					BillNumber,
					IsFullBilled,
					PurchaseOrderId,
					VendorId,
					IsGstApplicable,
					GSTNumber,
					GSTStateCode,
					GSTType,
					BilledAmount,
					TaxAmount,
					TotalAmount,
					VendorDONumber,
					VendorInvoiceNumber,
					VendorInvoiceDate,
					Remarks,
					IsActive,
					TransactionDate,
					RequestId)
				VALUES
					(@BillTypeId, 
					@BillDate, 
					@BillDueDate, 
					@BillNumber, 
					@IsFullBilled, 
					@PurchaseOrderId,
					@VendorId, 
					@IsGstApplicable, 
					@GSTNumber, 
					@GSTStateCode, 
					@GSTType, 
					@BilledAmount, 
					@TaxAmount, 
					@TotalAmount,
					@VendorDONumber, 
					@VendorInvoiceNumber, 
					@VendorInvoiceDate, 
					@Remarks, 
					1, 
					SingleIB.ISTNow(), 
					@RequestId)

				SET @BillId = CAST(SCOPE_IDENTITY() AS bigint);

				/** INSERT INTO mBillDetail **/
				INSERT INTO SingleIB.mBillDetail
					(BillId,
					BillNumber,
					PurchaseOrderDetailId,
					ProductId,
					HSNSAC,
					Price,
					BilledQuantity,
					SubTotal,
					TaxPercentage,
					CGSTAmount,
					SGSTAmount,
					IGSTAmount,
					Total,
					Description,
					IsActive,
					TransactionDate,
					RequestId)
				SELECT
					@BillId AS BillId,
					@BillNumber AS BillNumber,
					PurchaseOrderDetailId,
					ProductId,
					HSNSAC,
					Price,
					BilledQuantity,
					SubTotal,
					TaxPercentage,
					CGSTAmount,
					SGSTAmount,
					IGSTAmount,
					Total,
					Description,
					1,
					SingleIB.ISTNow(),
					@RequestId AS RequestId
				FROM @BillDetail;

				/* Check Full Billed or not */
				UPDATE SingleIB.mBillHeader
				SET IsFullBilled = SingleIB.IsFullBilled(@PurchaseOrderId)
				WHERE PurchaseOrderId = @PurchaseOrderId
					AND BillId = @BillId;
				
				SET @OutParam = '{ "BillId" : ' +
							CAST(@BillId as VARCHAR(64)) +
							', "BillNumber" : "' +
							CAST(@BillNumber AS VARCHAR(16)) +
							'" }';
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

PRINT N'Update complete.';


GO
