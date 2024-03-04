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
            List<LibroView> libros =await this.repo.GetLibrosViewAsync();
            return View(libros);
        }
        [HttpPost]
        public async Task<ActionResult> Index(string buscador)
        {
            List<LibroView> libros = await this.repo.GetLibrosViewAsync();
            ViewData["MENSAJE"] = buscador.Normalizar();
            return View(libros);
        }
    }
}
