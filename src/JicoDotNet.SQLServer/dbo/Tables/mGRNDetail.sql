CREATE TABLE [dbo].[mGRNDetail] (
    [GRNDetailId]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [GRNId]                 BIGINT          NULL,
    [GRNNumber]             NVARCHAR (16)   NULL,
    [PurchaseOrderDetailId] BIGINT          NULL,
    [ProductId]             BIGINT          NULL,
    [ReceivedQuantity]      DECIMAL (18, 5) NULL,
    [IsPerishable]          BIT             NULL,
    [ExpiryDate]            DATETIME        NULL,
    [BatchNo]               NVARCHAR (256)  NULL,
    [Description]           NVARCHAR (256)  NULL,
    [IsActive]              BIT             NULL,
    [TransactionDate]       DATETIME        NULL,
    [RequestId]             NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mGRNDetail] PRIMARY KEY CLUSTERED ([GRNDetailId] ASC)
);

