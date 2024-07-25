namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data
{
    public class BookingServiceEntity
    {
        public int Id { get; set; }
        public int IdAdditionalService { get; set; }

        public int IdBooking {  get; set; }

        public DateTime DateRequestOfService { get; set; }

        public int Quantity { get; set; }
    }
}
