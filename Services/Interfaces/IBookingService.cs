using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces
{
    public interface IBookingService
    {
        BookingDto Create(BookingDto booking);

        BookingDto Update(int id, BookingDto booking);

        BookingDto Delete(int id);

        BookingDto Read(int id);

        IEnumerable<BookingDto> GetAll();

        BookingDto Get(string fiscalCode);

        int GetCount(string typeOfStay);
    }
}
