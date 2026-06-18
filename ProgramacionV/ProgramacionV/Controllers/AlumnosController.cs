using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgramacionV.Models;

namespace ProgramacionV.Controllers
{
    public class AlumnosController : Controller
    {
        // GET: AlumnosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AlumnosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AlumnosController/Create
        public ActionResult Create()
        {
            return View("~/Views/Pacientes/AltaPaciente.cshtml");
        }

        // POST: AlumnosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlumnoViewModel alumno)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("AltaPaciente", alumno);
                }

                // Aquí iría tu lógica con DBController para guardar
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("AltaPaciente", alumno);
            }
        }

        // GET: AlumnosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AlumnosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AlumnosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AlumnosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}