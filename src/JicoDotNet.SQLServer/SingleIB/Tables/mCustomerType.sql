CREATE TABLE [SingleIB].[mCustomerType] (
    [CustomerTypeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [CustomerTypeName] NVARCHAR (128) NULL,
    [Description]      NVARCHAR (256) NULL,
    [IsActive]         BIT            NULL,
    [TransactionDate]  DATETIME       NULL,
    [RequestId]        NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mCustomerType] PRIMARY KEY CLUSTERED ([CustomerTypeId] ASC)
);

