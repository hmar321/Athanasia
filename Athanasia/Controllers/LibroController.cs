using Athanasia.Extension;
using Athanasia.Helpers;
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
            int idFormato = HelperFormatos.GetFormatoId(Formatos.TapaDura);
            List <ProductoSimpleView> libros =await this.repo.GetAllProductoSimpleViewByIdFormatoAsync(idFormato);
            return View(libros);
        }
        [HttpPost]
        public async Task<ActionResult> Index(string buscador)
        {
            int idFormato = HelperFormatos.GetFormatoId(Formatos.TapaDura);
            List<ProductoSimpleView> libros = await this.repo.GetAllProductoSimpleViewByIdFormatoAsync(idFormato);
            return View(libros);
        }

        public async Task<ActionResult> Detalles(int idproducto)
        {
            ProductoView libro = await this.repo.FindProductoAsync(idproducto);
            ViewData["GENEROS"]=libro.Generos.Split(", ").ToList();
            return View(libro);
        }
    }
}
