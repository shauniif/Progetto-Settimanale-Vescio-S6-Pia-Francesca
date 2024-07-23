CREATE TABLE [dbo].[UserRoles] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [IdUser] INT NOT NULL,
    [IdRole] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY ([IdUser]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY ([IdRole]) REFERENCES [Roles]([Id])
);

