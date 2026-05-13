using ProgramacionV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ProgramacionV.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                   SELECT COUNT(*)
                   FROM Users
                   WHERE Username = @Username
                   AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", model.username);
                    cmd.Parameters.AddWithValue("@Password", model.password);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";

            return View(model);
        }
    }
}
