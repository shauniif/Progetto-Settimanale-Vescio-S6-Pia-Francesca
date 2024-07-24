using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Models
{
    public class BookingModel
    {
        public int Id { get; set; }
        public DateTime DateBooking { get; set; }

        public int YearProg { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public decimal Deposit { get; set; }

        public decimal Rate { get; set; }

        public string TypeofStay { get; set; }

        public CustomerModel Customer { get; set; }

        public RoomModel Room { get; set; }
    }
}
