using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Classes
{
    public class CheckOutService : SqlServerServiceBase, ICheckOutService
    {
        private readonly ILogger<ICheckOutService> _logger;
        public CheckOutService(IConfiguration config, ILogger<ICheckOutService> logger) : base(config)
        {
            _logger = logger;
        }

        private const string SEARCH_CLIENT = "SELECT CONCAT(c.LastName, ' ', c.FirstName) as Cliente " +
            "FROM Customers AS c " +
            "JOIN Booking AS b ON c.IdCustomer = b.IdBooking " +
            "WHERE b.IdBooking = @id";
        // Il numero di stanza, il periodo, la tariffa applicata
        private const string ROOM_PERIOD_RATE = "SELECT r.NumberRoom, b.DateStart, b.DateEnd, b.Rate " +
            "FROM Rooms AS r " +
            "JOIN Booking AS b ON r.IdRoom = b.IdRoom " +
            "WHERE b.IdBooking = @id ";
        // La lista di tutti i servizi aggiuntivi richiesti durante il soggiorno,
        private const string TOTAl_ADDSERVICE = "SELECT TypeOfService, Quantity " +
            "FROM AdditionalServices AS addS " +
            "JOIN BookingServices AS BS ON addS.IdAdditionalService = BS.IdAdditionalService " +
            "JOIN Booking AS b On Bs.IdBooking = b.IdBooking " +
            "WHERE b.IdBooking = @id";
        // L’importo da saldare(tariffa – caparra + somma di tutti i servizi aggiuntivi),
        private const string TOTAL_TO_PAY = "SELECT (b.Rate - (b.Deposit + SUM(adds.Price * bs.Quantity))) as TOTALTOPAY " +
            "FROM Booking AS b " +
            "JOIN BookingServices AS bs ON b.IdBooking = bs.IdBooking " +
            "JOIN AdditionalServices AS adds ON bs.IdAdditionalService = adds.IdAdditionalService " +
            "WHERE b.IdBooking = @id " +
            " GROUP BY b.Deposit, b.Rate";

        public string NameOfCostumer(int id)
        { try
            {
                var cmd = GetCommand(SEARCH_CLIENT);
                var conn = GetConnection();
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@id", id));
                var name = (string)cmd.ExecuteScalar()!;
                conn.Close();
                return name;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Customer Not Found");
                throw;
            }
           
        }

        public InformationOfTheCheckOut RoomofCustomer(int id)
        {
            var cmd = GetCommand(ROOM_PERIOD_RATE);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@id", id));
            InformationOfTheCheckOut informationOfTheCheckOut = new InformationOfTheCheckOut();
            var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                informationOfTheCheckOut.NumberRoom = reader.GetString(0);
                informationOfTheCheckOut.DateStart = reader.GetDateTime(1);
                informationOfTheCheckOut.DateEnd = reader.GetDateTime(2);
                informationOfTheCheckOut.Rate = reader.GetDecimal(3);

            }
            conn.Close();
            return informationOfTheCheckOut;
        }

        public Dictionary<string, int> TolalAdditionalService(int id)
        {
            var cmd = GetCommand(TOTAl_ADDSERVICE);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@id", id));
            Dictionary<string,int> AdditionalService = new Dictionary<string,int>();
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                var typeOfService = reader.GetString(0);
                var quantity = reader.GetInt32(1);
                AdditionalService[typeOfService] = quantity;
            }
            conn.Close();
            return AdditionalService;
        }

        public decimal TotalToPay(int id)
        {
            var cmd = GetCommand(TOTAL_TO_PAY);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@id", id));
            var totalToPay = Convert.ToDecimal(cmd.ExecuteScalar())!;
            conn.Close();
            return totalToPay;
        }
    }
}
