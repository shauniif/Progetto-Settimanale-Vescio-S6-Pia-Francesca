using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces
{
    public interface IAdditionalServiceDao
    {
        AdditionalServicesEntity Create(AdditionalServicesEntity additionalservices);

        AdditionalServicesEntity Read(int id);

        IEnumerable<AdditionalServicesEntity> ReadAll();

        AdditionalServicesEntity Update(int id, AdditionalServicesEntity additionalservices);

        AdditionalServicesEntity Delete(int id);
    }
}
