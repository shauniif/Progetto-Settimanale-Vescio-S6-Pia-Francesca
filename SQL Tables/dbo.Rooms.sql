CREATE TABLE [dbo].[Rooms] (
    [IdRoom]          INT            IDENTITY (1, 1) NOT NULL,
    [NumberRoom]      CHAR (3)       NOT NULL,
    [DescriptionRoom] NVARCHAR (MAX) NOT NULL,
    [TypeOfRoom]      NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([IdRoom] ASC)
);

