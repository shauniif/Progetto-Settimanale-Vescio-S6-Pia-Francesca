using Progetto_Settimanale_Vescio_Pia_Francesca.DBContext;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using System.Data.SqlClient;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Classes
{
    public class BookingService : SqlServerServiceBase, IBookingService
    {
        private readonly DbContext _db;
        private readonly ILogger<IBookingService> _logger;
        public BookingService(DbContext db, ILogger<IBookingService> logger, IConfiguration config) : base(config)
        {
            _db = db;
            _logger = logger;
        }

        private const string RESEARCH_BY_FISCALCODE = "SELECT IdBooking, DateBooking, YearProg, DateStart, DateEnd, Deposit, Rate " +
            "FROM Booking AS b JOIN Customers AS c ON b.IdCustomer = c.IdCustomer " +
            "WHERE c.FiscalCode = @fc";
        private const string RESEARCH_BY_TYPEOFSTAY = "SELECT COUNT(*) " +
            "FROM Booking " +
            "WHERE TypeofStay = @tos";
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
            var b = _db.Booking.Delete(id);
            return new BookingDto { 
                Id = b.Id, 
                DateBooking = b.DateBooking,
                YearProg = b.YearProg,
                DateStart = b.DateStart,
                DateEnd = b.DateEnd,
                Deposit = b.Deposit,
                Rate = b.Rate,
                TypeofStay = b.TypeofStay,
                Customer = _db.Customer.ReadById(b.IdCustomer),
                Room = _db.Rooms.ReadById(b.IdRoom)
            };
        }

        public BookingDto Get(string fiscalCode)
        {
            var cmd = GetCommand(RESEARCH_BY_FISCALCODE);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@fc", fiscalCode));
            var reader = cmd.ExecuteReader();
            BookingDto booking = new BookingDto();
            if (reader.Read())
            {
                booking.Id = reader.GetInt32(0);
                booking.DateBooking = reader.GetDateTime(1);
                booking.YearProg = reader.GetInt32(2);
                booking.DateStart = reader.GetDateTime(3);
                booking.DateEnd = reader.GetDateTime(4);
                booking.Deposit = reader.GetDecimal(5);
                booking.Rate = reader.GetDecimal(6);
            }
            return booking;
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

        public int GetCount(string typeOfStay)
        {

            var cmd = GetCommand(RESEARCH_BY_TYPEOFSTAY);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@tos", typeOfStay));
            var TotalOfCount = (int)cmd.ExecuteScalar()!;
            return TotalOfCount;
        }

        public BookingDto Read(int id)
        {
            var b = _db.Booking.ReadById(id);
            return new BookingDto
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
            };
        }

        public BookingDto Update(int id, BookingDto booking)
        {
            var b = _db.Booking.Update(id, new BookingEntity
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
                TypeofStay = b.TypeofStay,
                Customer = _db.Customer.ReadById(b.IdCustomer),
                Room = _db.Rooms.ReadById(b.IdRoom)
            };
        }
    }
}
