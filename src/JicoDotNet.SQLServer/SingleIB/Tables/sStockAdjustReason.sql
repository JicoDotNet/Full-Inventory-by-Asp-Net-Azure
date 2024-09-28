CREATE TABLE [SingleIB].[sStockAdjustReason] (
    [AdjustReasonId]  BIGINT        IDENTITY (101, 1) NOT NULL,
    [AdjustReason]    NVARCHAR (64) NULL,
    [IsDefault]       BIT           NULL,
    [IsActive]        BIT           NULL,
    [TransactionDate] DATETIME      NULL,
    [RequestId]       NVARCHAR (64) NULL,
    CONSTRAINT [PK_sStockAdjustReason] PRIMARY KEY CLUSTERED ([AdjustReasonId] ASC)
);

