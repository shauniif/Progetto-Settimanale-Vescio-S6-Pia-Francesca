using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces
{
    public interface IBookingDao
    {
        BookingEntity Create(BookingEntity booking);
        BookingEntity ReadById(int id);
        IEnumerable<BookingEntity> ReadAll();
        BookingEntity Update(int id, BookingEntity booking);
        BookingEntity Delete(int id);
    }
}
