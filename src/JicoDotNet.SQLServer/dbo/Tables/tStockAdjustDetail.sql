CREATE TABLE [dbo].[tStockAdjustDetail] (
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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'if IsStockIncrease = 1 then this is usefull. rest it will set null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tStockAdjustDetail', @level2type = N'COLUMN', @level2name = N'GRNDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'if IsStockIncrease = 0 then this is usefull. rest it will set null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tStockAdjustDetail', @level2type = N'COLUMN', @level2name = N'StockDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'if IsStockIncrease = 0 then this is usefull. rest it will set null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tStockAdjustDetail', @level2type = N'COLUMN', @level2name = N'AvailableQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tStockAdjustDetail', @level2type = N'COLUMN', @level2name = N'IsStockIncrease';

