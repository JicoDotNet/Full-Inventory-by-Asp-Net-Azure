CREATE TABLE [SingleIB].[mPaymentOutHeader] (
    [PaymentOutId]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [VendorId]        BIGINT          NULL,
    [VendorBankId]    BIGINT          NULL,
    [IsTDSApplicable] BIT             NULL,
    [TDSPercentage]   DECIMAL (18, 5) NULL,
    [TDSAmount]       DECIMAL (18, 5) NULL,
    [PayAmount]       DECIMAL (18, 5) NULL,
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
    CONSTRAINT [PK_mPaymentOut] PRIMARY KEY CLUSTERED ([PaymentOutId] ASC)
);

