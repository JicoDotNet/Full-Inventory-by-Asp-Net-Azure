CREATE TYPE [dbo].[StockTransferDetailType] AS TABLE (
    [Id]                INT             NULL,
    [ProductId]         BIGINT          NULL,
    [AvailableQuantity] DECIMAL (18, 5) NULL,
    [TransferQuantity]  DECIMAL (18, 5) NULL,
    [Description]       NVARCHAR (256)  NULL,
    [StockDetailId]     BIGINT          NULL,
    [IsPerishable]      BIT             NULL,
    [BatchNo]           NVARCHAR (256)  NULL,
    [ExpiryDate]        DATETIME        NULL);

