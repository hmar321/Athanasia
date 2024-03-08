using Athanasia.Data;
using Athanasia.Extension;
using Athanasia.Helpers;
using Athanasia.Models.Views;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

#region VIEWS
//alter view V_PRODUCTO as
//select
//	  ISNULL(p.ID_PRODUCTO, - 1) ID_PRODUCTO,
//    l.TITULO,
//    l.SINOPSIS,
//    l.FECHA_PUBLICACION,
//    l.PORTADA,
//    c.NOMBRE CATEGORIA,
//    a.NOMBRE AUTOR,
//    STRING_AGG(g.NOMBRE, ', ') GENEROS,
//    s.NOMBRE SAGA,
//    p.ISBN,
//    f.NOMBRE FORMATO,
//    p.PRECIO,
//    e.NOMBRE EDITORIAL,
//    e.LOGO LOGO
//from LIBRO l 
//left join CATEGORIA c on l.ID_CATEGORIA = c.ID_CATEGORIA and l.TITULO is not null
//left join AUTOR a on l.ID_AUTOR = a.ID_AUTOR and a.NOMBRE is not null
//left join SAGA s on s.ID_SAGA = l.ID_SAGA 
//left join GENEROS_LIBROS gl on gl.ID_LIBRO = l.ID_LIBRO 
//left join GENERO g on g.ID_GENERO = gl.ID_GENERO 
//inner join PRODUCTO p on l.ID_LIBRO = p.ID_LIBRO and p.ISBN is not null and p.PRECIO is not null
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
//left join AUTOR a on l.ID_AUTOR=a.ID_AUTOR and l.TITULO is not null and a.NOMBRE is not null
//inner join PRODUCTO p on l.ID_LIBRO=p.ID_LIBRO and p.PRECIO is not null


#endregion

#region FUNCTIONS

//create function LIMPIAR (@str nvarchar(MAX))
//returns nvarchar(MAX)
//as
//begin
//    set @str = REPLACE(@str, 'á', 'a')
//    set @str = REPLACE(@str, 'é', 'e')
//    set @str = REPLACE(@str, 'í', 'i')
//    set @str = REPLACE(@str, 'ó', 'o')
//    set @str = REPLACE(@str, 'ú', 'u')
//    set @str = REPLACE(@str, 'Á', 'A')
//    set @str = REPLACE(@str, 'É', 'E')
//    set @str = REPLACE(@str, 'Í', 'I')
//    set @str = REPLACE(@str, 'Ó', 'O')
//    set @str = REPLACE(@str, 'Ú', 'U')
//    set @str = REPLACE(@str, '&', '')
//    set @str = REPLACE(@str, '-', '')
//    set @str = REPLACE(@str, '_', '')
//    set @str = REPLACE(@str, '+', '')
//    set @str = REPLACE(@str, '"', '')
//    set @str = REPLACE(@str, '''', '')
//    set @str = REPLACE(@str, ',', '')
//    set @str = REPLACE(@str, '.', '')
//    set @str = REPLACE(@str, '?', '')
//    set @str = REPLACE(@str, '`', '')
//	set @str = REPLACE(@str, '!', '')
//    set @str = REPLACE(@str, '¡', '')
//    set @str = REPLACE(@str, '¿', '')
//    set @str = REPLACE(@str, 'ñ', 'n')
//    set @str = REPLACE(@str, 'Ñ', 'N')
//    set @str = REPLACE(@str, 'ü', 'u')
//    set @str = REPLACE(@str, 'Ü', 'U')
//	set @str = REPLACE(@str, ' ', '')
//	set @str = UPPER(@str)
//    return @str
//end

#endregion

#region PROCEDURES

//create procedure SP_SEARCH_PRODUCTOS
//(@busqueda nvarchar(255), @idformato int)
//as
//	select ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO from V_PRODUCTO_SIMPLE
//	where TITULO is not null
//	and AUTOR is not null
//	and dbo.LIMPIAR(TITULO) like @busqueda
//	and ID_FORMATO=@idformato
//go

#endregion


#region
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

        public async Task<List<ProductoSimpleView>> GetProductosSimplesViewAsync()
        {
            List<ProductoSimpleView> productosSimples = await this.context.ProductosSimplesView.ToListAsync();
            return productosSimples;
        }

        public async Task<List<ProductoSimpleView>> GetlProductoSimplesViewAsync(int idFormato)
        {
            List<ProductoSimpleView> productosSimples = await this.context.ProductosSimplesView.Where(o => o.IdFormato == idFormato).ToListAsync();
            return productosSimples;
        }

        public async Task<List<ProductoSimpleView>> FindProductosSimplesViewAsync(string palabra, int idformato)
        {
            string busqueda = palabra.Limpiar();
            string sql = "SP_SEARCH_PRODUCTOS @busqueda,@idformato";
            SqlParameter parambusqueda = new SqlParameter("@busqueda", "%" + busqueda + "%");
            SqlParameter paramidformato = new SqlParameter("@idformato", idformato);
            var consulta = this.context.ProductosSimplesView.FromSqlRaw(sql, parambusqueda, paramidformato);
            return await consulta.ToListAsync();
        }

        public async Task<List<ProductoSimpleView>> FindProductosSimplesViewByIds(List<int> idsproductos)
        {
            if (idsproductos == null || idsproductos.Count == 0)
            {
                return null;
            }
            else
            {
                var consulta = from datos in this.context.ProductosSimplesView
                               where idsproductos.Contains(datos.IdProducto)
                               select datos;
                return await consulta.ToListAsync();
            }
        }

        #endregion

        #region
        #endregion
    }
}
