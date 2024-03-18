using Athanasia.Extension;
using Athanasia.Filters;
using Athanasia.Models.Tables;
using Athanasia.Models.Views;
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
            List<InformacionCompraView> infoUsuario = await this.repo.GetAllInformacionCompraViewByIdUsuarioAsync(idusuario);
            if (infoUsuario == null)
            {
                ViewData["MENSAJE"] = "No tienes métodos de pago";
                return View("MetodosPago");
            }
            ViewData["INFOCOMPRAUSUARIO"] = infoUsuario;
            return View();

        }
        [AuthorizeUsuarios]
        [HttpPost]
        public async Task<IActionResult> Comprar(int idinfocompra)
        {
            //AQUI ME HE QUEDAO
            List<ProductoSimpleView> productos = this.memoryCache.Get<List<ProductoSimpleView>>("CARRITO");
            Pedido pedido = await this.repo.InsertPedidoAsync();
            return View();
        }
    }
}
