using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services;
using System.Data.Common;
using System.Data.SqlClient;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Classes
{
    public class RoomDao : SqlServerServiceBase, IRoomDao
    {
        public RoomDao(IConfiguration config) : base(config)
        {
        }
        private const string CREATE_RO = "INSERT INTO " +
            "Rooms(NumberRoom, DescriptionRoom, TypeOfRoom) " +
            "OUTPUT INSERTED.IdRoom " +
            "VALUES(@nr, @dr, @tor)";
        private const string DELETE_RO = "DELETE FROM Rooms WHERE IdRoom = @id";
        private const string SELECT_BY_ID = "SELECT IdRoom, NumberRoom, DescriptionRoom, TypeOfRoom " +
            "FROM Rooms " +
            "WHERE IdRoom = @id";
        private const string SELECT_ALL_ROOMS = "SELECT IdRoom, NumberRoom, DescriptionRoom, TypeOfRoom " +
            "FROM Rooms";
        private const string UPDATE_RO = "UPDATE Rooms SET " +
            "NumberRoom = @nr, " +
            "DescriptionRoom = @dr, " +
            "TypeOfRoom = @tor " +
            "WHERE IdRoom = @id";

        private RoomEntity CreateReader(DbDataReader reader)
        {
            return new RoomEntity
            {
                Id = reader.GetInt32(0),
                NumberRoom = reader.GetString(1),
                DescriptionRoom = reader.GetString(2),
                TypeofRoom = reader.GetString(3),
            };
        }
        public void Paramaters(DbCommand cmd, RoomEntity room)
        {
            cmd.Parameters.Add(new SqlParameter("@nr", room.NumberRoom));
            cmd.Parameters.Add(new SqlParameter("@dr", room.DescriptionRoom));
            cmd.Parameters.Add(new SqlParameter("@tor", room.TypeofRoom));

        }

        public RoomEntity Create(RoomEntity room)
        {
            var cmd = GetCommand(CREATE_RO);
            var conn = GetConnection();
            conn.Open();
            Paramaters(cmd, room);
            room.Id = (int)cmd.ExecuteScalar();
            conn.Close();
            return room;
        }

        public RoomEntity Delete(int id)
        {
            var deleteRoom = ReadById(id);
            var cmd = GetCommand(DELETE_RO);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.ExecuteNonQuery();
            conn.Close();
            return deleteRoom;
        }

        public List<RoomEntity> ReadAll()
        {
            List<RoomEntity> rooms = new List<RoomEntity>();
            var cmd = GetCommand(SELECT_ALL_ROOMS);
            var conn = GetConnection();
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rooms.Add(CreateReader(reader));
            }
            conn.Close();
            return rooms;
        }

        public RoomEntity ReadById(int id)
        {
            var cmd = GetCommand(SELECT_BY_ID);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            RoomEntity room = new RoomEntity();
            if (reader.Read())
                room = CreateReader(reader);
            conn.Close();
            return room;
        }

        public RoomEntity Update(int id, RoomEntity room)
        {
            var cmd = GetCommand(UPDATE_RO);
            var conn = GetConnection();
            Paramaters(cmd, room);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            conn.Open();
            var reader = cmd.ExecuteReader();
            var r = new RoomEntity();
            if (reader.Read())
                r = CreateReader(reader);
            conn.Close();
            return r;
        }
    }
}
