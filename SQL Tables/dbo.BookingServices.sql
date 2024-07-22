CREATE TABLE [dbo].[BookingServices] (
    [IdBookingServices]    INT           IDENTITY (1, 1) NOT NULL,
    [IdAdditionalService]  INT           NOT NULL,
    [IdBooking]            INT           NOT NULL,
    [DateRequestOfService] DATETIME2 (7) NOT NULL,
    [Quantity]             INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IdBookingServices] ASC),
    CONSTRAINT [FK_BookingServices_AdditionalServices] FOREIGN KEY ([IdAdditionalService]) REFERENCES [dbo].[AdditionalServices] ([IdAdditionalService]),
    CONSTRAINT [FK_BookingServices_Booking] FOREIGN KEY ([IdBooking]) REFERENCES [dbo].[Booking] ([IdBooking])
);

