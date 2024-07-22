CREATE TABLE [dbo].[AdditionalServices] (
    [IdAdditionalService] INT            IDENTITY (1, 1) NOT NULL,
    [TypeOfService]       NVARCHAR (100) NOT NULL,
    [Price]               MONEY          NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAdditionalService] ASC)
);

