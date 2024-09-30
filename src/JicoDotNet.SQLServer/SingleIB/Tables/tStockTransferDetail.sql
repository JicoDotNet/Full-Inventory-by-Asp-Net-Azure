CREATE TABLE [SingleIB].[tStockTransferDetail] (
    [StockTransferDetailId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [StockTransferId]       BIGINT          NULL,
    [StockTransferNumber]   NVARCHAR (16)   NULL,
    [ProductId]             BIGINT          NULL,
    [AvailableQuantity]     DECIMAL (18, 5) NULL,
    [TransferQuantity]      DECIMAL (18, 5) NULL,
    [StockDetailId]         BIGINT          NULL,
    [IsPerishable]          BIT             NULL,
    [ExpiryDate]            DATETIME        NULL,
    [BatchNo]               NVARCHAR (256)  NULL,
    [Description]           NVARCHAR (256)  NULL,
    [IsActive]              BIT             NULL,
    [TransactionDate]       DATETIME        NULL,
    [RequestId]             NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tStockTransferDetail] PRIMARY KEY CLUSTERED ([StockTransferDetailId] ASC)
);

