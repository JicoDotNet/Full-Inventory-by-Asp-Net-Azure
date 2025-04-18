﻿CREATE TABLE [SingleIB].[mPaymentType] (
    [PaymentTypeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [PaymentTypeName] NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mPaymentType] PRIMARY KEY CLUSTERED ([PaymentTypeId] ASC)
);

