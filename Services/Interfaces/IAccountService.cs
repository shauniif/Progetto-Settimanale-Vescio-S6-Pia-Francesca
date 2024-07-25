using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces
{
    public interface IAccountService
    {
        UserDto Register(UserDto user);

        UserDto? Login (string username, string password);

        IEnumerable<UserDto> GetAllUsers();

        List<string> GetAllRoles();

        bool AddUserToRole(string username, string roleName);
      
        bool RemoveUserFromRole(string username, string roleName);
    }
}
