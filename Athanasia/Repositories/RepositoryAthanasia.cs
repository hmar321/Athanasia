using Athanasia.Data;
using Athanasia.Helpers;
using Athanasia.Models.Views;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

#region VIEWS
//alter view V_PRODUCTO as
//select
//	ISNULL(p.ID_PRODUCTO, - 1) ID_PRODUCTO,
//    ISNULL(l.TITULO, 'Sin título') TITULO,
//    ISNULL(l.SINOPSIS, 'Sin sinopsis') SINOPSIS,
//    ISNULL(l.FECHA_PUBLICACION, '1-01-01') FECHA_PUBLICACION,
//    ISNULL(l.PORTADA, 'libro.png') PORTADA,
//    ISNULL(c.NOMBRE, 'Sin genero') CATEGORIA,
//    ISNULL(a.NOMBRE, 'Autor desconocido') AUTOR,
//    STRING_AGG(g.NOMBRE, ', ') GENEROS,
//    ISNULL(s.NOMBRE, 'Sin saga') SAGA,
//    ISNULL(p.ISBN, 'Sin ISBN') ISBN,
//    ISNULL(f.NOMBRE, 'Formato sin nombre') FORMATO,
//    ISNULL(p.PRECIO, '1000000') PRECIO,
//    ISNULL(e.NOMBRE, 'Sin editorial') EDITORIAL,
//    ISNULL(e.LOGO, 'logo.png') LOGO
//from LIBRO l 
//	left join CATEGORIA c on l.ID_CATEGORIA = c.ID_CATEGORIA 
//	left join AUTOR a on l.ID_AUTOR = a.ID_AUTOR 
//	left join SAGA s on s.ID_SAGA = l.ID_SAGA 
//	left join GENEROS_LIBROS gl on gl.ID_LIBRO = l.ID_LIBRO 
//	left join GENERO g on g.ID_GENERO = gl.ID_GENERO 
//	inner join PRODUCTO p on l.ID_LIBRO = p.ID_LIBRO 
//	left join EDITORIAL e on e.ID_EDITORIAL = p.ID_EDITORIAL
//	inner join FORMATO f on f.ID_FORMATO=p.ID_FORMATO
//group by p.ID_PRODUCTO, l.TITULO, c.NOMBRE, a.NOMBRE, s.NOMBRE, l.SINOPSIS, l.FECHA_PUBLICACION, l.PORTADA, p.ISBN, p.PRECIO, e.NOMBRE, e.LOGO, f.NOMBRE

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
            return await this.context.ProductosView.ToListAsync();
        }

        public async Task<List<ProductoView>> GetProductosViewByFormatoAsync(Formatos formato)
        {
            string dato = HelperFormatos.GetFormato(formato);
            return await this.context.ProductosView.Where(o => o.Formato == dato).ToListAsync();
        }

        public async Task<ProductoView> FindProductoAsync(int idproducto)
        {
            return await this.context.ProductosView.FirstOrDefaultAsync(p => p.IdProducto == idproducto);
        }
        #endregion
    }
}
