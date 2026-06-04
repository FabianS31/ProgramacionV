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
            return View("Index_login");
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
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                ViewBag.Error = "Debe ingresar un nombre de usuario";
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                ViewBag.Error = "Debe ingresar un correo electrónico";
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                ViewBag.Error = "Debe ingresar una contraseña";
                return View(model);
            }

            if (model.Password.Length < 6)
            {
                ViewBag.Error = "La contraseña debe tener al menos 6 caracteres";
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View(model);
            }

            try
            {
                string connectionString =
                    _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //==================================================
                    // VERIFICAR SI EL USUARIO YA EXISTE
                    //==================================================

                    string checkQuery = @"
                SELECT COUNT(*)
                FROM Users
                WHERE Username = @Username";

                    using (SqlCommand checkCmd =
                           new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue(
                            "@Username",
                            model.Username);

                        int existe =
                            (int)checkCmd.ExecuteScalar();

                        if (existe > 0)
                        {
                            ViewBag.Error =
                                "El usuario ya existe";

                            return View(model);
                        }
                    }

                    //==================================================
                    // INSERTAR NUEVO USUARIO
                    //==================================================

                    string insertQuery = @"
                INSERT INTO Users
                (
                    Username,
                    Email,
                    Password
                )
                VALUES
                (
                    @Username,
                    @Email,
                    @Password
                )";

                    using (SqlCommand insertCmd =
                           new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue(
                            "@Username",
                            model.Username);

                        insertCmd.Parameters.AddWithValue(
                            "@Email",
                            model.Email);

                        insertCmd.Parameters.AddWithValue(
                            "@Password",
                            model.Password);

                        insertCmd.ExecuteNonQuery();
                    }
                }

                TempData["Success"] =
                    "Usuario creado correctamente";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Error =
                    "Error al acceder a la base de datos";

                return View(model);
            }
        }
    }
}