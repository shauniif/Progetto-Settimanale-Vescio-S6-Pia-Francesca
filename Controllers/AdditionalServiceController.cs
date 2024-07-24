using Microsoft.AspNetCore.Mvc;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Controllers
{
    public class AdditionalServiceController : Controller
    {
        private readonly IAdditionalSvcService _additionalSvcService;

        public AdditionalServiceController(IAdditionalSvcService additionalSvcService)
        {
            _additionalSvcService = additionalSvcService;
        }
        public IActionResult AllAdditionalServices()
        {
            var addServices = _additionalSvcService.GetAll();
            return View(addServices);
        }

        public IActionResult RegisterAdditionalService()
        {
            return View(new AdditionalServiceModel()); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterAdditionalService(AdditionalServiceModel additionalServiceModel)
        {
            if(ModelState.IsValid)
            {
                _additionalSvcService.RegisterAdditionalService(additionalServiceModel);
                return RedirectToAction("AllAdditionalServices", "AdditionalService");
            } else 
            { 
                return View(additionalServiceModel); 
            }
        }

        public IActionResult UpdateAdditionalService(int id)
        {
            var adds = _additionalSvcService.GetAdditionalService(id);
            return View(adds);
        }

        [HttpPost]
        public IActionResult UpdateAdditionalService(int id, AdditionalServiceModel model)
        {
            if (ModelState.IsValid)
            {
                _additionalSvcService.UpdateAdditionalService(id, model);
                return RedirectToAction("AllAdditionalServices", "AdditionalService");
            }
            else
            {
                return View(model);

            }
        }

        public IActionResult DeleteRoom(int id)
        {
            _additionalSvcService.DeleteAdditionalService(id);
            return RedirectToAction("AllAdditionalServices", "AdditionalService");
        }
    }
}
