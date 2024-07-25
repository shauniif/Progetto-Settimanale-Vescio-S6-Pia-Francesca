using Progetto_Settimanale_Vescio_Pia_Francesca.DBContext;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Classes
{
    public class BookingSvcService : IBookingSvcService
    {
        private readonly DbContext _db;
        private readonly ILogger<IBookingService> _logger;
        private readonly IBookingService _bookingService;
        public BookingSvcService(DbContext db, ILogger<IBookingService> logger, IBookingService bookingService)
        {
            _db = db;
            _logger = logger;
            _bookingService = bookingService;
        }
        public BookingServiceDto Create(BookingServiceDto bookingService)
        {
            try
            {
                var ba = _db.BookingService.Create(new BookingServiceEntity
                {
                    Id = bookingService.Id,
                    IdAdditionalService = bookingService.AdditionalService.Id,
                    DateRequestOfService = bookingService.DateRequestOfService,
                    IdBooking = bookingService.Booking.Id,
                    Quantity = bookingService.Quantity,
                });
                return new BookingServiceDto
                {
                    Id = ba.Id,
                    AdditionalService = _db.AdditionalService.Read(ba.IdAdditionalService),
                    Booking = _bookingService.Read(ba.IdBooking),
                    DateRequestOfService = ba.DateRequestOfService,
                    Quantity = ba.Quantity,
                };
            } catch (Exception ex) 
            {
                _logger.LogError(ex, "Creating not completed");
                throw;
            }
            
        }

        public BookingServiceDto Delete(int id)
        {
            var ba = _db.BookingService.Delete(id);
            return new BookingServiceDto
            {
                Id = ba.Id,
                AdditionalService = _db.AdditionalService.Read(ba.IdAdditionalService),
                DateRequestOfService = ba.DateRequestOfService,
                Booking = _bookingService.Read(ba.IdBooking),
                Quantity = ba.Quantity,
            };
        }

        public IEnumerable<BookingServiceDto> GetAll()
        {
            var bookingServices = _db.BookingService.ReadAll();
            return bookingServices.Select(ba => new BookingServiceDto
            {
                Id = ba.Id,
                AdditionalService = _db.AdditionalService.Read(ba.IdAdditionalService),
                Booking = _bookingService.Read(ba.IdBooking),
                DateRequestOfService = ba.DateRequestOfService,
                Quantity = ba.Quantity,
            });
        }

        public BookingServiceDto Read(int id)
        {
            var ba = _db.BookingService.Read(id);
            return new BookingServiceDto
            {
                Id = ba.Id,
                AdditionalService = _db.AdditionalService.Read(ba.IdAdditionalService),
                DateRequestOfService = ba.DateRequestOfService,
                Booking = _bookingService.Read(ba.IdBooking),
                Quantity = ba.Quantity,
            };
        }

        public BookingServiceDto Update(int id, BookingServiceDto bookingService)
        {
            var ba = _db.BookingService.Update(id, new BookingServiceEntity
            {
                Id = bookingService.Id,
                IdAdditionalService = bookingService.AdditionalService.Id,
                IdBooking = bookingService.Booking.Id,
                DateRequestOfService = bookingService.DateRequestOfService,
                Quantity = bookingService.Quantity,
            });
            return new BookingServiceDto
            {
                Id = ba.Id,
                AdditionalService = _db.AdditionalService.Read(ba.IdAdditionalService),
                Booking = _bookingService.Read(ba.IdBooking),
                DateRequestOfService = ba.DateRequestOfService,
                Quantity = ba.Quantity,
            };
        }
    }
}
