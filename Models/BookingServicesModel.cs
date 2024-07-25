using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Models
{
    public class BookingServicesModel
    {
        public int Id { get; set; }
        public AdditionalServiceModel AdditionalService { get; set; }

        public BookingModel Booking { get; set; }

        public DateTime DateRequestOfService { get; set; }

        public int Quantity { get; set; }
    }
}
