using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services;
using System.Data.SqlClient;
using System.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Classes
{
    public class UserRoleDao : SqlServerServiceBase, IUserRoleDao
    {
        public UserRoleDao(IConfiguration config) : base(config)
        {
        }
        private const string INSERT_USERROLE = "INSERT INTO UserRoles(IdUser, IdRole) VALUES(@userId, @roleId)";
        private const string DELETE_USERROLE = "DELETE FROM UserRoles WHERE UserId = @userId AND RoleId = @roleId";
        private const string SELECT_BY_USERNAME = "SELECT r.RoleName " +
            "FROM UserRoles AS ur " +
            "JOIN Roles AS r ON ur.IdRole = r.Id " +
            "JOIN Users AS u ON ur.IdUser = u.Id " +
            "WHERE u.Username = @username";
        public void Create(int userId, int roleId)
        {
            try
            {
                var cmd = GetCommand(INSERT_USERROLE);
                var conn = GetConnection();
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@userId", userId));
                cmd.Parameters.Add(new SqlParameter("@roleId", roleId));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(int userId, int roleId)
        {
            try
            {
                var cmd = GetCommand(DELETE_USERROLE);
                var conn = GetConnection();
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@userId", userId));
                cmd.Parameters.Add(new SqlParameter("@roleId", roleId));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<string> ReadAllByUsername(string username)
        {
             List<string> roles = new List<string>();
            var cmd = GetCommand(SELECT_BY_USERNAME);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@username", username));
            var reader = cmd.ExecuteReader();
            while (reader.Read()) 
            { 
                roles.Add(reader.GetString(0)); 
            }
            return roles;
        }
    }
}
