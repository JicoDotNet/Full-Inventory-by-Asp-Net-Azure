CREATE TYPE [dbo].[GRNDetailType] AS TABLE (
    [Id]                    INT             NULL,
    [PurchaseOrderDetailId] BIGINT          NULL,
    [ProductId]             BIGINT          NULL,
    [ReceivedQuantity]      DECIMAL (18, 5) NULL,
    [Description]           NVARCHAR (256)  NULL,
    [IsPerishable]          BIT             NULL,
    [BatchNo]               NVARCHAR (256)  NULL,
    [ExpiryDate]            DATETIME        NULL);

