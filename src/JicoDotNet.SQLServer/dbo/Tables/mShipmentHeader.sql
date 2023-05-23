CREATE TABLE [dbo].[mShipmentHeader] (
    [ShipmentId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [ShipmentTypeId]       BIGINT         NULL,
    [ShipmentDate]         DATETIME       NULL,
    [ShipmentNumber]       NVARCHAR (16)  NULL,
    [IsDirect]             BIT            NULL,
    [IsFullShipped]        BIT            NULL,
    [SalesOrderId]         BIGINT         NULL,
    [SalesOrderNumber]     NVARCHAR (16)  NULL,
    [WareHouseId]          BIGINT         NULL,
    [VehicleNumber]        NVARCHAR (16)  NULL,
    [HandOverPerson]       NVARCHAR (64)  NULL,
    [HandOverPersonMobile] NVARCHAR (16)  NULL,
    [Remarks]              NVARCHAR (512) NULL,
    [IsActive]             BIT            NULL,
    [TransactionDate]      DATETIME       NULL,
    [RequestId]            NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mShipmentHeader] PRIMARY KEY CLUSTERED ([ShipmentId] ASC)
);

