using Athanasia.Data;
using Athanasia.Extension;
using Athanasia.Helpers;
using Athanasia.Models.Tables;
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

//alter procedure SP_SEARCH_PRODUCTOS
//(@busqueda nvarchar(255))
//as
//SELECT ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO
//FROM (
//	SELECT ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO,
//           ROW_NUMBER() OVER(PARTITION BY TITULO ORDER BY ID_PRODUCTO) AS REPETICION
//    FROM V_PRODUCTO_SIMPLE) PRODUCTOS
//WHERE REPETICION = 1
//and TITULO is not null
//and AUTOR is not null
//and dbo.LIMPIAR(TITULO) like @busqueda
//or dbo.LIMPIAR(AUTOR) like @busqueda
//and REPETICION = 1
//order by ID_PRODUCTO
//go

//create procedure SP_PRODUCTOS
//as
//SELECT ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO
//FROM (
//	SELECT ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO,
//           ROW_NUMBER() OVER(PARTITION BY TITULO ORDER BY ID_PRODUCTO) AS REPETICION
//    FROM V_PRODUCTO_SIMPLE) PRODUCTOS
//WHERE REPETICION = 1
//and TITULO is not null
//and AUTOR is not null
//order by ID_PRODUCTO
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

        public async Task<List<ProductoSimpleView>> GetProductoSimplesViewAsync()
        {
            string sql = "SP_PRODUCTOS";
            var consulta = this.context.ProductosSimplesView.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }

        public async Task<List<ProductoSimpleView>> FindProductosSimplesViewAsync(string palabra)
        {
            if (palabra == null)
            {
                return await this.GetProductoSimplesViewAsync();
            }
            else
            {

                string busqueda = palabra.Limpiar();
                string sql = "SP_SEARCH_PRODUCTOS @busqueda";
                SqlParameter parambusqueda = new SqlParameter("@busqueda", "%" + busqueda + "%");
                var consulta = this.context.ProductosSimplesView.FromSqlRaw(sql, parambusqueda);
                return await consulta.ToListAsync();
            }
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

        #region USUARIO

        private async Task<int> GetMaxIdUsuarioAsync()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Usuarios.MaxAsync(u => u.IdUsuario) + 1;
            }
        }

        public async Task<Usuario> RegistrarUsuarioAsync(string nombre,string apellido, string email, string password)
        {
            Usuario usuario = new Usuario();
            usuario.IdUsuario = await this.GetMaxIdUsuarioAsync();
            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            usuario.Email = email;
            usuario.Password = password;
            usuario.Imagen = "usuario.png";
            usuario.Salt = HelperTools.GenerateSalt();
            usuario.Pass = HelperCryptography.EncryptPassword(password, usuario.Salt);
            usuario.IdEstado = HelperEstados.GetEstadoId(Estados.Pendiente);
            usuario.Token = HelperTools.GenerateTokenMail();
            usuario.IdRol = HelperRoles.GetRolId(Roles.Cliente);
            this.context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> ActivarUsuarioAsync(string token)
        {
            Usuario user = await this.context.Usuarios.FirstOrDefaultAsync(u => u.Token == token);
            user.IdEstado = HelperEstados.GetEstadoId(Estados.Activo);
            user.Token = "";
            await this.context.SaveChangesAsync();
            return user;
        }

        public async Task<Usuario> LogInUserAsync(string email, string password)
        {
            Usuario usuario = await this.context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
            {
                return null;
            }
            else
            {
                string salt = usuario.Salt;
                byte[] temp = HelperCryptography.EncryptPassword(password, salt);
                byte[] passUser = usuario.Pass;
                bool respuesta = HelperTools.CompareArrays(temp, passUser);
                if (respuesta == true)
                {
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region
        #endregion
    }
}
