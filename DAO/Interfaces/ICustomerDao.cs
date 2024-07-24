using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces
{
    public interface ICustomerDao
    {
        
       CustomerEntity Create(CustomerEntity customer);
        CustomerEntity Update(int id, CustomerEntity customer);
        CustomerEntity Delete(int id);
        CustomerEntity ReadById(int id);
        List<CustomerEntity> GetAll();

    }
}
