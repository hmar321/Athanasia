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

        public IActionResult Login()
        {
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
                //ID_USUARIO,NOMBRE,APELLIDO,EMAIL,PASSWORD,IMAGEN
                //PASS,SALT,TOKEN,ID_ROL,ID_ESTADO
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim(ClaimTypes.Role, usuario.IdRol+"")
                };
                identity.AddClaims(claims);

                ClaimsPrincipal usePrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    usePrincipal);
                HttpContext.Session.SetObject("USUARIO", usuario);
                return RedirectToAction("Perfil", "Usuario");
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
            HttpContext.Session.Remove("USUARIO");
            return RedirectToAction("Index", "Libro");
        }
    }
}
