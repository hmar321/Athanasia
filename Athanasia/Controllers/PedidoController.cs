using Athanasia.Extension;
using Athanasia.Filters;
using Athanasia.Models.Tables;
using Athanasia.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

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

        [AuthorizeUsuarios]
        public async Task<IActionResult> Comprar()
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //InformacionCompra infoUsuario=await this.repo
            return View();

        }

        //public IActionResult RealizarPedido()
        //{
        //    return View();
        //}
    }
}
