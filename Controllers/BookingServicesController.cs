using Microsoft.AspNetCore.Mvc;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Microsoft.AspNetCore.Authorization;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Controllers
{
    [Authorize]
    public class BookingServicesController : Controller
    {
        private readonly IBookingSvcService _bookingSvcService;

        public BookingServicesController(IBookingSvcService bookingSvcService)
        {
            _bookingSvcService = bookingSvcService;
        }
        public IActionResult AllBookingServices()
        {
            var bookingServices = _bookingSvcService.GetAll();
            var bookingServicesModel = bookingServices.Select(ba => new BookingServicesModel
            {
                Id = ba.Id,
                AdditionalService = new AdditionalServiceModel
                {
                    Id = ba.AdditionalService.Id,
                    TypeOfService = ba.AdditionalService.TypeOfService,
                    Price = ba.AdditionalService.Price,
                },
                Booking = new BookingModel
                {
                    Id = ba.Booking.Id,
                    DateBooking = ba.Booking.DateBooking,
                    YearProg = ba.Booking.YearProg,
                    DateStart = ba.Booking.DateStart,
                    DateEnd = ba.Booking.DateEnd,
                    Deposit = ba.Booking.Deposit,
                    Rate = ba.Booking.Rate,
                    TypeofStay = ba.Booking.TypeofStay,
                    Customer = new CustomerModel
                    {
                        Id = ba.Booking.Customer.Id,
                        LastName = ba.Booking.Customer.LastName,
                        FirstName = ba.Booking.Customer.FirstName,
                        City = ba.Booking.Customer.City,
                        Province = ba.Booking.Customer.Province,
                        Email = ba.Booking.Customer.Email,
                        Telephone = ba.Booking.Customer.Telephone,
                        MobilePhone = ba.Booking.Customer.MobilePhone,
                        FiscalCode = ba.Booking.Customer.FiscalCode
                    },
                    Room = new RoomModel {
                        Id = ba.Booking.Room.Id,
                        NumberRoom = ba.Booking.Room.NumberRoom,
                        TypeofRoom = ba.Booking.Room.TypeofRoom,
                    }
                },
                DateRequestOfService = ba.DateRequestOfService,
                Quantity = ba.Quantity,
            });
            return View(bookingServicesModel);
        }

        public IActionResult Create()
        {
            return View(new BookingServicesModel());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id,BookingServicesModel bookingServices)
        { 
                _bookingSvcService.Create(new BookingServiceDto
                {
                    Id = bookingServices.Id,
                    AdditionalService = new AdditionalServicesEntity
                    {
                        Id = bookingServices.AdditionalService.Id,
                    },
                    Booking = new BookingDto
                    {
                        Id = bookingServices.Booking.Id,
                    },
                    DateRequestOfService = bookingServices.DateRequestOfService,
                    Quantity = bookingServices.Quantity,
                });
                return RedirectToAction("AllBookingServices", "BookingServices");
           
           
        }

        public IActionResult Update(int id)
        {
            var bookingSvc = _bookingSvcService.Read(id);
            var bookingSvcModel = new BookingServicesModel
            {
                Id = bookingSvc.Id,
                AdditionalService = new AdditionalServiceModel
                {
                    Id = bookingSvc.AdditionalService.Id,
                    TypeOfService = bookingSvc.AdditionalService.TypeOfService,
                    Price = bookingSvc.AdditionalService.Price,
                },
                Booking = new BookingModel
                {
                    Id = bookingSvc.Booking.Id,
                    DateBooking = bookingSvc.Booking.DateBooking,
                    YearProg = bookingSvc.Booking.YearProg,
                    DateStart = bookingSvc.Booking.DateStart,
                    DateEnd = bookingSvc.Booking.DateEnd,
                    Deposit = bookingSvc.Booking.Deposit,
                    Rate = bookingSvc.Booking.Rate,
                    TypeofStay = bookingSvc.Booking.TypeofStay,
                    Customer = new CustomerModel
                    {
                        Id = bookingSvc.Booking.Customer.Id,
                        LastName = bookingSvc.Booking.Customer.LastName,
                        FirstName = bookingSvc.Booking.Customer.FirstName,
                        City = bookingSvc.Booking.Customer.City,
                        Province = bookingSvc.Booking.Customer.Province,
                        Email = bookingSvc.Booking.Customer.Email,
                        Telephone = bookingSvc.Booking.Customer.Telephone,
                        MobilePhone = bookingSvc.Booking.Customer.MobilePhone,
                        FiscalCode = bookingSvc.Booking.Customer.FiscalCode
                    },
                    Room = new RoomModel
                    {
                        Id = bookingSvc.Booking.Room.Id,
                        NumberRoom = bookingSvc.Booking.Room.NumberRoom,
                        TypeofRoom = bookingSvc.Booking.Room.TypeofRoom,
                    }
                },
                DateRequestOfService = bookingSvc.DateRequestOfService,
                Quantity = bookingSvc.Quantity,
            };
            return View(bookingSvcModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, BookingServicesModel bookingServices)
        {
            var b = _bookingSvcService.Update(id, new BookingServiceDto
            {
                Id = bookingServices.Id,
                AdditionalService = new AdditionalServicesEntity
                {
                    Id = bookingServices.AdditionalService.Id,
                },
                Booking = new BookingDto
                {
                    Id = bookingServices.Booking.Id,
                },
                DateRequestOfService = bookingServices.DateRequestOfService,
                Quantity = bookingServices.Quantity,
            });
            return RedirectToAction("AllBookingServices", "BookingServices");
        }

      public IActionResult Delete(int id)
        {
                _bookingSvcService.Delete(id);
            return RedirectToAction("AllBookingServices", "BookingServices");
        }
    }

   
}
