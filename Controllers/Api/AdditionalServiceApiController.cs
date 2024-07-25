using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class AdditionalServiceApiController : ControllerBase
    {
    public readonly IAdditionalSvcService _additionalSvc;

        public AdditionalServiceApiController(IAdditionalSvcService additionalSvc)
        {
            _additionalSvc = additionalSvc; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<AdditionalServiceModel>> AllAdditionalService()
        {
            return Ok(_additionalSvc.GetAll());

        }
    }
}
