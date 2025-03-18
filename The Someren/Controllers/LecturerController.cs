using Microsoft.AspNetCore.Mvc;
using The_Someren.Models;
using The_Someren.Respository;

namespace The_Someren.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ILecturerRepository lecturerRepository;

        public LecturerController(ILecturerRepository lecturerRepository)
        {
            this.lecturerRepository = lecturerRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var lecturs = lecturerRepository.GetAll();
            return View(lecturs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create (Lecturer lecturer)
        {
            lecturerRepository.Add(lecturer);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var lecture = lecturerRepository.GetById(id);
            return View(lecture);
        }
        [HttpPost]
        public IActionResult Update(Lecturer lecturer)
        {
            lecturerRepository.Update(lecturer);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var lecture = lecturerRepository.GetById(id);
            return View(lecture);
        }
        [HttpPost]
        public IActionResult Delete(Lecturer lecturer)
        {
            lecturerRepository.Delete(lecturer);
            return RedirectToAction("Index");
        }
    }
}
