CREATE TABLE [dbo].[mPaymentOutDetail] (
    [PaymentOutDetailId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [PaymentOutId]       BIGINT          NULL,
    [BillId]             BIGINT          NULL,
    [BillNumber]         NVARCHAR (16)   NULL,
    [Amount]             DECIMAL (18, 5) NULL,
    [IsFullPaid]         BIT             NULL,
    [PaymentDate]        DATETIME        NULL,
    [Description]        NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [TransactionDate]    DATETIME        NULL,
    [RequestId]          NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mPaymentOutDetail] PRIMARY KEY CLUSTERED ([PaymentOutDetailId] ASC)
);

