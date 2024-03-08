using Athanasia.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Athanasia.Controllers
{
    public class UsuarioController : Controller
    {
        private RepositoryAthanasia repo;
        private IMemoryCache memoryCache;

        public UsuarioController(RepositoryAthanasia repo, IMemoryCache memoryCache)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
        }
        public IActionResult Terminos()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registro(string nombre,string apellido,string email,string password,string password2,bool terminos)
        {

            return View();
        }
    }
}
