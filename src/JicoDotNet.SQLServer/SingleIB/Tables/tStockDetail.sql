CREATE TABLE [SingleIB].[tStockDetail] (
    [StockDetailId]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [StockId]         BIGINT          NULL,
    [ProductId]       BIGINT          NULL,
    [WareHouseId]     BIGINT          NULL,
    [Stock]           DECIMAL (18, 5) NULL,
    [IsPerishable]    BIT             NULL,
    [ExpiryDate]      DATETIME        NULL,
    [BatchNo]         NVARCHAR (256)  NULL,
    [Remarks]         NVARCHAR (512)  NULL,
    [GRNDate]         DATETIME        NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tStockDetail] PRIMARY KEY CLUSTERED ([StockDetailId] ASC)
);

