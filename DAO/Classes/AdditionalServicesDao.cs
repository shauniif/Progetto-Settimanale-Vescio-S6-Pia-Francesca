using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services;
using System.Data.Common;
using System.Data.SqlClient;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Classes
{
    public class AdditionalServicesDao : SqlServerServiceBase, IAdditionalServiceDao
    {
        public AdditionalServicesDao(IConfiguration config) : base(config)
        {
        }
        private const string CREATE_AS = "INSERT INTO " +
           "AdditionalServices(TypeOfService, Price) " +
           "OUTPUT INSERTED.IdAdditionalService " +
           "VALUES(@tos, @price)";
        private const string DELETE_AS = "DELETE FROM AdditionalServices WHERE IdAdditionalService = @id";
        private const string SELECT_BY_ID = "SELECT IdAdditionalService, TypeOfService, Price " +
            "FROM AdditionalServices " +
            "WHERE IdAdditionalService = @id";
        private const string SELECT_ALL_AS = "SELECT IdAdditionalService, TypeOfService, Price " +
            "FROM AdditionalServices";
        private const string UPDATE_AS = "UPDATE AdditionalServices SET " +
            "TypeOfService = @tos, " +
            "Price = @price " +
            "WHERE IdAdditionalService = @id";

        private AdditionalServicesEntity CreateReader(DbDataReader reader)
        {
            return new AdditionalServicesEntity
            {
                Id = reader.GetInt32(0),
                TypeOfService = reader.GetString(1),
                Price = reader.GetDecimal(2),
            };
        }
        public void Paramaters(DbCommand cmd, AdditionalServicesEntity additionalService)
        {
            cmd.Parameters.Add(new SqlParameter("@tos", additionalService.TypeOfService));
            cmd.Parameters.Add(new SqlParameter("@price", additionalService.Price));

        }
        public AdditionalServicesEntity Create(AdditionalServicesEntity additionalservices)
        {
            var cmd = GetCommand(CREATE_AS);
            var conn = GetConnection();
            conn.Open();
            Paramaters(cmd, additionalservices);
            additionalservices.Id = (int)cmd.ExecuteScalar();
            conn.Close();
            return additionalservices;
        }

        public AdditionalServicesEntity Delete(int id)
        {
            var deleteAdditionalService = Read(id);
            var cmd = GetCommand(DELETE_AS);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.ExecuteNonQuery();
            conn.Close();
            return deleteAdditionalService;
        }

        public AdditionalServicesEntity Read(int id)
        {
            var cmd = GetCommand(SELECT_BY_ID);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            AdditionalServicesEntity additionalService = new AdditionalServicesEntity();
            if (reader.Read())
                additionalService = CreateReader(reader);
            conn.Close();
            return additionalService;
        }

        public IEnumerable<AdditionalServicesEntity> ReadAll()
        {
            List<AdditionalServicesEntity> additionalServices= new List<AdditionalServicesEntity>();
            var cmd = GetCommand(SELECT_ALL_AS);
            var conn = GetConnection();
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                additionalServices.Add(CreateReader(reader));
            }
            conn.Close();
            return additionalServices;
        }

        public AdditionalServicesEntity Update(int id, AdditionalServicesEntity additionalservices)
        {
            var cmd = GetCommand(UPDATE_AS);
            var conn = GetConnection();
            Paramaters(cmd, additionalservices);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            conn.Open();
            var reader = cmd.ExecuteReader();
            var addS = new AdditionalServicesEntity();
            if (reader.Read())
                addS = CreateReader(reader);
            conn.Close();
            return addS;
        }
    }
}
