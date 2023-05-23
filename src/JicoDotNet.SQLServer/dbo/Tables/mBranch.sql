CREATE TABLE [dbo].[mBranch] (
    [BranchId]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [BranchName]      NVARCHAR (64)  NULL,
    [BranchCode]      NVARCHAR (64)  NULL,
    [Address]         NVARCHAR (128) NULL,
    [City]            NVARCHAR (32)  NULL,
    [State]           NVARCHAR (2)   NULL,
    [PostalCode]      NVARCHAR (8)   NULL,
    [ContactPerson]   NVARCHAR (64)  NULL,
    [Email]           NVARCHAR (64)  NULL,
    [Phone]           NVARCHAR (16)  NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mBranch] PRIMARY KEY CLUSTERED ([BranchId] ASC)
);

