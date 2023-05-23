CREATE TYPE [dbo].[StockAdjustDetailType] AS TABLE (
    [Id]                INT             NULL,
    [ProductId]         BIGINT          NULL,
    [AvailableQuantity] DECIMAL (18, 5) NULL,
    [AdjustQuantity]    DECIMAL (18, 5) NULL,
    [StockDetailId]     BIGINT          NULL,
    [GRNDate]           DATETIME        NULL,
    [IsPerishable]      BIT             NULL,
    [BatchNo]           NVARCHAR (256)  NULL,
    [ExpiryDate]        DATETIME        NULL,
    [Description]       NVARCHAR (256)  NULL);

