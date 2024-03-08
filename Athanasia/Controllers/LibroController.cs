using Athanasia.Extension;
using Athanasia.Helpers;
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

        public async Task<ActionResult> Index(int? idproducto)
        {
            List<int> idsproducto = this.memoryCache.Get<List<int>>("CARRITO");
            if (idproducto != null)
            {
                if (idsproducto == null)
                {
                    idsproducto = new List<int>();
                }
                if (idsproducto.Any(id => id == idproducto.Value)==false)
                {
                    idsproducto.Add(idproducto.Value);
                    this.memoryCache.Set("CARRITO", idsproducto);
                }
            }
            int idformato = HelperFormatos.GetFormatoId(Formatos.TapaBlanda);
            List<ProductoSimpleView> libros = await this.repo.GetlProductoSimplesViewAsync(idformato);
            return View(libros);
        }
        [HttpPost]
        public async Task<ActionResult> Index(string buscador)
        {
            int idformato = HelperFormatos.GetFormatoId(Formatos.TapaBlanda);
            List<ProductoSimpleView> libros = await this.repo.FindProductosSimplesViewAsync(buscador, idformato);
            return View(libros);
        }

        public async Task<ActionResult> Detalles(int idproducto)
        {
            ProductoView libro = await this.repo.FindProductoAsync(idproducto);
            ViewData["GENEROS"] = libro.Generos.Split(", ").ToList();
            return View(libro);
        }
    }
}
