CREATE TABLE [dbo].[mPaymentInHeader] (
    [PaymentInId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [CustomerId]      BIGINT          NULL,
    [CompanyBankId]   BIGINT          NULL,
    [IsTDSApplicable] BIT             NULL,
    [TDSPercentage]   DECIMAL (18, 5) NULL,
    [TDSAmount]       DECIMAL (18, 5) NULL,
    [ReceiveAmount]   DECIMAL (18, 5) NULL,
    [TotalAmount]     DECIMAL (18, 5) NULL,
    [PaymentDate]     DATETIME        NULL,
    [PaymentMode]     SMALLINT        NULL,
    [ReferenceNo]     NVARCHAR (64)   NULL,
    [ChequeNo]        NVARCHAR (8)    NULL,
    [ChequeDate]      DATETIME        NULL,
    [ChequeIFSC]      NVARCHAR (16)   NULL,
    [Remarks]         NVARCHAR (512)  NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mPaymentInHeader] PRIMARY KEY CLUSTERED ([PaymentInId] ASC)
);

