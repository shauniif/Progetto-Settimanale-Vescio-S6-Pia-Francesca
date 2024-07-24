using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.DBContext;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Classes
{
    public class AdditionalSvcService : IAdditionalSvcService
    {
        private readonly DbContext _db;
        private readonly ILogger<IAdditionalSvcService> _logger;
        public AdditionalSvcService(DbContext db, ILogger<IAdditionalSvcService> logger)
        {
            _db = db;
            _logger = logger;
        }
        private AdditionalServiceModel rAdditonalServiceModel(AdditionalServicesEntity addS)
        {
            return new AdditionalServiceModel
            {
                Id = addS.Id,
                TypeOfService = addS.TypeOfService,
                Price = addS.Price,
            };
        }
        public AdditionalServiceModel DeleteAdditionalService(int id)
        {
            try
            {
                var addS = _db.AdditionalService.Delete(id);
                return rAdditonalServiceModel(addS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception delting additional service with id: {}", id);
                throw;
            }
        }

        public AdditionalServiceModel GetAdditionalService(int id)
        {
            try
            {

                var addS = _db.AdditionalService.Read(id);
                return rAdditonalServiceModel(addS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception find additional service with id: {}", id);
                throw;
            }
        
        }

        public IEnumerable<AdditionalServiceModel> GetAll()
        { try
            {

                var addServices = _db.AdditionalService.ReadAll();
                return addServices.Select(addS => rAdditonalServiceModel(addS));
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Not found all the additional services");
                throw;
            }
        }

        public AdditionalServiceModel RegisterAdditionalService(AdditionalServiceModel additionalServ)
        { try
            {
                var adds = _db.AdditionalService.Create(
                new AdditionalServicesEntity
                {
                    Id = additionalServ.Id,
                    TypeOfService = additionalServ.TypeOfService,
                    Price = additionalServ.Price,
                });
                return rAdditonalServiceModel(adds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creating don't completed");
                throw;
            }
        }

        public AdditionalServiceModel UpdateAdditionalService(int id, AdditionalServiceModel additionalServ)
        { try
            {

                var adds = _db.AdditionalService.Update(id,
                new AdditionalServicesEntity
                {
                    Id = id,
                    TypeOfService = additionalServ.TypeOfService,
                    Price = additionalServ.Price,
                });
                return rAdditonalServiceModel(adds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Updating don't completed");
                throw;
            }
        }
    }
}
