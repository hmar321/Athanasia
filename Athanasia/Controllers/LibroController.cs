using Athanasia.Models.Tables;
using Athanasia.Models.Util;
using Athanasia.Models.Views;
using Athanasia.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Athanasia.Controllers
{
    public class LibroController : Controller
    {
        private RepositoryAthanasia repo;
        private IMemoryCache memoryCache;

        public LibroController(RepositoryAthanasia repo, IMemoryCache memoryCache)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
        }

        public async Task<ActionResult> Index(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int ndatos = 4;
            PaginacionModel<ProductoSimpleView> model = await this.repo.GetProductosSimplesPaginacionAsyn(posicion.Value, ndatos);
            ViewData["POSICION"] = posicion.Value;
            ViewData["PAGINAS"] = model.NumeroPaginas;
            return View(model.Lista);
        }

        public async Task<ActionResult> Detalles(int idproducto)
        {
            ProductoView libro = await this.repo.GetProductoByIdAsync(idproducto);
            List<FormatoLibroView> formatos = await this.repo.GetAllFormatoLibroViewByIdLibroAsync(libro.IdLibro);
            ViewData["GENEROS"] = libro.Generos.Split(", ").ToList();
            ViewData["FORMATOS"] = formatos;
            return View(libro);
        }

        public async Task<IActionResult> _IconoCarrito(int idproducto, bool agregar)
        {
            List<ProductoSimpleView> productos = this.memoryCache.Get<List<ProductoSimpleView>>("CARRITO");
            if (agregar == true)
            {
                if (productos == null)
                {
                    productos = new List<ProductoSimpleView>();
                }
                if (productos.Any(id => id.IdProducto == idproducto) == false)
                {
                    ProductoSimpleView producto = await this.repo.GetProductoSimpleByIdAsync(idproducto);
                    productos.Add(producto);
                    this.memoryCache.Set("CARRITO", productos);
                }
            }
            ViewData["IDPRODUCTO"] = idproducto;
            return PartialView("_IconoCarrito");
        }

    }
}
