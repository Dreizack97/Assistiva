using Microsoft.AspNetCore.Mvc;

namespace AppUI.Areas.Students.Controllers
{
    public class SubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Subject()
        {
            return View();
        }
    }
}
