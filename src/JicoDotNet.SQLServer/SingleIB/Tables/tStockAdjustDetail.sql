CREATE TABLE [SingleIB].[tStockAdjustDetail] (
    [StockAdjustDetailId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [StockAdjustId]       BIGINT          NULL,
    [StockAdjustNumber]   NVARCHAR (16)   NULL,
    [ProductId]           BIGINT          NULL,
    [IsStockIncrease]     BIT             NULL,
    [AvailableQuantity]   DECIMAL (18, 5) NULL,
    [AdjustQuantity]      DECIMAL (18, 5) NULL,
    [StockDetailId]       BIGINT          NULL,
    [GRNDate]             DATETIME        NULL,
    [IsPerishable]        BIT             NULL,
    [ExpiryDate]          DATETIME        NULL,
    [BatchNo]             NVARCHAR (256)  NULL,
    [Description]         NVARCHAR (256)  NULL,
    [IsActive]            BIT             NULL,
    [TransactionDate]     DATETIME        NULL,
    [RequestId]           NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tStockAdjustDetail] PRIMARY KEY CLUSTERED ([StockAdjustDetailId] ASC)
);

