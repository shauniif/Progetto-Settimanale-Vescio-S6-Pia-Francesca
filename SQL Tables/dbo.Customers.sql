CREATE TABLE [dbo].[Customers] (
    [IdCustomer]  INT           IDENTITY (1, 1) NOT NULL,
    [LastName]    NVARCHAR (50) NOT NULL,
    [FirstName]   NVARCHAR (50) NOT NULL,
    [City]        NVARCHAR (50) NOT NULL,
    [Province]    NVARCHAR (50) NOT NULL,
    [Email]       NVARCHAR (80) NOT NULL,
    [Telephone]   CHAR (13)     NOT NULL,
    [MobilePhone] CHAR (13)     NOT NULL,
    [TaxCode]     CHAR (16)     NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCustomer] ASC),
    UNIQUE NONCLUSTERED ([TaxCode] ASC)
);

