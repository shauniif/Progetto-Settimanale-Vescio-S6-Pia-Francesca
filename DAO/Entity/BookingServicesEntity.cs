namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data
{
    public class BookingServicesEntity
    {
        public int Id { get; set; }
        public int IdAdditionalService { get; set; }

        public int IdBooking {  get; set; }

        public DateOnly DateRequestOfService { get; set; }

        public int Quantity { get; set; }
    }
}
