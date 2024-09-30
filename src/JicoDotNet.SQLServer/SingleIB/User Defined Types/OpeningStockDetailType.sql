CREATE TYPE [SingleIB].[OpeningStockDetailType] AS TABLE (
    [Id]           INT             NULL,
    [WareHouseId]  BIGINT          NULL,
    [ProductId]    BIGINT          NULL,
    [Quantity]     DECIMAL (18, 5) NULL,
    [GRNDate]      DATETIME        NULL,
    [IsPerishable] BIT             NULL,
    [BatchNo]      NVARCHAR (256)  NULL,
    [ExpiryDate]   DATETIME        NULL,
    [Description]  NVARCHAR (256)  NULL);

