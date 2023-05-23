CREATE TABLE [dbo].[tSmsTrack] (
    [SmsSendId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]            BIGINT         NULL,
    [UserName]          NVARCHAR (64)  NULL,
    [ComponentIdentity] NVARCHAR (64)  NULL,
    [SendTime]          DATETIME       NULL,
    [UrlLink]           NVARCHAR (128) NULL,
    [SmsContent]        NVARCHAR (512) NULL,
    [MobileNo]          NVARCHAR (16)  NULL,
    [IsMobileNoChanged] BIT            NULL,
    [IsResend]          BIT            NULL,
    [SmsFor]            VARCHAR (64)   NULL,
    [IsActive]          BIT            NULL,
    [TransactionDate]   DATETIME       NULL,
    [RequestId]         NVARCHAR (64)  NULL,
    CONSTRAINT [PK_tSmsTrack] PRIMARY KEY CLUSTERED ([SmsSendId] ASC)
);

