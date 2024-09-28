CREATE TABLE [SingleIB].[mQuotationHeader] (
    [QuotationId]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [CustomerId]           BIGINT          NULL,
    [IsGstAllowed]         BIT             NULL,
    [QuotationDate]        DATETIME        NULL,
    [QuotationNumber]      NVARCHAR (16)   NULL,
    [QuotationAmount]      DECIMAL (18, 5) NULL,
    [QuotationTaxAmount]   DECIMAL (18, 5) NULL,
    [QuotationTotalAmount] DECIMAL (18, 5) NULL,
    [TandC]                TEXT            NULL,
    [Remarks]              NVARCHAR (512)  NULL,
    [IsActive]             BIT             NULL,
    [TransactionDate]      DATETIME        NULL,
    [RequestId]            NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mQuotationHeader] PRIMARY KEY CLUSTERED ([QuotationId] ASC)
);

