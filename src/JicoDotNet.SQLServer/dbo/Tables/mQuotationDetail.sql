CREATE TABLE [dbo].[mQuotationDetail] (
    [QuotationDetailId]  BIGINT          IDENTITY (1, 1) NOT NULL,
    [QuotationId]        BIGINT          NULL,
    [QuotationNumber]    NVARCHAR (16)   NULL,
    [ProductId]          BIGINT          NULL,
    [HSNSAC]             NVARCHAR (8)    NULL,
    [Amount]             DECIMAL (18, 5) NULL,
    [DiscountPercentage] DECIMAL (18, 5) NULL,
    [DiscountAmount]     DECIMAL (18, 5) NULL,
    [Price]              DECIMAL (18, 5) NULL,
    [Quantity]           DECIMAL (18, 5) NULL,
    [SubTotal]           DECIMAL (18, 5) NULL,
    [TaxPercentage]      DECIMAL (18, 5) NULL,
    [TaxAmount]          DECIMAL (18, 5) NULL,
    [Total]              DECIMAL (18, 5) NULL,
    [Description]        NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [TransactionDate]    DATETIME        NULL,
    [RequestId]          NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mQuotationDetail_1] PRIMARY KEY CLUSTERED ([QuotationDetailId] ASC)
);

