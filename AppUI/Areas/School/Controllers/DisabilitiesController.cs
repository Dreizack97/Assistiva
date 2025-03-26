using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppUI.Areas.School.Controllers
{
    [Authorize]
    [Area("School")]
    public class DisabilitiesController : Controller
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
