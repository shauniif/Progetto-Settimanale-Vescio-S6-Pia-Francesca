
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.DBContext;
using Microsoft.Extensions.Logging;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Password_Crypth_Implementations;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly DbContext _db;
        private readonly IPasswordEncoder _ps;

        public AccountService(DbContext db, IPasswordEncoder ps) {
            _db = db;
            _ps = ps;
        }

        public bool AddUserToRole(string username, string roleName)
        { try
            {
                var user = _db.User.ReadByUserName(username);
                var role = _db.Role.ReadByName(roleName);
                _db.RoleUser.Create(user.Id, role.Id);
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public List<string> GetAllRoles()
        {
           try {
                return _db.Role.ReadAll();
            } 
            catch 
            { 
                return[];
            }
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            try {
                var users = _db.User.GetAll();
                return users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Password = u.Password,
                }).ToList();
            } catch 
            {
                return [];
            };
        }

        public UserDto? Login(string username, string password)
        {
            var u = _db.User.Login(username);
            if (u != null && _ps.IsSame(password, u.Password))
                return new UserDto
                {
                    Id = u.Id,
                    Password = u.Password,
                    Username=u.Username,
                    Roles = _db.RoleUser.ReadAllByUsername(u.Username)
                };
            return null;
        }

        public UserDto Register(UserDto user)
        {
            var u = _db.User.Create(
                new UserEntity
                {
                    Password = _ps.Encode(user.Password),
                    Username = user.Username,
                });
            return new UserDto { Id = u.Id, Password = u.Password, Username = user.Username };
        }

        public bool RemoveUserFromRole(string username, string roleName)
        {try
            {
          var user = _db.User.ReadByUserName(username);
          var role = _db.Role.ReadByName(roleName);
            _db.RoleUser.Delete(user.Id, role.Id);
                return true;
            }
            catch 
            { 
                return false; 
            }
        }
    }
}
