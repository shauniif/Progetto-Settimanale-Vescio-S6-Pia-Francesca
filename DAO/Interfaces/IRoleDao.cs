using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces
{
    public interface IRoleDao
    {
        RoleEntity Create(RoleEntity role);

        RoleEntity ReadById(int idRole);
        RoleEntity ReadByName(string roleName);

        List<string> ReadAll();
        RoleEntity Update(int idRole, RoleEntity role);

        RoleEntity Delete(int roleId);
        
    }
}
