CREATE TABLE [dbo].[mProduct] (
    [ProductId]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [ProductTypeId]       INT             NULL,
    [ProductInOut]        SMALLINT        NULL,
    [Brand]               NVARCHAR (256)  NULL,
    [ProductName]         NVARCHAR (512)  NULL,
    [ProductCode]         NVARCHAR (256)  NULL,
    [IsGoods]             BIT             NULL,
    [SKU]                 NVARCHAR (128)  NULL,
    [PurchasePrice]       DECIMAL (18, 5) NULL,
    [SalePrice]           DECIMAL (18, 5) NULL,
    [HSNSAC]              NVARCHAR (8)    NULL,
    [TaxPercentage]       DECIMAL (18, 5) NULL,
    [Description]         NVARCHAR (1024) NULL,
    [IsPerishableProduct] BIT             NULL,
    [HasExpirationDate]   BIT             NULL,
    [HasBatchNo]          BIT             NULL,
    [ProductImageUrl]     NVARCHAR (256)  NULL,
    [UnitOfMeasureId]     BIGINT          NULL,
    [UnitOfMeasureName]   NVARCHAR (8)    NULL,
    [IsActive]            BIT             NULL,
    [TransactionDate]     DATETIME        NULL,
    [RequestId]           NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mProduct] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'use this column as NoSql, to avoid joining. As because this column is not editable once inserted', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mProduct', @level2type = N'COLUMN', @level2name = N'UnitOfMeasureName';

