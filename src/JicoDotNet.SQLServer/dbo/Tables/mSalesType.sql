CREATE TABLE [dbo].[mSalesType] (
    [SalesTypeId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [SalesTypeName]   NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mSalesType] PRIMARY KEY CLUSTERED ([SalesTypeId] ASC)
);

