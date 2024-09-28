CREATE TYPE [SingleIB].[BillDetailType] AS TABLE (
    [Id]                    INT             NULL,
    [PurchaseOrderDetailId] BIGINT          NULL,
    [ProductId]             BIGINT          NULL,
    [HSNSAC]                NVARCHAR (8)    NULL,
    [Price]                 DECIMAL (18, 5) NULL,
    [BilledQuantity]        DECIMAL (18, 5) NULL,
    [SubTotal]              DECIMAL (18, 5) NULL,
    [TaxPercentage]         DECIMAL (18, 5) NULL,
    [CGSTAmount]            DECIMAL (18, 5) NULL,
    [SGSTAmount]            DECIMAL (18, 5) NULL,
    [IGSTAmount]            DECIMAL (18, 5) NULL,
    [Total]                 DECIMAL (18, 5) NULL,
    [Description]           NVARCHAR (256)  NULL);