CREATE TABLE [dbo].[mCustomer] (
    [CustomerId]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [CustomerTypeId]   BIGINT          NULL,
    [CompanyName]      NVARCHAR (1204) NULL,
    [CompanyType]      NVARCHAR (64)   NULL,
    [StateCode]        NVARCHAR (2)    NULL,
    [IsGSTRegistered]  BIT             NULL,
    [GSTStateCode]     NVARCHAR (2)    NULL,
    [GSTNumber]        NVARCHAR (16)   NULL,
    [PANNumber]        NVARCHAR (16)   NULL,
    [ContactPerson]    NVARCHAR (64)   NULL,
    [Email]            NVARCHAR (64)   NULL,
    [Mobile]           NVARCHAR (16)   NULL,
    [IsRetailCustomer] BIT             NULL,
    [IsActive]         BIT             NULL,
    [TransactionDate]  DATETIME        NULL,
    [RequestId]        NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mCustomer] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);

