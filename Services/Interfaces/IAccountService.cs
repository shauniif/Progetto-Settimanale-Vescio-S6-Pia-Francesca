using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces
{
    public interface IAccountService
    {
        UserDto Register(UserDto user);

        UserDto? Login (string username, string password);

        IEnumerable<UserDto> GetAllUsers();

        public UserDto RemoveUser(int id);

        IEnumerable<RoleModel> GetAllRoles();

        UserDto GetUserById(int id);

        RoleModel CreateRole(RoleModel role);
        RoleModel DeleteRole(int id);


        public RoleModel Read(int id);
        RoleModel UpdateModel(int id, RoleModel role);

        bool AddUserToRole(string username, string roleName);
      
        bool RemoveUserFromRole(string username, string roleName);
    }
}
