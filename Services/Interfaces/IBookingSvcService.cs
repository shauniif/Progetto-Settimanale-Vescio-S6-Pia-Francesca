using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces
{
    public interface IBookingSvcService
    {
        BookingServiceDto Create(BookingServiceDto bookingService);

        BookingServiceDto Update(int id, BookingServiceDto bookingService);

        BookingServiceDto Delete(int id);

        BookingServiceDto Read(int id);

        IEnumerable<BookingServiceDto> GetAll();
    }
}
