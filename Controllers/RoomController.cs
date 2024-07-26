using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;

using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
namespace Progetto_Settimanale_Vescio_Pia_Francesca.Controllers
{
    [Authorize(Policy = Policies.IsAdmin)]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomSvc;

        public RoomController(IRoomService roomService)
        {
            _roomSvc = roomService;
        }
        public IActionResult AllRooms()
        {
            var rooms = _roomSvc.GetAll();
            return View(rooms);
        }
        public IActionResult RegisterRoom()
        {
            return View(new RoomModel());
        }
        [HttpPost]
        public IActionResult RegisterRoom(RoomModel room)
        {
            if (ModelState.IsValid)
            {
                _roomSvc.RegisterRoom(room);
                return RedirectToAction("AllRooms", "Room");
            }
            else
            {
                return View(room);
            }
        }

        public IActionResult UpdateRoom(int id)
        {
            var r = _roomSvc.GetRoom(id);
            return View(r);
        }

        [HttpPost]
        public IActionResult UpdateRoom(int id, RoomModel model)
        {
            if (ModelState.IsValid)
            {
                _roomSvc.UpdateRoom(id, model);
                return RedirectToAction("AllRooms", "Room");
            }
            else
            {
                return View(model);

            }
        }

        
        public IActionResult DeleteRoom(int id)
        {
            _roomSvc.DeleteRoom(id);
            return RedirectToAction("AllRooms", "Room");
        }
    }
}
