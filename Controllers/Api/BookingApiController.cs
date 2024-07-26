using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingApiController : ControllerBase
    {
        public readonly IBookingService _bookingService;

        public BookingApiController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookingModel>> AllAdditionalService()
        {
            return Ok(_bookingService.GetAll());

        }
        [HttpGet("{fiscalCode}")]
        public ActionResult<IEnumerable<BookingModel>> ResearcByFiscalCode([FromRoute] string fiscalCode) 
        {
            var booking= _bookingService.Get(fiscalCode);
            var bModel = booking.Select(b => new BookingModel
            {
                Id = b.Id,
                DateBooking = b.DateBooking,
                YearProg = b.YearProg,
                DateStart = b.DateStart,
                DateEnd = b.DateEnd,
                Deposit = b.Deposit,
                Rate = b.Rate,
            });

            return Ok(bModel);
        }

        [HttpGet("Count/{typeOfStay}")]
        public ActionResult<int> ResarchByTypeOfStay([FromRoute] string typeOfStay) 
        {
            var count = _bookingService.GetCount(typeOfStay);
            return Ok(count);
        }
    }
}
