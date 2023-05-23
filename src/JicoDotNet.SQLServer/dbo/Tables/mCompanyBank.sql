CREATE TABLE [dbo].[mCompanyBank] (
    [CompanyBankId]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [AccountName]     NVARCHAR (1024) NULL,
    [AccountNumber]   VARCHAR (64)    NULL,
    [BankName]        NVARCHAR (128)  NULL,
    [IFSC]            NVARCHAR (16)   NULL,
    [MICR]            NVARCHAR (16)   NULL,
    [BranchName]      NVARCHAR (64)   NULL,
    [BranchAddress]   NVARCHAR (128)  NULL,
    [IsPrintable]     BIT             NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mCompanyBank] PRIMARY KEY CLUSTERED ([CompanyBankId] ASC)
);

