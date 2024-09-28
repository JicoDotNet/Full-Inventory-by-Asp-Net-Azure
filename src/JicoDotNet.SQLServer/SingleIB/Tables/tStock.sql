CREATE TABLE [SingleIB].[tStock] (
    [StockId]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [ProductId]         BIGINT          NULL,
    [WareHouseId]       BIGINT          NULL,
    [CurrentStock]      DECIMAL (18, 5) NULL,
    [Remarks]           NVARCHAR (512)  NULL,
    [GRNOrShipmentId]   BIGINT          NULL,
    [GRNOrShipmentDate] DATETIME        NULL,
    [StockType]         SMALLINT        NULL,
    [IsActive]          BIT             NULL,
    [TransactionDate]   DATETIME        NULL,
    [RequestId]         NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tStock] PRIMARY KEY CLUSTERED ([StockId] ASC)
);

