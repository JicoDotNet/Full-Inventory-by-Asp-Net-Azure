CREATE TABLE [dbo].[tTDSPay] (
    [TDSPayId]        BIGINT          IDENTITY (1, 1) NOT NULL,
    [PaymentOutId]    BIGINT          NULL,
    [VendorId]        BIGINT          NULL,
    [PANNumber]       NVARCHAR (16)   NULL,
    [TDSAmount]       DECIMAL (18, 5) NULL,
    [IsPaid]          BIT             NULL,
    [PayDate]         DATETIME        NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tTDSPay] PRIMARY KEY CLUSTERED ([TDSPayId] ASC)
);

