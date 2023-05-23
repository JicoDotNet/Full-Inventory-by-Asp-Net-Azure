CREATE TABLE [dbo].[tStock] (
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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 = Transfer,1 = GRN, -1 = Shipment, 2 = Sales Return, -2 = Purchase Return, OpeningStock = 10, ClosingStock = -10, AdjustAdd = 20, AdjustLess = -20', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tStock', @level2type = N'COLUMN', @level2name = N'StockType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'GRNHeaderDate, ShipmentHeaderDate', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tStock', @level2type = N'COLUMN', @level2name = N'GRNOrShipmentDate';

