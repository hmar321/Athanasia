using Athanasia.Extension;
using Athanasia.Models.Views;
using Athanasia.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Athanasia.Controllers
{
    public class LibroController : Controller
    {
        private RepositoryAthanasia repo;

        public LibroController(RepositoryAthanasia repo)
        {
            this.repo = repo;
        }

        public async Task<ActionResult> Index()
        {
            List<ProductoView> libros =await this.repo.GetProductosViewAsync();
            return View(libros);
        }
        [HttpPost]
        public async Task<ActionResult> Index(string buscador)
        {
            List<ProductoView> libros = await this.repo.GetProductosViewAsync();
            return View(libros);
        }

        public async Task<ActionResult> Detalles(int idproducto)
        {
            ProductoView libro = await this.repo.FindProductoAsync(idproducto);
            return View(libro);
        }
    }
}
