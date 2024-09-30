CREATE TYPE [SingleIB].[PaymentOutDetailType] AS TABLE (
    [Id]          INT             NULL,
    [BillId]      BIGINT          NULL,
    [BillNumber]  NVARCHAR (16)   NULL,
    [Amount]      DECIMAL (18, 5) NULL,
    [IsFullPaid]  BIT             NULL,
    [PaymentDate] DATETIME        NULL,
    [Description] NVARCHAR (256)  NULL);

