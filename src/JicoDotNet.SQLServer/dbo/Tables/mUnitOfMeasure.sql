CREATE TABLE [dbo].[mUnitOfMeasure] (
    [UnitOfMeasureId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [UnitOfMeasureName] NVARCHAR (8)   NULL,
    [Description]       NVARCHAR (256) NULL,
    [IsActive]          BIT            NULL,
    [TransactionDate]   DATETIME       NULL,
    [RequestId]         NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mUnitOfMeasure] PRIMARY KEY CLUSTERED ([UnitOfMeasureId] ASC)
);

