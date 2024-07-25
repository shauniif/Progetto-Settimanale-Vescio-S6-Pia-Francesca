using Microsoft.AspNetCore.Rewrite;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services;
using System.Data.Common;
using System.Data.SqlClient;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Classes
{
    public class RoleDao : SqlServerServiceBase, IRoleDao
    {
        public RoleDao(IConfiguration config) : base(config)
        {
        }
        private const string CREATE_ROLE = "INSERT INTO Roles(RoleName) OUTPUT INSERTED.Id VALUES(@roleName)";
        private const string DELETE_ROLE = "DELETE FROM Roles WHERE Id = @roleId";
        private const string SELECT_ROLE_BY_ID = "SELECT Id, RoleName FROM Roles WHERE Id = @roleId";
        private const string SELECT_ROLE_BY_NAME = "SELECT Id, RoleName FROM Roles WHERE RoleName = @roleName";
        private const string SELECT_ALL_ROLES = "SELECT RoleName FROM Roles";
        private const string UPDATE_ROLE = "UPDATE Roles SET RoleName = @roleName WHERE Id = @roleId";

        private RoleEntity CreateReader(DbDataReader reader)
        {
            return new RoleEntity
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                
            };
        }
        public RoleEntity Create(RoleEntity role)
        {
            try
            {
                var cmd = GetCommand(CREATE_ROLE);
                var conn = GetConnection();
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@roleName", role.Name));
                role.Id = (int)cmd.ExecuteScalar();
                conn.Close();
                return role;
            }
            catch (Exception ex) {
                throw;
            }

        }

        public RoleEntity Delete(int roleId)
        {
            try
            {
                var delRole = ReadById(roleId);
                var cmd = GetCommand(DELETE_ROLE);
                var conn = GetConnection();
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@id", roleId));
                cmd.ExecuteNonQuery();
                conn.Close();
                return delRole;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<string> ReadAll()
        {
            List<string> roles = new List<string>();
            var cmd = GetCommand(SELECT_ALL_ROLES);
            var conn = GetConnection();
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                roles.Add(reader.GetString(0));
            }
            conn.Close();
            return roles;
        }

        public RoleEntity ReadById(int idRole)
        {
            var cmd = GetCommand(SELECT_ROLE_BY_ID);
            cmd.Parameters.Add(new SqlParameter("@roleId", idRole));
            var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            RoleEntity role = new RoleEntity();
            if (reader.Read())
                role = CreateReader(reader);
            conn.Close();
            return role;
        }

        public RoleEntity ReadByName(string roleName)
        {
            var cmd = GetCommand(SELECT_ROLE_BY_NAME);
            cmd.Parameters.Add(new SqlParameter("@roleName", roleName));
            var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            RoleEntity role = new RoleEntity();
            if (reader.Read())
                role = CreateReader(reader);
            conn.Close();
            return role;
        }

        public RoleEntity Update(int idRole, RoleEntity role)
        {
            var cmd = GetCommand(UPDATE_ROLE);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@roleName", role.Name));
            cmd.Parameters.Add(new SqlParameter("@roleId", idRole));
            var reader = cmd.ExecuteReader();
            var r = new RoleEntity();
            if (reader.Read())
                r = CreateReader(reader);
            conn.Close();
            return r;

        }

        
    }
}
