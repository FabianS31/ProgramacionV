using Microsoft.AspNetCore.Mvc;

namespace ProgramacionV.Controllers
{
    public class CalendarioController : Controller
    {
        public IActionResult Index()
        {
            return View("Index_calendario");
        }
    }
}