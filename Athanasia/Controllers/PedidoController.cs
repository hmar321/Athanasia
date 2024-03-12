using Athanasia.Extension;
using Athanasia.Models.Tables;
using Athanasia.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Athanasia.Controllers
{
    public class PedidoController : Controller
    {
        private RepositoryAthanasia repo;
        private IMemoryCache memoryCache;

        public PedidoController(RepositoryAthanasia repo, IMemoryCache memoryCache)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
        }

        public IActionResult RealizarPedido()
        {
            Usuario usuario = HttpContext.Session.GetObject<Usuario>("USUARIO");
            if (usuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            return View();

        }

        //public IActionResult RealizarPedido()
        //{
        //    return View();
        //}
    }
}
