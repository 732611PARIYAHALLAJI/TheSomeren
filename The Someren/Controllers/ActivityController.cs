using Microsoft.AspNetCore.Mvc;
using The_Someren.Respository;
using Activity = The_Someren.Models.Activity;

namespace The_Someren.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityRepository repository;

        public ActivityController(IActivityRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var activities = repository.GetAll();
            return View(activities);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Activity activity)
        {
            repository.Add(activity);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var activity = repository.GetById(id);
            return View(activity);
        }
        [HttpPost]
        public IActionResult Update(Activity activity)
        {
            repository.Update(activity);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var activity = repository.GetById(id);
            return View(activity);
        }
        [HttpPost]
        public IActionResult Delete(Activity activity)
        {
            repository.Delete(activity);
            return RedirectToAction("Index");
        }
    }
}
