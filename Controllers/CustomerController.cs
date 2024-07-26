using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerSvc;
        public CustomerController(ICustomerService customerSvc)
        {
            _customerSvc = customerSvc;
        }
        public IActionResult AllCustomer()
        {
            var customers = _customerSvc.GetAll();
            return View(customers);
        }
        public IActionResult RegisterCustomer()
        {
            return View(new CustomerModel());
        }
        [HttpPost]
        public IActionResult RegisterCustomer(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                _customerSvc.RegisterCustomer(model);
                return RedirectToAction("AllCustomer", "Customer");
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult UpdateCustomer(int id)
        {
            var c = _customerSvc.GetCustomer(id);
            return View(c);
        }

        [HttpPost]
        public IActionResult UpdateCustomer(int id, CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                _customerSvc.UpdateCustomer(id, model);
                return RedirectToAction("AllCustomer", "Customer");
            }
            else
            {
                return View(model);

            }
        }

       
        public IActionResult DeleteCustomer(int id) { 
            _customerSvc.DeleteCustomer(id);
            return RedirectToAction("AllCustomer", "Customer");
        }
    }
}
