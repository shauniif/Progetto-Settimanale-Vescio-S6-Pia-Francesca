using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces
{
    public interface IUserDao
    {
        UserEntity Create(UserEntity user);

        UserEntity Update(int userId, UserEntity user);

        UserEntity Delete(int userId);

        UserEntity ReadByUserName(string username);

        UserEntity ReadById(int userId);

        List<UserEntity> GetAll();

        UserEntity? Login(string username);
    }
}
