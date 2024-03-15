using Athanasia.Models.Views;
using Athanasia.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Athanasia.Controllers
{
    public class ProductoController : Controller
    {
        private RepositoryAthanasia repo;
        private IMemoryCache memoryCache;

        public ProductoController(RepositoryAthanasia repo, IMemoryCache memoryCache)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> Carrito()
        {
            return View();
        }

        public IActionResult _Precio(int idproducto, int? valor)
        {
            List<ProductoSimpleView> productos = memoryCache.Get<List<ProductoSimpleView>>("CARRITO");
            ProductoSimpleView producto = productos.FirstOrDefault(p => p.IdProducto == idproducto);
            if (valor != null)
            {
                producto.Unidades += valor.Value;
                if (producto.Unidades < 1)
                {
                    productos.Remove(producto);
                }
                if (productos.Count == 0)
                {
                    memoryCache.Remove("CARRITO");
                }
            }
            return PartialView("_Precio", producto);
        }

        public async Task<IActionResult> QuitarProductoCarrito(int idproducto)
        {
            List<ProductoSimpleView> productos = this.memoryCache.Get<List<ProductoSimpleView>>("CARRITO");
            ProductoSimpleView producto = productos.FirstOrDefault(prod => prod.IdProducto == idproducto);
            productos.Remove(producto);
            if (productos.Count == 0)
            {
                this.memoryCache.Remove("CARRITO");
            }
            else
            {
                this.memoryCache.Set("CARRITO", productos);
            }
            return RedirectToAction("Carrito");
        }
    }
}
