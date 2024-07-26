using Microsoft.AspNetCore.Mvc;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Microsoft.AspNetCore.Authorization;
namespace Progetto_Settimanale_Vescio_Pia_Francesca.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public IActionResult AllBooking()
        {
            var booking = _bookingService.GetAll();
            var bookingModels = booking.Select(b => new BookingModel
            {
                Id = b.Id,
                DateBooking = b.DateBooking,
                YearProg = b.YearProg,
                DateStart = b.DateStart,
                DateEnd = b.DateEnd,
                Deposit = b.Deposit,
                Rate = b.Rate,
                TypeofStay = b.TypeofStay,
                Customer = new CustomerModel
                {
                    Id = b.Customer.Id,
                    LastName = b.Customer.LastName,
                    FirstName = b.Customer.FirstName,
                    City = b.Customer.City,
                    Province = b.Customer.Province,
                    Email = b.Customer.Email,
                    Telephone = b.Customer.Telephone,
                    MobilePhone = b.Customer.MobilePhone,
                    FiscalCode = b.Customer.FiscalCode
                },
                Room = new RoomModel
                {
                    Id = b.Room.Id,
                    NumberRoom = b.Room.NumberRoom,
                    TypeofRoom = b.Room.TypeofRoom,
                    
                }
            }).ToList();
            return View(bookingModels);
        }
        
        public IActionResult RegisterBooking()
        {
            return View(new BookingModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterBooking(BookingModel booking) 
        { 
                _bookingService.Create(
                    new BookingDto
                    {
                        Id = booking.Id,
                        DateBooking = booking.DateBooking,
                        YearProg = booking.YearProg,
                        DateStart = booking.DateStart,
                        DateEnd = booking.DateEnd,
                        Deposit = booking.Deposit,
                        Rate = booking.Rate,
                        TypeofStay = booking.TypeofStay,
                        Customer = new CustomerEntity
                        {
                            Id = booking.Customer.Id,
                        },
                        Room = new RoomEntity
                        {
                            Id = booking.Room.Id,
                        }

                    });
                return RedirectToAction("AllBooking", "Booking");
        }

        public IActionResult Update(int id)
        {
            var booking = _bookingService.Read(id);
            var bookingModel = new BookingModel
            {

                Id = booking.Id,
                DateBooking = booking.DateBooking,
                YearProg = booking.YearProg,
                DateStart = booking.DateStart,
                DateEnd = booking.DateEnd,
                Deposit = booking.Deposit,
                Rate = booking.Rate,
                TypeofStay = booking.TypeofStay,
                Customer = new CustomerModel
                {
                    Id = booking.Customer.Id,
                },
                Room = new RoomModel
                {
                    Id = booking.Room.Id,
                }
            };
            return View(bookingModel); 
        }

        [HttpPost]
        public IActionResult Update(int id, BookingModel booking)
        {
            var b = _bookingService.Update(id, new BookingDto
            {
                Id = booking.Id,
                DateBooking = booking.DateBooking,
                YearProg = booking.YearProg,
                DateStart = booking.DateStart,
                DateEnd = booking.DateEnd,
                Deposit = booking.Deposit,
                Rate = booking.Rate,
                TypeofStay = booking.TypeofStay,
                Customer = new CustomerEntity
                {
                    Id = booking.Customer.Id,
                },
                Room = new RoomEntity
                {
                    Id = booking.Room.Id,
                }
            });
            return RedirectToAction("AllBooking", "Booking");
        }
        public IActionResult DeleteBooking(int id)
        {
            _bookingService.Delete(id);
            return RedirectToAction("AllBooking", "Booking");
        }
    }

}
