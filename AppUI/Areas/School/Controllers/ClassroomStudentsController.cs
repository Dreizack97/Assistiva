using Microsoft.AspNetCore.Mvc;

namespace AppUI.Areas.School.Controllers
{
    public class ClassroomStudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert()
        {
            return View();
        }
    }
}