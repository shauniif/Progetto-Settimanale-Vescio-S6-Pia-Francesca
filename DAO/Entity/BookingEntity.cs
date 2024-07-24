namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data
{
    public class BookingEntity
    {
        public int Id { get; set; }
        public DateTime DateBooking {  get; set; }

        public int YearProg{  get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public decimal Deposit {  get; set; }

        public decimal Rate { get; set; }

        public string TypeofStay { get; set; }

        public int IdCustomer { get; set; }

        public int IdRoom { get; set; }
    }
}
