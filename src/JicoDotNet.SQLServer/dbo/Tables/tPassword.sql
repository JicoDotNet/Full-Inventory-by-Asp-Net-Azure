CREATE TABLE [dbo].[tPassword] (
    [PasswordId]      INT             IDENTITY (1, 1) NOT NULL,
    [UserId]          INT             NULL,
    [PasswordHash]    NVARCHAR (2048) NULL,
    [PasswordSalt]    NVARCHAR (2048) NULL,
    [IsActive]        BIT             NULL,
    [IsChangeable]    BIT             NULL,
    [TransactionDate] DATETIME        NULL,
    [RequestId]       VARCHAR (32)    NULL,
    CONSTRAINT [PK_tPassword] PRIMARY KEY CLUSTERED ([PasswordId] ASC)
);

