using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services;
using System.Data.Common;
using System.Data.SqlClient;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Classes
{
    public class BookingServiceDao : SqlServerServiceBase, IBookingServiceDao
    {
        public readonly ILogger<IBookingServiceDao> _logger;
        public BookingServiceDao(IConfiguration config, ILogger<IBookingServiceDao> logger) : base(config)
        {
            _logger = logger;
        }
        private const string CREATE_BA = "INSERT INTO BookingServices (IdAdditionalService, IdBooking, " +
            "DateRequestOfService, Quantity) " +
            "OUTPUT INSERTED.IdBookingServices " +
            "VALUES " +
            "(@IdAdd, @IdB, @DROS, @q)";
        private const string DELETE_BA= "DELETE FROM BookingServices WHERE IdBookingServices = @id";
        private const string SELECT_ALL_BA= "SELECT IdBookingServices, IdAdditionalService, IdBooking, DateRequestOfService, Quantity " +
            "FROM BookingServices";
        private const string SELECT_BY_ID = "SELECT IdBookingServices, IdAdditionalService, IdBooking, DateRequestOfService, Quantity " +
            "FROM BookingServices " +
            "WHERE IdBookingServices = @id";
        private const string UPDATE_BA= "UPDATE BookingServices SET " +
            "IdAdditionalService =  @IdAdd," +
            "IdBooking =  @IdB, " +
            "DateRequestOfService = @DROS, " +
            "Quantity = @q " +
            "WHERE IdBookingServices = @id";

        private BookingServiceEntity CreateReader(DbDataReader reader)
        {
            return new BookingServiceEntity
            {
                Id = reader.GetInt32(0),
                IdAdditionalService = reader.GetInt32(1),
                IdBooking = reader.GetInt32(2),
                DateRequestOfService = reader.GetDateTime(3),
                Quantity = reader.GetInt32(4),
            };
        }
        public void Paramaters(DbCommand cmd, BookingServiceEntity bookingService)
        {
            cmd.Parameters.Add(new SqlParameter("@IdAdd", bookingService.IdAdditionalService));
            cmd.Parameters.Add(new SqlParameter("@IdB", bookingService.IdBooking));
            cmd.Parameters.Add(new SqlParameter("@DROS", bookingService.DateRequestOfService));
            cmd.Parameters.Add(new SqlParameter("@q", bookingService.Quantity));
        }

        public BookingServiceEntity Create(BookingServiceEntity bookingService)
        {
            try
            {
                var cmd = GetCommand(CREATE_BA);
                var conn = GetConnection();
                conn.Open();
                Paramaters(cmd, bookingService);
                bookingService.Id = (int)cmd.ExecuteScalar();
                conn.Close();
                return bookingService;
            }   
            catch (Exception ex) {
                _logger.LogError(ex, "Error creating");
                throw;
            }
        }

        public BookingServiceEntity Delete(int id)
        {
            var bookingService = Read(id);
            var cmd = GetCommand(DELETE_BA);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.ExecuteNonQuery();
            conn.Close();
            return bookingService;
        }

        public BookingServiceEntity Read(int id)
        {
            {
                try
                {
                    var cmd = GetCommand(SELECT_BY_ID);
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    var conn = GetConnection();
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    BookingServiceEntity bookingService = new BookingServiceEntity();
                    if (reader.Read())
                        bookingService = CreateReader(reader);
                    conn.Close();
                    return bookingService;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error find single booking with id: {}", id);
                    throw;
                }
            }
        }

        public IEnumerable<BookingServiceEntity> ReadAll()
        {
            try
            {

                List<BookingServiceEntity> bookingServices = new List<BookingServiceEntity>();
                var cmd = GetCommand(SELECT_ALL_BA);
                var conn = GetConnection();
                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bookingServices.Add(CreateReader(reader));
                }
                conn.Close();
                return bookingServices;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error all booking");
                throw;
            }
        }

        public BookingServiceEntity Update(int id, BookingServiceEntity bookingService)
        {
            try
            {
                var cmd = GetCommand(UPDATE_BA);
                var conn = GetConnection();
                Paramaters(cmd, bookingService);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                conn.Open();
                var reader = cmd.ExecuteReader();
                var ba = new BookingServiceEntity();
                if (reader.Read())
                    ba = CreateReader(reader);
                conn.Close();
                return ba;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error update bookingService with id: {}", id);
                throw;
            }
        }
    }
}
