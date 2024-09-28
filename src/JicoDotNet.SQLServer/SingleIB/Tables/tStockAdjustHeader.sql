CREATE TABLE [SingleIB].[tStockAdjustHeader] (
    [StockAdjustId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [WareHouseId]       BIGINT         NULL,
    [StockAdjustNumber] NVARCHAR (16)  NULL,
    [StockAdjustDate]   DATETIME       NULL,
    [IsStockIncrease]   BIT            NULL,
    [AdjustReasonId]    BIGINT         NULL,
    [AdjustReason]      NVARCHAR (64)  NULL,
    [Remarks]           NVARCHAR (512) NULL,
    [IsActive]          BIT            NULL,
    [TransactionDate]   DATETIME       NULL,
    [RequestId]         NVARCHAR (64)  NULL,
    CONSTRAINT [PK_tStockAdjustHeader] PRIMARY KEY CLUSTERED ([StockAdjustId] ASC)
);

