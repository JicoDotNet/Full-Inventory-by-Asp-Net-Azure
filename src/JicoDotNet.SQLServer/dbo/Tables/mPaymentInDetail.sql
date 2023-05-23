CREATE TABLE [dbo].[mPaymentInDetail] (
    [PaymentInDetailId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [PaymentInId]       BIGINT          NULL,
    [InvoiceId]         BIGINT          NULL,
    [InvoiceNumber]     NVARCHAR (16)   NULL,
    [Amount]            DECIMAL (18, 5) NULL,
    [IsFullReceived]    BIT             NULL,
    [PaymentDate]       DATETIME        NULL,
    [Description]       NVARCHAR (256)  NULL,
    [IsActive]          BIT             NULL,
    [TransactionDate]   DATETIME        NULL,
    [RequestId]         NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mPaymentInDetail] PRIMARY KEY CLUSTERED ([PaymentInDetailId] ASC)
);

