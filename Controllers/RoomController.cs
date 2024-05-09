using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StanfordHospital.Data;
using StanfordHospital.Models;

namespace StanfordHospital.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly ILogger<RoomController> _logger;
        private readonly ApplicationDbContext _context;

        public RoomController(ILogger<RoomController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult RoomDetails()
        //{
        //    ViewBag.isroom = "active";
        //    return View();
        //}

        public IActionResult Room(List<Room> rooms)
        {
            ViewBag.isroom = "active";

            var model = new List<Room>();
            model = _context.Room.Select(r => new Room
            {
                Roomid = r.Roomid,
                RoomName = r.RoomName,
                RoomFloor = r.RoomFloor,
                RoomType = r.RoomType,
                RoomPrice = r.RoomPrice,
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddRoom(Room room)
        {
            ViewBag.isroom = "active";
            if (ModelState.IsValid)
            {
                var Room = new Room
                {
                   Roomid =room.Roomid,
                   RoomName = room.RoomName,
                   RoomFloor = room.RoomFloor,
                   RoomType = room.RoomType,
                   RoomPrice = room.RoomPrice,
                };

                if (room.Roomid != 0)
                {
                    //Edit
                    var rooms = _context.Room.Find(room.Roomid);
                    if (Room != null)
                    {
                        room.Roomid = room.Roomid;
                        room.RoomName = room.RoomName;
                        room.RoomFloor = room.RoomFloor;
                        room.RoomType = room.RoomType;
                        room.RoomPrice = room.RoomPrice;
                        _context.SaveChanges();
                        TempData["Message"] = "Room Updated Successfully....";
                    }
                }
                else
                {
                    //Create
                    _context.Room.Add(Room);
                    _context.SaveChanges();
                    TempData["Message"] = "Room Added Successfully....";
                }
                return RedirectToAction("Room");
            }

            return View("AddRoom", room);
        }

        public IActionResult EditRoom(Room room)
        {
            ViewBag.isroom = "active";
            if (ModelState.IsValid)
            {
                var Room = _context.Room.Find(room.Roomid);
                if (Room != null)
                {
                    Room.Roomid = room.Roomid;
                    Room.RoomName = room.RoomName;
                    Room.RoomFloor = room.RoomFloor;
                    Room.RoomType = room.RoomType;
                    Room.RoomPrice = room.RoomPrice;
                    _context.Room.Update(Room);
                    _context.SaveChanges();
                    TempData["Message"] = "Room Updated Successfully....";
                    return RedirectToAction("Room");
                }
                else
                {
                    return NotFound();
                }
            }
            return View("EditRoom", room);
        }

        public IActionResult DeleteRoom(int Roomid)
        {
            var room = _context.Room.Find(Roomid);
            if (room != null)
            {
                _context.Room.Remove(room);
                _context.SaveChanges();

                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

        public IActionResult Create(Room room)
        {
            ViewBag.isroom = "active";
            return View("AddRoom", room);
        }

        public IActionResult Edit(int Id)
        {
            ViewBag.isroom = "active";
            var updateroom = _context.Room.Where(r => r.Roomid == Id)
                .Select(r => new Room
                {
                    Roomid = r.Roomid,
                    RoomName = r.RoomName,
                    RoomFloor = r.RoomFloor,
                    RoomType = r.RoomType,
                    RoomPrice = r.RoomPrice,
                }).FirstOrDefault();

            return View("EditRoom", updateroom);
        }

        public IActionResult Delete(Room room)
        {
            var removeroom = _context.Room.Where(r => r.Roomid == room.Roomid)
                .Select(r => new Room
                {
                    Roomid=r.Roomid,
                    RoomName=r.RoomName,
                    RoomFloor=r.RoomFloor,
                    RoomType = r.RoomType,
                    RoomPrice = r.RoomPrice,
                }).FirstOrDefault();

            return View("DeleteRoom", removeroom);
        }
    }
}
