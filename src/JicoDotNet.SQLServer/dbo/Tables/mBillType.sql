CREATE TABLE [dbo].[mBillType] (
    [BillTypeId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [BillTypeName]    NVARCHAR (128) NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mBillType] PRIMARY KEY CLUSTERED ([BillTypeId] ASC)
);

