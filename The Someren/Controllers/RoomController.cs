using Microsoft.AspNetCore.Mvc;
using The_Someren.Models;
using The_Someren.Respository;

namespace The_Someren.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository repository;

        public RoomController(IRoomRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var rooms = repository.GetAll();
            return View(rooms);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Room room)
        {
            repository.Add(room);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var room = repository.GetById(id);
            return View(room);
        }

        [HttpPost]
        public IActionResult Update(Room room)
        {
            repository.Update(room);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var room = repository.GetById(id);
            return View(room);
        }

        [HttpPost]
        public IActionResult Delete(Room room)
        {
            repository.Delete(room);
            return RedirectToAction("Index");
        }
    }
}
