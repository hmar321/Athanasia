using Athanasia.Extension;
using Athanasia.Filters;
using Athanasia.Helpers;
using Athanasia.Models.Tables;
using Athanasia.Models.Util;
using Athanasia.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static NuGet.Packaging.PackagingConstants;

namespace Athanasia.Controllers
{
    public class UsuarioController : Controller
    {
        private RepositoryAthanasia repo;
        private IMemoryCache memoryCache;
        private HelperMails helperMail;
        private HelperPathProvider helperPathProvider;

        public UsuarioController(RepositoryAthanasia repo, IMemoryCache memoryCache, HelperMails helperMail, HelperPathProvider helperPathProvider)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
            this.helperMail = helperMail;
            this.helperPathProvider = helperPathProvider;
        }

        public IActionResult Terminos()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registro(string nombre, string apellido, string email, string password, string password2, bool terminos)
        {
            UsuarioRegistro info = new UsuarioRegistro
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Password = password,
                Password2 = password2,
                Terminos = terminos
            };
            if (password != password2)
            {
                ViewData["REGISTROUSUARIO"] = info;
                ViewData["ERROR"] = "La contraseña no es la misma";
                return View();
            }
            Usuario usuario = await this.repo.RegistrarUsuarioAsync(nombre, apellido, email, password);
            string serverUrl = this.helperPathProvider.MapUrlServerPath();
            serverUrl = serverUrl + "/Usuario/Login?token=" + usuario.Token;
            string mensaje = "<h3>Usuario registrado<h3>";
            mensaje += "<p>Puede activar su cuenta pulsando el siguiente enlace:</p>";
            mensaje += "<a href='" + serverUrl + "'>" + serverUrl + "</a>";
            try
            {
                await this.helperMail.SendMailAsync(email, "Activación cuenta[NO REPLY]", mensaje);
                ViewData["MENSAJE"] = "Usuario registrado correctamente, activa tu cuenta desde tu correo";
            }
            catch (Exception)
            {
                ViewData["REGISTROUSUARIO"] = info;
                ViewData["ERROR"] = "Error al enviar correo";
                //await this.repo.DeleteUsuarioAsync(usuario.IdUsuario);
            }
            return View();
        }

        public IActionResult _Menus()
        {
            return PartialView("_Menus");
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> Perfil()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int idusuario)
        {
            Usuario usuario = await this.repo.FindUsuarioByIdAsync(idusuario);
            return View(usuario);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(int idusuario, string nombre, string apellido, string email, IFormFile? fichero)
        {
            string imagen = null;
            if (fichero != null)
            {
                string path = this.helperPathProvider.MapPath(fichero.FileName, FoldersImages.Usuarios);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await fichero.CopyToAsync(stream);
                }
                imagen = fichero.FileName;
            }
            Usuario usuario = await this.repo.UpdateUsuarioAsync(idusuario, nombre, apellido, email, imagen);
            return RedirectToAction("Perfil");
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> Compras()
        {

            return View();
        }
    }
}
