using Microsoft.AspNetCore.Mvc;
using TP6MVC.Models;
using TP6MVC.Repositories;

namespace TP6MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public LoginController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _usuariosRepository.ObtenerPorUsuario(model.Usuario);
                if (usuario != null && usuario.Contraseña == model.Contraseña)
                {
                    HttpContext.Session.SetString("Usuario", usuario.UsuarioNombre);
                    HttpContext.Session.SetString("Rol", usuario.Rol);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            }
            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
