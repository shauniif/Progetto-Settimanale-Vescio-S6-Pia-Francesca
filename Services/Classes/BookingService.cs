using Progetto_Settimanale_Vescio_Pia_Francesca.DBContext;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Classes
{
    public class BookingService : IBookingService
    {
        private readonly DbContext _db;
        private readonly ILogger<IBookingService> _logger;
        public BookingService(DbContext db, ILogger<IBookingService> logger)
        {
            _db = db;
            _logger = logger;
        }
        public BookingDto Create(BookingDto booking)
        {
            try
            {
               var b =  _db.Booking.Create(
                    new BookingEntity
                    {
                        Id = booking.Id,
                        DateBooking = booking.DateBooking,
                        YearProg = booking.YearProg,
                        DateStart = booking.DateStart,
                        DateEnd = booking.DateEnd,
                        Deposit = booking.Deposit,
                        Rate = booking.Rate,
                        TypeofStay = booking.TypeofStay,
                        IdCustomer = booking.Customer.Id,
                        IdRoom = booking.Room.Id,
                    });
                return new BookingDto 
                { 
                    Id = b.Id, 
                    DateBooking = b.DateBooking, 
                    YearProg = b.YearProg,
                    DateStart = b.DateStart,
                    DateEnd = b.DateEnd,
                    Deposit = b.Deposit,
                    Rate = b.Rate,
                    TypeofStay  = b.TypeofStay,
                    Customer = _db.Customer.ReadById(b.IdCustomer),
                    Room = _db.Rooms.ReadById(b.IdRoom)
                };
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Creating not completed");
                throw;
            }
        }

        public BookingDto Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookingDto> GetAll()
        {
            try
            {
                
                var booking = _db.Booking.ReadAll();
                return booking.Select(b => new BookingDto
                {
                    Id = b.Id,
                    DateBooking = b.DateBooking,
                    YearProg = b.YearProg,
                    DateStart = b.DateStart,
                    DateEnd = b.DateEnd,
                    Deposit = b.Deposit,
                    Rate = b.Rate,
                    TypeofStay = b.TypeofStay,
                    Customer = _db.Customer.ReadById(b.IdCustomer),
                    Room = _db.Rooms.ReadById(b.IdRoom),
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Not found all the Booking");
                throw;
            }
        }

        public BookingDto Read(int id)
        {
            throw new NotImplementedException();
        }

        public BookingDto Update(int id, BookingDto booking)
        {
            throw new NotImplementedException();
        }
    }
}
