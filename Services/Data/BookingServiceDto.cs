using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data
{
    public class BookingServiceDto
    {
        public int Id { get; set; }
        public AdditionalServicesEntity AdditionalService { get; set; }

        public BookingDto Booking { get; set; }

        public DateTime DateRequestOfService { get; set; }

        public int Quantity { get; set; }
    }
}
