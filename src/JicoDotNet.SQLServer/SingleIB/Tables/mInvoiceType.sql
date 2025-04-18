﻿CREATE TABLE [SingleIB].[mInvoiceType] (
    [InvoiceTypeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [InvoiceTypeName] NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mInvoiceType] PRIMARY KEY CLUSTERED ([InvoiceTypeId] ASC)
);

