using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskService _taskService;

        public HomeController(ILogger<HomeController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Details(int Id)
        {
            var taskdetails=_taskService.GetById(Id);
            if (taskdetails == null) 
            {
            return NotFound();
            }
            return PartialView(taskdetails);
        }
        [HttpPost]
        public IActionResult GetAll(string searchTerm)
        {
            var tasks=_taskService.GetAllTask(searchTerm);
            return PartialView(tasks);
        }
        [HttpPost]
        public IActionResult Edit(int Id = 0)
        {
            ViewBag.CurrentDate= DateTime.Now.ToString("yyyy-MM-dd");
            var task = Id == 0 ? new TaskModel():_taskService.GetById(Id);
            return PartialView(task);
        }
        [HttpPost]
        public IActionResult Save(TaskModel task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskService.AddOrUpdate(task);
                    return RedirectToAction("Index");
                }
                return Json("Inavalid response");
            }
            catch (Exception ex)
            {
                return Json($"Error:{ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult DeleteTask(int ID)
        {
            _taskService.Delete(ID);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
