using Athanasia.Extension;
using Athanasia.Helpers;
using Athanasia.Models.Tables;
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

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public async Task<ActionResult> Index()
        {
            List<ProductoSimpleView> libros = await this.repo.GetProductoSimplesViewAsync();
            return View(libros);
        }
        [HttpPost]
        public async Task<ActionResult> Index(string buscador)
        {
            List<ProductoSimpleView> libros = await this.repo.FindProductosSimplesViewAsync(buscador);
            return View(libros);
        }

        public async Task<ActionResult> Detalles(int idproducto)
        {
            ProductoView libro = await this.repo.FindProductoAsync(idproducto);
            ViewData["GENEROS"] = libro.Generos.Split(", ").ToList();
            return View(libro);
        }

        public IActionResult _IconoCarrito(int idproducto, bool agregar)
        {
            List<int> idsproducto = this.memoryCache.Get<List<int>>("CARRITO");
            if (agregar == true)
            {
                if (idsproducto == null)
                {
                    idsproducto = new List<int>();
                }
                if (idsproducto.Any(id => id == idproducto) == false)
                {
                    idsproducto.Add(idproducto);
                    this.memoryCache.Set("CARRITO", idsproducto);
                }
            }
            ViewData["IDPRODUCTO"] = idproducto;
            return PartialView("_IconoCarrito");
        }

    }
}
