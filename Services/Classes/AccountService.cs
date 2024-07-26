
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.DBContext;

using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Password_Crypth_Implementations;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using System.Data;

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

        public RoleModel CreateRole(RoleModel role)
        {
            var r = _db.Role.Create(new RoleEntity 
            { 
                Id = role.Id, 
                Name = role.Name 
            });
            return new RoleModel { Id = r.Id, Name = r.Name };
        }

        public RoleModel DeleteRole(int id) 
        {
            var r = _db.Role.Delete(id);
            return new RoleModel
            {
                Id = r.Id,
                Name = r.Name
            };
        }
        public UserDto RemoveUser(int id)
        {
            var u = _db.User.Delete(id);
            return new UserDto { Id = u.Id, Username = u.Username };
        }   
        public RoleModel Read(int id)
        {
            var r = _db.Role.ReadById(id);
            return new RoleModel { Id = r.Id, Name = r.Name };
        }
        public RoleModel UpdateModel(int id, RoleModel role)
        {
            var r = _db.Role.Update(id, new RoleEntity
            {
                Id = role.Id,
                Name = role.Name
            });
            return new RoleModel { Id = r.Id, Name = r.Name };
        }

        public IEnumerable<RoleModel> GetAllRoles()
        {
           try {
                var Roles = _db.Role.ReadAll();
                return Roles.Select(x => new RoleModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    
                });
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
                    Roles = _db.RoleUser.ReadAllByUsername(u.Username)
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

        public UserDto GetUserById(int id)
        {
             var u = _db.User.ReadById(id);
                return new UserDto { Id = id, Username = u.Username, Roles = _db.RoleUser.ReadAllByUsername(u.Username) };
        }
    }
}
