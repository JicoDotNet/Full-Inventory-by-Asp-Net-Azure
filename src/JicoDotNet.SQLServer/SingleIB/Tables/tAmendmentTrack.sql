CREATE TABLE [SingleIB].[tAmendmentTrack] (
    [AmendmentTrackId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [AmendmentOf]        NVARCHAR (4)  NULL,
    [AmendmentObjectId]  NVARCHAR (16) NULL,
    [OldAmendmentNumber] NVARCHAR (4)  NULL,
    [AmendmentNumber]    NVARCHAR (4)  NULL,
    [HeaderJson]         TEXT          NULL,
    [DetailJson]         TEXT          NULL,
    [IsActive]           BIT           NULL,
    [TransactionDate]    DATETIME      NULL,
    [RequestId]          NVARCHAR (64) NULL,
    CONSTRAINT [PK_mPurchaseOrderAmendment] PRIMARY KEY CLUSTERED ([AmendmentTrackId] ASC)
);

