CREATE TABLE [dbo].[mWareHouse] (
    [WareHouseId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [BranchId]        BIGINT         NULL,
    [WareHouseName]   NVARCHAR (64)  NULL,
    [IsRetailCounter] BIT            NULL,
    [Description]     NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [TransactionDate] DATETIME       NULL,
    [RequestId]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_mWareHouse] PRIMARY KEY CLUSTERED ([WareHouseId] ASC)
);

