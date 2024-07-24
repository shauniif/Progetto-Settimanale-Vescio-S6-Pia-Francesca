using Progetto_Settimanale_Vescio_Pia_Francesca.Models;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces
{
    public interface IAdditionalSvcService
    {
        AdditionalServiceModel RegisterAdditionalService(AdditionalServiceModel additionalServ);

        AdditionalServiceModel GetAdditionalService(int id);

        AdditionalServiceModel UpdateAdditionalService(int id, AdditionalServiceModel additionalServ);

        AdditionalServiceModel DeleteAdditionalService(int id);
        IEnumerable<AdditionalServiceModel> GetAll();
    }
}
