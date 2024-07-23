using Microsoft.Extensions.Logging;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services;
using System.Data.Common;
using System.Data.SqlClient;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Classes
{
    public class UserDao : SqlServerServiceBase, IUserDao
    {
        private readonly ILogger _logger;
        public UserDao(IConfiguration config, ILogger<IUserRoleDao> logger) : base(config)
        {
            _logger = logger;
        }

        private const string CREATE_US = "INSERT INTO Users(Username, Password) OUTPUT INSERTED.Id VALUES(@username, @password)";
        private const string DELETE_US = "DELETE FROM Users WHERE Id = @userId";
        private const string SELECT_USER_BY_ID = "SELECT Id, Username, Password FROM Users WHERE Id = @userId";
        private const string SELECT_USER_BY_USERNAME = "SELECT Id, Username, Password FROM Users WHERE Username = @username";
        private const string SELECT_ALL_USERS = "SELECT Id, Username, Password FROM Users";
        private const string LOGIN = "SELECT Id, Username, Password FROM Users WHERE Username = @username";
        private const string UPDATE_US = "UPDATE Users SET Password = @password WHERE Id = @userId";

        private UserEntity CreateReader(DbDataReader reader)
        {
            return new UserEntity
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                Password = reader.GetString(2),
            };
        }
        public UserEntity Create(UserEntity user)
        {
            try
            {
                var cmd = GetCommand(CREATE_US);
                var conn = GetConnection();
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@username", user.Username));
                cmd.Parameters.Add(new SqlParameter("@password", user.Password));
                user.Id = (int)cmd.ExecuteScalar();
                conn.Close();
                return user;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Exception creating user");
                throw;
            }
        }

        public UserEntity Delete(int userId)
        {
            try
            {
                var deluser = ReadById(userId);
                var cmd = GetCommand(DELETE_US);
                var conn = GetConnection();
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@id", userId));
                cmd.ExecuteNonQuery();
                conn.Close();
                return deluser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception creating user");
                throw;
            }
        }

        public List<UserEntity> GetAll()
        {
           List<UserEntity> users = new List<UserEntity>();
            var cmd = GetCommand(SELECT_ALL_USERS);
            var conn = GetConnection();
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(CreateReader(reader));
            }
            conn.Close();
            return users;
        }

        public UserEntity? Login(string username)
        {
            var cmd = GetCommand(LOGIN);
            var con = GetConnection();
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@username", username));
            using var reader = cmd.ExecuteReader();
            if (reader.Read()) 
                return CreateReader(reader);
            return null;
        }

        public UserEntity ReadById(int userId)
        {
            var cmd = GetCommand(SELECT_USER_BY_ID);
            cmd.Parameters.Add(new SqlParameter("@id", userId));
            var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            UserEntity user = new UserEntity();
            if (reader.Read())
                user = CreateReader(reader);
            conn.Close();
            return user;
        }

        public UserEntity ReadByUserName(string username)
        {

            var cmd = GetCommand(SELECT_USER_BY_USERNAME);
            cmd.Parameters.Add(new SqlParameter("@username", username));
            var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            UserEntity user = new UserEntity();
            if (reader.Read())
                user = CreateReader(reader);
            conn.Close();
            return user;
        }

        public UserEntity Update(int userId, UserEntity user)
        {
            var cmd = GetCommand(UPDATE_US);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@password", user.Password));
            cmd.Parameters.Add(new SqlParameter("@userId", userId));
            var reader = cmd.ExecuteReader();
            var u = new UserEntity();
            if(reader.Read()) 
                u = CreateReader(reader);
            conn.Close();
            return u;
        }
    }
}
