using Progetto_Settimanale_Vescio_Pia_Francesca.Models;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces
{
    public interface ICustomerService
    {
        CustomerModel RegisterCustomer(CustomerModel customer);

        CustomerModel GetCustomer(int id);

        CustomerModel UpdateCustomer(int id, CustomerModel customer);

        CustomerModel DeleteCustomer(int id);
        IEnumerable<CustomerModel> GetAll();

    }
}
