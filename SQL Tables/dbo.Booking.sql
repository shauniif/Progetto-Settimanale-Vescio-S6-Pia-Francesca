CREATE TABLE [dbo].[Booking] (
    [IdBooking]   INT           IDENTITY (1, 1) NOT NULL,
    [DateBooking] DATETIME2 (7) NOT NULL,
    [YearProg]    INT           NOT NULL,
    [DateStart]   DATETIME2 (7) NOT NULL,
    [DateEnd]     DATETIME2 (7) NOT NULL,
    [Deposit]     MONEY         NOT NULL,
    [Rate]        MONEY         NOT NULL,
    [TypeofStay]  NVARCHAR (50) NOT NULL,
    [IdCustomer]  INT           NOT NULL,
    [IdRoom]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IdBooking] ASC),
    CONSTRAINT [FK_Booking_Customers] FOREIGN KEY ([IdCustomer]) REFERENCES [dbo].[Customers] ([IdCustomer]),
    CONSTRAINT [FK_Booking_Rooms] FOREIGN KEY ([IdRoom]) REFERENCES [dbo].[Rooms] ([IdRoom])
);

