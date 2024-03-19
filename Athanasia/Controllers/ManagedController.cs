using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Athanasia.Repositories;
using Athanasia.Models.Tables;
using Athanasia.Extension;

namespace Athanasia.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryAthanasia repo;

        public ManagedController(RepositoryAthanasia repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Login(string? token)
        {
            if (token!=null)
            {
                await this.repo.ActivarUsuarioAsync(token);
                ViewData["MENSAJE"] = "Usuario activado";
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            Usuario usuario = await this.repo.LogInUserAsync(email, password);
            if (usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name,
                    ClaimTypes.Role);
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario+""),
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim(ClaimTypes.Surname, usuario.Apellido),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.IdRol+""),
                    new Claim(ClaimTypes.UserData, usuario.Imagen)
                };
                identity.AddClaims(claims);
                ClaimsPrincipal usePrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    usePrincipal);
                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                return RedirectToAction(action, controller);
            }
            else
            {
                ViewData["ERROR"] = "Credenciales incorrectas";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Libro");
        }
    }
}
