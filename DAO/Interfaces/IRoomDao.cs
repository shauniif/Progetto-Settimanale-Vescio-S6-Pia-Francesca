using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces
{
    public interface IRoomDao
    {
        RoomEntity Create(RoomEntity room);

        RoomEntity ReadById(int id);

        List<RoomEntity> ReadAll();
        RoomEntity Update(int id, RoomEntity room);

        RoomEntity Delete(int id);
    }
}
