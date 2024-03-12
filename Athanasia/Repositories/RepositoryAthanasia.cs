using Athanasia.Data;
using Athanasia.Extension;
using Athanasia.Helpers;
using Athanasia.Models.Tables;
using Athanasia.Models.Views;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

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
//    p.ID_FORMATO,
//	1 UNIDADES
//from Libro l
//left join AUTOR a on l.ID_AUTOR=a.ID_AUTOR and l.TITULO is not null and a.NOMBRE is not null
//inner join PRODUCTO p on l.ID_LIBRO=p.ID_LIBRO and p.PRECIO is not null

//create view V_PEDIDO_PRODUCTOS as
// select 
//	pp.ID_PEDIDO_PRODUCTO,
//   p.ID_PEDIDO,
//   l.TITULO,
//   f.NOMBRE FORMATO,
//   pp.UNIDADES,
//   pr.PRECIO,
//   ep.NOMBRE ESTADO_PEDIDO
//from PEDIDO p
//inner join ESTADO_PEDIDO ep on p.ID_ESTADO_PEDIDO=ep.ID_ESTADO_PEDIDO
//inner join PEDIDOS_PRODUCTOS pp on p.ID_PEDIDO=pp.ID_PEDIDO
//inner join PRODUCTO pr on pr.ID_PRODUCTO=pr.ID_PRODUCTO
//inner join FORMATO f on pr.ID_FORMATO=f.ID_FORMATO
//inner join LIBRO l on pr.ID_LIBRO=l.ID_LIBRO


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
//SELECT ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, UNIDADES
//FROM (
//	SELECT ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, UNIDADES,
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

//alter procedure SP_PRODUCTOS
//as
//SELECT ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, UNIDADES
//FROM (
//	SELECT ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, UNIDADES,
//           ROW_NUMBER() OVER(PARTITION BY TITULO ORDER BY ID_PRODUCTO) AS REPETICION
//    FROM V_PRODUCTO_SIMPLE) PRODUCTOS
//WHERE REPETICION = 1
//and TITULO is not null
//and AUTOR is not null
//order by ID_PRODUCTO
//go


//alter procedure SP_INSERT_PEDIDOS_PRODUCTOS
//(@unidades int, @idpedido int, @idproducto int)
//as
//	declare @maxid int
//	select @maxid=MAX(ID_PEDIDO_PRODUCTO) from PEDIDOS_PRODUCTOS

//	insert into PEDIDOS_PRODUCTOS values
//	(@maxid, @unidades, @idpedido, @idproducto)

//	select ID_PEDIDO_PRODUCTO, UNIDADES, ID_PEDIDO, ID_PRODUCTO 
//	from PEDIDOS_PRODUCTOS
//	where ID_PEDIDO_PRODUCTO=@maxid
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

        public async Task<ProductoSimpleView> FindProductoSimpleAsync(int idproducto)
        {
            ProductoSimpleView producto = await this.context.ProductosSimplesView.FirstOrDefaultAsync(p => p.IdProducto == idproducto);
            return producto;
        }

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

        public async Task<Usuario> RegistrarUsuarioAsync(string nombre, string apellido, string email, string password)
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
            usuario.IdEstado = HelperTools.GetEstadoId(Estados.Pendiente);
            usuario.Token = HelperTools.GenerateTokenMail();
            usuario.IdRol = HelperTools.GetRolId(Roles.Cliente);
            this.context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> ActivarUsuarioAsync(string token)
        {
            Usuario user = await this.context.Usuarios.FirstOrDefaultAsync(u => u.Token == token);
            user.IdEstado = HelperTools.GetEstadoId(Estados.Activo);
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

        #region PEDIDO

        public async Task<Pedido> InsertPedidoAsync()
        {
            //id procesando
            int estadopedido = 2;
            int nextid = this.context.Pedidos.Max(o=>o.IdPedido);
            Pedido pedido = new Pedido
            {
                IdPedido=nextid,
                FechaSolicitud=DateTime.Now,
                FechaEstimada=DateTime.Now.AddDays(3),
                FechaEntrega=null,
                IdEstadoPedido=estadopedido
            };
            return null;
        }

        #endregion

        #region PEDIDOS_PRODUCTOS

        public async Task<PedidosProductos> InsertPedidoProductosAsync(int unidades, int idpedido, int idproducto)
        {
            string sql = "SP_INSERT_PEDIDOS_PRODUCTOS @unidades,@idpedido,@idproducto";
            SqlParameter paramunidades = new SqlParameter("@unidades", unidades);
            SqlParameter paramidpedido = new SqlParameter("@idpedido", idpedido);
            SqlParameter paramidproducto = new SqlParameter("@idproducto", idproducto);
            var consulta = this.context.PedidosProductos.FromSqlRaw(sql, paramunidades, paramidpedido, paramidproducto);
            return await consulta.FirstOrDefaultAsync();
        }

        #endregion

        #region 
        #endregion

    }
}
