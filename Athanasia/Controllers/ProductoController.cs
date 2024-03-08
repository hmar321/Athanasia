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
            List<int> idsproductos = this.memoryCache.Get<List<int>>("CARRITO");
            List<ProductoSimpleView> productos = await this.repo.FindProductosSimplesViewByIds(idsproductos);
            return View(productos);
        }
    }
}
