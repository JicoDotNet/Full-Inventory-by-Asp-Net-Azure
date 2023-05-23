CREATE TYPE [dbo].[ShipmentDetailType] AS TABLE (
    [Id]                 INT             NULL,
    [SalesOrderDetailId] BIGINT          NULL,
    [ProductId]          BIGINT          NULL,
    [ShippedQuantity]    DECIMAL (18, 5) NULL,
    [StockDetailId]      BIGINT          NULL,
    [IsPerishable]       BIT             NULL,
    [ExpiryDate]         DATETIME        NULL,
    [BatchNo]            NVARCHAR (256)  NULL,
    [Description]        NVARCHAR (256)  NULL);

