CREATE TABLE [dbo].[mVendorBank] (
    [VendorBankId]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [VendorId]        BIGINT          NULL,
    [AccountName]     NVARCHAR (1024) NULL,
    [AccountNumber]   NVARCHAR (64)   NULL,
    [BankName]        NVARCHAR (128)  NULL,
    [IFSC]            NVARCHAR (16)   NULL,
    [MICR]            NVARCHAR (16)   NULL,
    [BranchName]      NVARCHAR (64)   NULL,
    [BranchAddress]   NVARCHAR (128)  NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mVendorBank] PRIMARY KEY CLUSTERED ([VendorBankId] ASC)
);

