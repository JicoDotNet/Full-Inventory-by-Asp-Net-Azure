CREATE TABLE [dbo].[mSalesOrderHeader] (
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Based on Company''s IsGSTRegistered on the day of SO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mSalesOrderHeader', @level2type = N'COLUMN', @level2name = N'IsGstAllowed';

