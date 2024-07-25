using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces
{
    public interface IBookingServiceDao
    {
        BookingServiceEntity Create(BookingServiceEntity bookingService);
        BookingServiceEntity Update(int id, BookingServiceEntity bookingService);

        BookingServiceEntity Delete(int id);

        BookingServiceEntity Read(int id);

        IEnumerable<BookingServiceEntity> ReadAll();
    }
}
