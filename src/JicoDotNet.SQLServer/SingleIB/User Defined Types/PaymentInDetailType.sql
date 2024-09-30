CREATE TYPE [SingleIB].[PaymentInDetailType] AS TABLE (
    [Id]             INT             NULL,
    [InvoiceId]      BIGINT          NULL,
    [InvoiceNumber]  NVARCHAR (16)   NULL,
    [Amount]         DECIMAL (18, 5) NULL,
    [IsFullReceived] BIT             NULL,
    [PaymentDate]    DATETIME        NULL,
    [Description]    NVARCHAR (256)  NULL);

