CREATE TYPE [dbo].[SalesOrderDetailType] AS TABLE (
    [Id]                 INT             NULL,
    [SalesOrderDetailId] BIGINT          NULL,
    [SalesOrderId]       BIGINT          NULL,
    [SalesOrderNumber]   NVARCHAR (16)   NULL,
    [AmendmentNumber]    NVARCHAR (4)    NULL,
    [ProductId]          BIGINT          NULL,
    [HSNSAC]             NVARCHAR (8)    NULL,
    [Amount]             DECIMAL (18, 5) NULL,
    [DiscountPercentage] DECIMAL (18, 5) NULL,
    [DiscountAmount]     DECIMAL (18, 5) NULL,
    [Price]              DECIMAL (18, 5) NULL,
    [Quantity]           DECIMAL (18, 5) NULL,
    [SubTotal]           DECIMAL (18, 5) NULL,
    [TaxPercentage]      DECIMAL (18, 5) NULL,
    [TaxAmount]          DECIMAL (18, 5) NULL,
    [Total]              DECIMAL (18, 5) NULL,
    [Description]        NVARCHAR (256)  NULL);

