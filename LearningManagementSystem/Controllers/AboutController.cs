using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAboutUs()
        {
            return View();
        }
    }
}
