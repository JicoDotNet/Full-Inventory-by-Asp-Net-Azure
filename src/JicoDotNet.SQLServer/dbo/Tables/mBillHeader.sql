CREATE TABLE [dbo].[mBillHeader] (
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'None = 0, CGSTSGST = 1, IGST = 2, UGST = 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mBillHeader', @level2type = N'COLUMN', @level2name = N'GSTType';

