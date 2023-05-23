CREATE TABLE [dbo].[mInvoiceDetail] (
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

