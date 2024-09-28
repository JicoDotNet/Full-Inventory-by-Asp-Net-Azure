CREATE TABLE [SingleIB].[mShipmentDetail] (
    [ShipmentDetailId]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [ShipmentId]         BIGINT          NULL,
    [ShipmentNumber]     NVARCHAR (16)   NULL,
    [SalesOrderDetailId] BIGINT          NULL,
    [ProductId]          BIGINT          NULL,
    [ShippedQuantity]    DECIMAL (18, 5) NULL,
    [StockDetailId]      BIGINT          NULL,
    [IsPerishable]       BIT             NULL,
    [ExpiryDate]         DATETIME        NULL,
    [BatchNo]            NVARCHAR (256)  NULL,
    [Description]        NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [TransactionDate]    DATETIME        NULL,
    [RequestId]          NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mShipmentDetail] PRIMARY KEY CLUSTERED ([ShipmentDetailId] ASC)
);

