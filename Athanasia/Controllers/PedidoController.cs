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
            if (infoUsuario.Count == 0)
            {
                return RedirectToAction("MetodosPago","Usuario");
            }
            ViewData["INFOCOMPRAUSUARIO"] = infoUsuario;
            return View();

        }
        [AuthorizeUsuarios]
        [HttpPost]
        public async Task<IActionResult> Comprar(int idinfocompra)
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<ProductoSimpleView> productos = this.memoryCache.Get<List<ProductoSimpleView>>("CARRITO");
            if (productos != null)
            {
                Pedido pedido = await this.repo.InsertPedidoAsync(idusuario);
                await this.repo.InsertListPedidoProductosAsync(pedido.IdPedido, productos);
                this.memoryCache.Remove("CARRITO");
            }
            return RedirectToAction("HistorialCompras", "Usuario");
        }
        [AuthorizeUsuarios]
        public async Task<IActionResult> CancelarPedido(int idpedido)
        {
            await this.repo.UpdatePedidoEstadoCancelarAsync(idpedido);
            return RedirectToAction("HistorialCompras", "Usuario");
        }
        [AuthorizeUsuarios]
        public async Task<IActionResult> Detalles(int idpedido)
        {
            List<PedidoProductoView> prodPedido = await this.repo.GetPedidoProductoViewsByIdPedidoAsync(idpedido);
            return View(prodPedido);
        }
    }
}
