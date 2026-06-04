using Microsoft.AspNetCore.Mvc;
using ProgramacionV.Models;

namespace ProgramacionV.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBController _db;

        public HomeController(DBController db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.TotalAlumnos =
                _db.Count("Alumnos");

            ViewBag.TotalProfesionales =
                _db.Count("Profesionales");

            ViewBag.TotalHistorias =
                _db.Count("Historia_Clinica");

            ViewBag.TotalUsuarios =
                _db.Count("Users");

            return View();
        }
    }
}