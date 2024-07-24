using Progetto_Settimanale_Vescio_Pia_Francesca.Models;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces
{
    public interface IRoomService
    {
        RoomModel RegisterRoom(RoomModel room);

        RoomModel GetRoom(int id);

        RoomModel UpdateRoom(int id, RoomModel room);

        RoomModel DeleteRoom(int id);
        IEnumerable<RoomModel> GetAll();
    }
}
