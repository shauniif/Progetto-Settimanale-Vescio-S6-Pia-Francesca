using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        public readonly ICheckOutService _checkoutService;
        public CheckOutController(ICheckOutService checkOutService) 
        {
            _checkoutService = checkOutService;
        }
        public IActionResult Checkout(int id)
        {   
            TotalCheckOut totalCheckOut = new TotalCheckOut();
            totalCheckOut.NameCustomer = _checkoutService.NameOfCostumer(id);
            totalCheckOut.RoomofCustomer = _checkoutService.RoomofCustomer(id);
            totalCheckOut.TolalAdditionalService = _checkoutService.TolalAdditionalService(id);
            totalCheckOut.TotalToPay = _checkoutService.TotalToPay(id);

            return View(totalCheckOut);
        }
    }
}
