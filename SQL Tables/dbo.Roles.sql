CREATE TABLE [dbo].[Roles] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
