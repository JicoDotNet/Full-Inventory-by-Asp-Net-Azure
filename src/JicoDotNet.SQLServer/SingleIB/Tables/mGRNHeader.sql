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

