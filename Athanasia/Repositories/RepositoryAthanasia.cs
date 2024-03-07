using Athanasia.Data;
using Athanasia.Extension;
using Athanasia.Helpers;
using Athanasia.Models.Views;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

#region VIEWS
//alter view V_PRODUCTO as
//select
//	  ISNULL(p.ID_PRODUCTO, - 1) ID_PRODUCTO,
//    l.TITULO,
//    l.SINOPSIS,
//    l.FECHA_PUBLICACION,
//    l.PORTADA PORTADA,
//    c.NOMBRE CATEGORIA,
//    a.NOMBRE AUTOR,
//    STRING_AGG(g.NOMBRE, ', ') GENEROS,
//    s.NOMBRE SAGA,
//    p.ISBN ISBN,
//    f.NOMBRE FORMATO,
//    p.PRECIO PRECIO,
//    e.NOMBRE EDITORIAL,
//    e.LOGO LOGO
//from LIBRO l 
//left join CATEGORIA c on l.ID_CATEGORIA = c.ID_CATEGORIA 
//left join AUTOR a on l.ID_AUTOR = a.ID_AUTOR 
//left join SAGA s on s.ID_SAGA = l.ID_SAGA 
//left join GENEROS_LIBROS gl on gl.ID_LIBRO = l.ID_LIBRO 
//left join GENERO g on g.ID_GENERO = gl.ID_GENERO 
//inner join PRODUCTO p on l.ID_LIBRO = p.ID_LIBRO 
//left join EDITORIAL e on e.ID_EDITORIAL = p.ID_EDITORIAL
//inner join FORMATO f on f.ID_FORMATO=p.ID_FORMATO
//group by p.ID_PRODUCTO, l.TITULO, c.NOMBRE, a.NOMBRE, 
//s.NOMBRE, l.SINOPSIS, l.FECHA_PUBLICACION, l.PORTADA, 
//p.ISBN, p.PRECIO, e.NOMBRE, e.LOGO, f.NOMBRE


//alter view V_PRODUCTO_SIMPLE as
//select
//    ISNULL(p.ID_PRODUCTO, -1) ID_PRODUCTO,
//    l.TITULO,
//    l.PORTADA,
//    a.NOMBRE AUTOR,
//    p.PRECIO,
//    p.ID_FORMATO
//from Libro l
//left join AUTOR a on l.ID_AUTOR=a.ID_AUTOR
//inner join PRODUCTO p on l.ID_LIBRO=p.ID_LIBRO


#endregion
namespace Athanasia.Repositories
{
    public class RepositoryAthanasia
    {
        private AthanasiaContext context;

        public RepositoryAthanasia(AthanasiaContext context)
        {
            this.context = context;
        }
        #region PRODUCTO_VIEW
        public async Task<List<ProductoView>> GetProductosViewAsync()
        {
            List<ProductoView> productos = await this.context.ProductosView.ToListAsync();
            return productos;
        }

        public async Task<List<ProductoView>> GetProductosViewByFormatoAsync(string formato)
        {
            List<ProductoView> productos = await this.context.ProductosView.Where(o => o.Formato == formato).ToListAsync();
            return productos;
        }

        public async Task<ProductoView> FindProductoAsync(int idproducto)
        {
            ProductoView producto = await this.context.ProductosView.FirstOrDefaultAsync(p => p.IdProducto == idproducto);
            return producto;
        }
        #endregion

        #region PRODUCTO_SIMPLE_VIEW

        public async Task<List<ProductoSimpleView>> GetProductoSimpleViewAsync()
        {
            List<ProductoSimpleView> productosSimples = await this.context.ProductosSimplesView.ToListAsync();
            return productosSimples;
        }

        public async Task<List<ProductoSimpleView>> GetAllProductoSimpleViewByIdFormatoAsync(int idFormato)
        {
            List<ProductoSimpleView> productosSimples = await this.context.ProductosSimplesView.Where(o => o.IdFormato == idFormato).ToListAsync();
            return productosSimples;
        }

        public async Task<List<ProductoSimpleView>> SearchProductosSimplesViewAsync(string palabra, int idformato)
        {
            string limpio = palabra.Limpiar();
            var consulta = from datos in this.context.ProductosSimplesView
                           where (datos.Titulo.Contains(limpio) || datos.Autor.Limpiar().Contains(limpio))
                           && datos.IdFormato == idformato
                           select datos;
            return await consulta.ToListAsync();
        }

        #endregion

        #region
        #endregion
    }
}
