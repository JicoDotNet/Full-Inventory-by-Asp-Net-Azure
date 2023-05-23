CREATE TABLE [dbo].[mInvoiceHeader] (
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'None = 0, CGSTSGST = 1, IGST = 2, UGST = 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mInvoiceHeader', @level2type = N'COLUMN', @level2name = N'GSTType';

