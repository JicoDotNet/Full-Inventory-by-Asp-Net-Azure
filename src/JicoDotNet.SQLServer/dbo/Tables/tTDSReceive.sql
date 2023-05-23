CREATE TABLE [dbo].[tTDSReceive] (
    [TDSReceiveId]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [PaymentInId]     BIGINT          NULL,
    [CustomerId]      BIGINT          NULL,
    [PANNumber]       NVARCHAR (16)   NULL,
    [TDSAmount]       DECIMAL (18, 5) NULL,
    [IsReceived]      BIT             NULL,
    [ReceivedDate]    DATETIME        NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_tTDSReceive] PRIMARY KEY CLUSTERED ([TDSReceiveId] ASC)
);

