CREATE TABLE [dbo].[mPurchaseOrderHeader] (
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Based on Vendor''s IsGSTRegistered on the day of PO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mPurchaseOrderHeader', @level2type = N'COLUMN', @level2name = N'IsGstAllowed';

