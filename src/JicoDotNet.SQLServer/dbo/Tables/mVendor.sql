CREATE TABLE [dbo].[mVendor] (
    [VendorId]        BIGINT          IDENTITY (1, 1) NOT NULL,
    [VendorTypeId]    BIGINT          NULL,
    [CompanyName]     NVARCHAR (1204) NULL,
    [CompanyType]     NVARCHAR (64)   NULL,
    [StateCode]       NVARCHAR (2)    NULL,
    [IsGSTRegistered] BIT             NULL,
    [GSTStateCode]    NVARCHAR (2)    NULL,
    [GSTNumber]       NVARCHAR (16)   NULL,
    [PANNumber]       NVARCHAR (16)   NULL,
    [ContactPerson]   NVARCHAR (64)   NULL,
    [Email]           NVARCHAR (64)   NULL,
    [Mobile]          NVARCHAR (16)   NULL,
    [IsActive]        BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       NVARCHAR (64)   NULL,
    CONSTRAINT [PK_mVendor] PRIMARY KEY CLUSTERED ([VendorId] ASC)
);

