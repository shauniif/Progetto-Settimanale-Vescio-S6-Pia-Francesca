using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Classes
{
    public class BookingDao : SqlServerServiceBase, IBookingDao
    {
        private readonly ILogger<IBookingDao> _logger;
        public BookingDao(IConfiguration config, ILogger<IBookingDao> logger) : base(config)
        {
            _logger = logger;
        }

        private const string CREATE_BO = "INSERT INTO Booking (DateBooking, YearProg, DateStart, DateEnd, Deposit, Rate, " +
            "TypeofStay, IdCustomer, IdRoom) " +
            "OUTPUT INSERTED.IdBooking " + 
            "VALUES " +
            "(@db, @yp, @ds, @de, @deposit, " +
            "@rate, @tos, @idC, @idR)";
        private const string DELETE_BO = "DELETE FROM Booking WHERE IdBooking = @id";
        private const string SELECT_ALL_BO = "SELECT IdBooking, DateBooking, YearProg, DateStart, DateEnd, Deposit, Rate," +
            "TypeofStay, IdCustomer, IdRoom " +
            "FROM Booking";
        private const string SELECT_BY_ID= "SELECT IdBooking, DateBooking, YearProg, DateStart, DateEnd, Deposit, Rate," +
            "TypeofStay, IdCustomer, IdRoom " +
            "FROM Booking " +
            "WHERE IdBooking = @id";
        private const string UPDATE_BO = "UPDATE Booking SET " +
            "DateBooking = @db, YearProg = @yp , DateStart = @ds, DateEnd = @de, Deposit = @deposit, Rate = @rate, " +
            "TypeofStay = @tos, IdCustomer = @idC, IdRoom = @idR " +
            "WHERE IdBooking = @id";

        private BookingEntity CreateReader(DbDataReader reader)
        {
            return new BookingEntity
            {
                Id = reader.GetInt32(0),
                DateBooking = reader.GetDateTime(1),
                YearProg = reader.GetInt32(2),
                DateEnd = reader.GetDateTime(3),
                DateStart = reader.GetDateTime(4),
                Deposit = reader.GetDecimal(5),
                Rate = reader.GetDecimal(6),
                TypeofStay = reader.GetString(7),
                IdCustomer = reader.GetInt32(8),
                IdRoom = reader.GetInt32(9),

            };
        }
        public void Paramaters(DbCommand cmd, BookingEntity booking)
        {
            cmd.Parameters.Add(new SqlParameter("@db", booking.DateBooking));
            cmd.Parameters.Add(new SqlParameter("@yp", booking.YearProg));
            cmd.Parameters.Add(new SqlParameter("@ds", booking.DateStart));
            cmd.Parameters.Add(new SqlParameter("@de", booking.DateEnd));
            cmd.Parameters.Add(new SqlParameter("@deposit", booking.Deposit));
            cmd.Parameters.Add(new SqlParameter("@rate", booking.Rate));
            cmd.Parameters.Add(new SqlParameter("@tos", booking.TypeofStay));
            cmd.Parameters.Add(new SqlParameter("@idC", booking.IdCustomer));
            cmd.Parameters.Add(new SqlParameter("@idR", booking.IdRoom));
        }
        public BookingEntity Create(BookingEntity booking)
        { try
            {
                var cmd = GetCommand(CREATE_BO);
                var conn = GetConnection();
                conn.Open();
                Paramaters(cmd, booking);
                booking.Id = (int)cmd.ExecuteScalar();
                conn.Close();
                return booking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating");
                throw;
            }
            
        }

        public BookingEntity Delete(int id)
        {
            var booking = ReadById(id);
            var cmd = GetCommand(DELETE_BO);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.ExecuteNonQuery();
            conn.Close();
            return booking;
        }

        public IEnumerable<BookingEntity> ReadAll()
        {
            try
            {

                List<BookingEntity> bookings = new List<BookingEntity>();
                var cmd = GetCommand(SELECT_ALL_BO);
                var conn = GetConnection();
                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bookings.Add(CreateReader(reader));
                }
                conn.Close();
                return bookings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error all booking");
                throw;
            }
        }

        public BookingEntity ReadById(int id)
        { try
            {
            var cmd = GetCommand(SELECT_BY_ID);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            BookingEntity booking = new BookingEntity();
            if (reader.Read())
                booking = CreateReader(reader);
            conn.Close();
            return booking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error find single booking with id: {}", id);
                throw;
            }
        }

        public BookingEntity Update(int id, BookingEntity booking)
        { try {
            var cmd = GetCommand(UPDATE_BO);
            var conn = GetConnection();
            Paramaters(cmd, booking);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            conn.Open();
            var reader = cmd.ExecuteReader();
            var bo = new BookingEntity();
            if (reader.Read())
                bo = CreateReader(reader);
            conn.Close();
            return bo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error update booking with id: {}", id);
                throw;
            }

        }
    }
}
