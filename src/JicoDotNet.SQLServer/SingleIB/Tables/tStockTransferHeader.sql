CREATE TABLE [SingleIB].[tStockTransferHeader] (
    [StockTransferId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [StockTransferNumber] NVARCHAR (16)  NULL,
    [StockTransferDate]   DATETIME       NULL,
    [FromWareHouseId]     BIGINT         NULL,
    [ToWareHouseId]       BIGINT         NULL,
    [Remarks]             NVARCHAR (512) NULL,
    [IsActive]            BIT            NULL,
    [TransactionDate]     DATETIME       NULL,
    [RequestId]           NVARCHAR (64)  NULL,
    CONSTRAINT [PK_tStockTransferHeader] PRIMARY KEY CLUSTERED ([StockTransferId] ASC)
);

