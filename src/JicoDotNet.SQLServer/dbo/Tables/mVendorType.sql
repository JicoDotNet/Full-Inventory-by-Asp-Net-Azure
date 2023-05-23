CREATE TABLE [dbo].[mVendorType] (
    [VendorTypeId]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [VendorTypeName]  NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mVendorType] PRIMARY KEY CLUSTERED ([VendorTypeId] ASC)
);

