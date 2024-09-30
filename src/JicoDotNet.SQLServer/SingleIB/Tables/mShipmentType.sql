CREATE TABLE [SingleIB].[mShipmentType] (
    [ShipmentTypeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [ShipmentTypeName] NVARCHAR (128) NULL,
    [Description]      NVARCHAR (256) NULL,
    [IsActive]         BIT            NULL,
    [TransactionDate]  DATETIME       NULL,
    [RequestId]        NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mShipmentType] PRIMARY KEY CLUSTERED ([ShipmentTypeId] ASC)
);

