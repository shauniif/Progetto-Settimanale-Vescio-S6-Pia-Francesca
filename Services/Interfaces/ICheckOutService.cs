using Progetto_Settimanale_Vescio_Pia_Francesca.Models;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces
{
    public interface ICheckOutService
    {
        string NameOfCostumer(int id);
        
        InformationOfTheCheckOut RoomofCustomer(int id);

        IEnumerable<AdditionalServiceCheckOutModel> TolalAdditionalService(int id);

        TotalToPayModel TotalToPay(int id);

    }
}
