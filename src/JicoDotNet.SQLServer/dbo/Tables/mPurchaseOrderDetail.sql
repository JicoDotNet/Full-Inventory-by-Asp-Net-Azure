CREATE TABLE [dbo].[mPurchaseOrderDetail] (
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

