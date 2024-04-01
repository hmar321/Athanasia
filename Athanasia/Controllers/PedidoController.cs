using Athanasia.Extension;
using Athanasia.Filters;
using Athanasia.Models.Tables;
using Athanasia.Models.Views;
using Athanasia.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Security.Claims;

namespace Athanasia.Controllers
{
    public class PedidoController : Controller
    {
        private RepositoryAthanasia repo;
        private IMemoryCache memoryCache;
        private IMapper mapper;

        public PedidoController(RepositoryAthanasia repo, IMemoryCache memoryCache,IMapper mapper)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
            this.mapper = mapper;
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
            List<ProductoSimpleView> productosView = this.memoryCache.Get<List<ProductoSimpleView>>("CARRITO");
            if (productosView != null)
            {
                Pedido pedido = await this.repo.InsertPedidoAsync(idusuario);
                List<PedidoProducto> productos=this.mapper.Map<List<PedidoProducto>>(productosView);
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
