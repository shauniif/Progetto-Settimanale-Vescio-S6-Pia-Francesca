namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces
{
    public interface IUserRoleDao
    {
        void Create(int userId, int roleId);

        void Delete(int userId, int roleId);

        List<string> ReadAllByUsername(string username);
    }
}
