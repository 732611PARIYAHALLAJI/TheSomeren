using Microsoft.AspNetCore.Mvc;
using The_Someren.Models;
using The_Someren.Respository;

namespace The_Someren.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository repository;

        public StudentController(IStudentRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var students = repository.GetAll();
            return View(students);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            repository.Add(student);
            return RedirectToAction("Index");
        }
        [HttpGet]

        public IActionResult Update( int id)
        {
            var student = repository.GetById(id);
            return View(student);
        }
        [HttpPost]

        public IActionResult Update(Student student)
        {
            repository.Update(student);
            return RedirectToAction("Index");
        }
        [HttpGet]

        public IActionResult Delete(int id)
        {
            var student = repository.GetById(id);
            return View(student);
        }
        [HttpPost]

        public IActionResult Delete(Student student)
        {
            repository.Delete(student);
            return RedirectToAction("Index");
        }
    }
}
