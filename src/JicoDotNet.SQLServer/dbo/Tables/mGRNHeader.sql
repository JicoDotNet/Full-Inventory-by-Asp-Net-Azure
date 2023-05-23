CREATE TABLE [dbo].[mGRNHeader] (
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mGRNHeader', @level2type = N'COLUMN', @level2name = N'IsFullReceived';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'if user don''t want to set PO - then true, else false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mGRNHeader', @level2type = N'COLUMN', @level2name = N'IsDirect';

