CREATE TABLE [SingleIB].[mProductType] (
    [ProductTypeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [ProductTypeName] NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mProductType] PRIMARY KEY CLUSTERED ([ProductTypeId] ASC)
);

