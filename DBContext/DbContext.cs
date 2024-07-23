using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DBContext
{
    public class DbContext
    {
        public IUserDao User {  get; set; }
        public IRoleDao Role { get; set; }

        public IUserRoleDao RoleUser { get; set; }

        public DbContext(IUserDao userDao, IRoleDao roleDao, IUserRoleDao roleUserDao) {
            User = userDao;
            Role = roleDao;
            RoleUser = roleUserDao;

        }

    }
}
