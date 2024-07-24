using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.DBContext;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Classes
{
    public class RoomService : IRoomService
    {
        private readonly DbContext _db;

        public RoomService(DbContext db)
        {
            _db = db;
        }
        public RoomModel DeleteRoom(int id)
        {
            var c = _db.Rooms.Delete(id);
            return new RoomModel
            {
                Id = c.Id,
                NumberRoom = c.NumberRoom,
                DescriptionRoom = c.DescriptionRoom,
                TypeofRoom = c.TypeofRoom,
            };
        }

        public IEnumerable<RoomModel> GetAll()
        {
            var rooms = _db.Rooms.ReadAll();
            return rooms.Select(c => new RoomModel
            {
                Id = c.Id,
                NumberRoom = c.NumberRoom,
                DescriptionRoom = c.DescriptionRoom,
                TypeofRoom = c.TypeofRoom
            });
        }

        public RoomModel GetRoom(int id)
        {
            var c = _db.Rooms.ReadById(id);
            return new RoomModel
            {
                Id = c.Id,
                NumberRoom = c.NumberRoom,
                DescriptionRoom = c.DescriptionRoom,
                TypeofRoom = c.TypeofRoom
            };
        }

        public RoomModel RegisterRoom(RoomModel room)
        {
            var c = _db.Rooms.Create(
               new RoomEntity
               {
                   Id = room.Id,
                   NumberRoom = room.NumberRoom,
                   DescriptionRoom = room.DescriptionRoom,
                   TypeofRoom = room.TypeofRoom
               });
            return new RoomModel
            {
                Id = c.Id,
                NumberRoom = c.NumberRoom,
                DescriptionRoom = c.DescriptionRoom,
                TypeofRoom = c.TypeofRoom
            };
        }

        public RoomModel UpdateRoom(int id, RoomModel room)
        {
            var c = _db.Rooms.Update(id,
               new RoomEntity
               {
                   Id = id,
                   NumberRoom = room.NumberRoom,
                   DescriptionRoom = room.DescriptionRoom,
                   TypeofRoom = room.TypeofRoom
               });
            return new RoomModel
            {
                Id = c.Id,
                NumberRoom = c.NumberRoom,
                DescriptionRoom = c.DescriptionRoom,
                TypeofRoom = c.TypeofRoom
            };
        }
    }
}
