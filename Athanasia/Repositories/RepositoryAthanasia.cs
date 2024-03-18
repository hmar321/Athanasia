﻿using Athanasia.Data;
using Athanasia.Extension;
using Athanasia.Helpers;
using Athanasia.Models.Tables;
using Athanasia.Models.Util;
using Athanasia.Models.Views;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Metrics;

#region VIEWS
//alter view V_PRODUCTO as
//select
//	ISNULL(p.ID_PRODUCTO, - 1) ID_PRODUCTO,
//    l.ID_LIBRO,
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
//group by p.ID_PRODUCTO, l.ID_LIBRO, l.TITULO, c.NOMBRE, a.NOMBRE,
//s.NOMBRE, l.SINOPSIS, l.FECHA_PUBLICACION, l.PORTADA,
//p.ISBN, p.PRECIO, e.NOMBRE, e.LOGO, f.NOMBRE



//alter view V_PRODUCTO_SIMPLE as
//select
//    ISNULL(p.ID_PRODUCTO, -1) ID_PRODUCTO,
//    l.ID_LIBRO,
//    l.TITULO,
//    l.PORTADA,
//    a.NOMBRE AUTOR,
//    p.PRECIO,
//    p.ID_FORMATO,
//    f.NOMBRE FORMATO,
//      CAST(1 as int) UNIDADES
//from Libro l
//left join AUTOR a on l.ID_AUTOR=a.ID_AUTOR and l.TITULO is not null and a.NOMBRE is not null
//inner join PRODUCTO p on l.ID_LIBRO=p.ID_LIBRO and p.PRECIO is not null
//inner join FORMATO f on p.ID_FORMATO=f.ID_FORMATO and f.NOMBRE is not null

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

//alter view V_PRODUCTO_BUSCADO as
//select
//    ISNULL(p.ID_PRODUCTO, -1) ID_PRODUCTO,
//    l.TITULO,
//    l.PORTADA,
//    a.NOMBRE AUTOR,
//    p.PRECIO,
//    p.ID_FORMATO,
//    f.NOMBRE FORMATO,
//    G.ID_GENERO,
//    l.ID_CATEGORIA,
//	1 UNIDADES
//from Libro l
//left join AUTOR a on l.ID_AUTOR=a.ID_AUTOR and l.TITULO is not null and a.NOMBRE is not null
//inner join PRODUCTO p on l.ID_LIBRO=p.ID_LIBRO and p.PRECIO is not null
//inner join FORMATO f on p.ID_FORMATO=f.ID_FORMATO and f.NOMBRE is not null
//inner join GENEROS_LIBROS gl on l.ID_LIBRO=gl.ID_LIBRO
//inner join GENERO g on gl.ID_GENERO=g.ID_GENERO

//alter view SP_FORMATO_LIBRO as
//select ISNULL(ID_PRODUCTO,-1) ID_PRODUCTO, ID_LIBRO, f.NOMBRE FORMATO from PRODUCTO p
//inner join FORMATO f on f.ID_FORMATO=p.ID_FORMATO

//alter view V_INFORMACION_COMPRA_USUARIO as
//select 
//ISNULL(ic.ID_INFORMACION_COMPRA,-1) ID_INFORMACION_COMPRA,
//ic.NOMBRE,
//ic.DIRECCION,
//ic.INDICACIONES,
//ic.ID_METODO_PAGO,
//mp.NOMBRE METODO_PAGO,
//ic.ID_USUARIO
//from INFORMACION_COMPRA ic
//inner join METODO_PAGO mp on ic.ID_METODO_PAGO=mp.ID_METODO_PAGO

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
//(@busqueda nvarchar(255), @posicion int, @ndatos int, @npaginas int out)
//as
//	select @npaginas=CEILING(COUNT(ID_PRODUCTO)/CAST(@ndatos AS FLOAT)) from
//	(select ID_PRODUCTO, ID_LIBRO, TITULO, AUTOR, ROW_NUMBER() over(partition by TITULO, AUTOR order by ID_PRODUCTO) REPETICION
//	from V_PRODUCTO_SIMPLE) AGRUPADOS
//	where REPETICION=1
//	and TITULO is not null
//	and AUTOR is not null
//	and dbo.LIMPIAR(TITULO) like @busqueda
//	or dbo.LIMPIAR(AUTOR) like @busqueda
//	and REPETICION = 1
//	select ID_PRODUCTO, ID_LIBRO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES
//	from
//		(select ID_PRODUCTO, ID_LIBRO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES,
//        ROW_NUMBER() over (order by ID_PRODUCTO) POSICION
//		from 
//			(select ID_PRODUCTO, ID_LIBRO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES,
//            ROW_NUMBER() over(partition by TITULO, AUTOR order by ID_PRODUCTO) REPETICION
//			from V_PRODUCTO_SIMPLE) QUERY
//		where REPETICION = 1
//		and TITULO is not null
//		and AUTOR is not null
//		and dbo.LIMPIAR(TITULO) like @busqueda
//		or dbo.LIMPIAR(AUTOR) like @busqueda
//		and REPETICION = 1) QUERY
//	where POSICION>=@ndatos*@posicion-(@ndatos-1) and POSICION<=@posicion*@ndatos
//	order by ID_PRODUCTO
//go

//alter procedure SP_PRODUCTOS
//as
//SELECT ID_PRODUCTO, ID_LIBRO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES
//FROM (
//	SELECT ID_PRODUCTO, ID_LIBRO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES,
//           ROW_NUMBER() OVER(PARTITION BY TITULO ORDER BY ID_PRODUCTO) AS REPETICION
//    FROM V_PRODUCTO_SIMPLE) PRODUCTOS
//WHERE REPETICION = 1
//and TITULO is not null
//and AUTOR is not null
//order by ID_PRODUCTO
//go


//alter procedure SP_PRODUCTO_SIMPLE_PAGINACION
//(@posicion int, @ndatos int, @npaginas int out)
//as
//	select @npaginas=CEILING(COUNT(ID_PRODUCTO)/CAST(@ndatos AS FLOAT)) from
//	(select ID_PRODUCTO, ROW_NUMBER() over(partition by TITULO, AUTOR order by ID_PRODUCTO) REPETICION
//	from V_PRODUCTO_SIMPLE) AGRUPADOS
//	where REPETICION=1
//	select ID_PRODUCTO, ID_LIBRO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES from
//		(select 
//			ID_PRODUCTO, ID_LIBRO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES,
//            ROW_NUMBER() over (order by ID_PRODUCTO) POSICION
//		from
//			(select
//			ID_PRODUCTO, ID_LIBRO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES,
//            ROW_NUMBER() over(partition by TITULO, AUTOR order by ID_PRODUCTO) REPETICION
//			from V_PRODUCTO_SIMPLE) QUERY
//		where REPETICION=1) PRIMEROS
//	where  POSICION>=@ndatos*@posicion-(@ndatos-1) and POSICION<=@posicion*@ndatos
//go

//alter procedure SP_GENEROS
//as
//	select distinct g.ID_GENERO, g.NOMBRE, g.DESCRIPCION from GENERO g
//	inner join GENEROS_LIBROS gl on gl.ID_GENERO=g.ID_GENERO
//	group by g.ID_GENERO, g.NOMBRE, g.DESCRIPCION
//	order by g.NOMBRE
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


//create procedure SP_SEARCH_PRODUCTOS_FILTRO
//(@busqueda nvarchar(255), @posicion int, @ndatos int, @categorias nvarchar(255), @generos nvarchar(255), @npaginas int out)
//as
//	select @npaginas=CEILING(COUNT(ID_PRODUCTO)/CAST(@ndatos AS FLOAT)) from
//		(select ID_PRODUCTO, TITULO, AUTOR, ROW_NUMBER() over(partition by TITULO, AUTOR order by ID_PRODUCTO) REPETICION
//		from
//			(select distinct ID_PRODUCTO, TITULO, AUTOR
//			from V_PRODUCTO_BUSCADO
//			where ID_CATEGORIA in (select CAST(value as int) from STRING_SPLIT(@categorias, ','))
//			and ID_GENERO in (select CAST(value as int) from STRING_SPLIT(@generos, ','))) DISTINTOS) AGRUPADOS
//	where REPETICION=1
//	and TITULO is not null
//	and AUTOR is not null
//	and dbo.LIMPIAR(TITULO) like @busqueda
//	or dbo.LIMPIAR(AUTOR) like @busqueda
//	and REPETICION = 1
//	select ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES
//	from
//		(select ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES,
//        ROW_NUMBER() over (order by ID_PRODUCTO) POSICION
//		from 
//		(select 
//		ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES,
//        ROW_NUMBER() over(partition by TITULO, AUTOR order by ID_PRODUCTO) REPETICION
//		from
//			(select distinct ID_PRODUCTO, TITULO, PORTADA, AUTOR, PRECIO, ID_FORMATO, FORMATO, UNIDADES
//			from V_PRODUCTO_BUSCADO
//			where ID_CATEGORIA in (select CAST(value as int) from STRING_SPLIT(@categorias, ','))
//			and ID_GENERO in (select CAST(value as int) from STRING_SPLIT(@generos, ','))) QUERY) GRUPO
//		where REPETICION = 1
//		and TITULO is not null
//		and AUTOR is not null
//		and dbo.LIMPIAR(TITULO) like @busqueda
//		or dbo.LIMPIAR(AUTOR) like @busqueda
//		and REPETICION = 1) QUERY
//	where POSICION>=@ndatos*@posicion-(@ndatos-1) and POSICION<=@posicion*@ndatos
//	order by ID_PRODUCTO
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
        public async Task<List<ProductoView>> GetAllProductoViewAsync()
        {
            List<ProductoView> productos = await this.context.ProductosView.ToListAsync();
            return productos;
        }

        public async Task<List<ProductoView>> GetProductoViewByFormatoAsync(string formato)
        {
            List<ProductoView> productos = await this.context.ProductosView.Where(o => o.Formato == formato).ToListAsync();
            return productos;
        }

        public async Task<ProductoView> GetProductoByIdAsync(int idproducto)
        {
            ProductoView producto = await this.context.ProductosView.FirstOrDefaultAsync(p => p.IdProducto == idproducto);
            return producto;
        }
        #endregion

        #region PRODUCTO_SIMPLE_VIEW


        public async Task<ProductoSimpleView> GetProductoSimpleByIdAsync(int idproducto)
        {
            ProductoSimpleView producto = await this.context.ProductosSimplesView.FirstOrDefaultAsync(p => p.IdProducto == idproducto);
            return producto;
        }

        public async Task<List<ProductoSimpleView>> GetAllProductoSimpleViewAsync()
        {
            List<ProductoSimpleView> productosSimples = await this.context.ProductosSimplesView.ToListAsync();
            return productosSimples;
        }

        public async Task<List<ProductoSimpleView>> GetProductoSimpleViewTituloAutorAsync()
        {
            string sql = "SP_PRODUCTOS";
            var consulta = this.context.ProductosSimplesView.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }
        public async Task<PaginacionModel<ProductoSimpleView>> GetProductosSimplesPaginacionAsyn(int posicion, int ndatos)
        {
            string sql = "SP_PRODUCTO_SIMPLE_PAGINACION @posicion,@ndatos,@npaginas out";
            SqlParameter paramposicion = new SqlParameter("@posicion", posicion);
            SqlParameter paramndatos = new SqlParameter("@ndatos", ndatos);
            SqlParameter paramnpaginas = new SqlParameter("@npaginas", -1);
            paramnpaginas.Direction = ParameterDirection.Output;
            var consulta = this.context.ProductosSimplesView.FromSqlRaw(sql, paramposicion, paramndatos, paramnpaginas);
            List<ProductoSimpleView> productos = await consulta.ToListAsync();
            int registros = int.Parse(paramnpaginas.Value.ToString());
            PaginacionModel<ProductoSimpleView> model = new PaginacionModel<ProductoSimpleView>
            {
                Lista = productos,
                NumeroPaginas = registros
            };
            return model;
        }

        public async Task<PaginacionModel<ProductoSimpleView>> GetAllProductoSimpleViewSearchPaginacionAsync(string palabra, int posicion, int ndatos)
        {
            if (palabra == null)
            {
                palabra = "";
            }
            string busqueda = palabra.Limpiar();
            string sql = "SP_SEARCH_PRODUCTOS @busqueda,@posicion,@ndatos,@npaginas out";
            SqlParameter parambusqueda = new SqlParameter("@busqueda", "%" + busqueda + "%");
            SqlParameter paramposicion = new SqlParameter("@posicion", posicion);
            SqlParameter paramndatos = new SqlParameter("@ndatos", ndatos);
            SqlParameter paramnpaginas = new SqlParameter("@npaginas", -1);
            paramnpaginas.Direction = ParameterDirection.Output;
            var consulta = this.context.ProductosSimplesView.FromSqlRaw(sql, parambusqueda, paramposicion, paramndatos, paramnpaginas);
            List<ProductoSimpleView> productos = await consulta.ToListAsync();
            int registros = int.Parse(paramnpaginas.Value.ToString());
            PaginacionModel<ProductoSimpleView> model = new PaginacionModel<ProductoSimpleView>
            {
                Lista = productos,
                NumeroPaginas = registros
            };
            return model;

        }

        public async Task<List<ProductoSimpleView>> GetAllProductoSimpleViewByIds(List<int> idsproductos)
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

        public async Task<PaginacionModel<ProductoSimpleView>> GetProductoSimpleViewsCategoriasGeneroAsync(string palabra, int posicion, int ndatos, List<int> idscategorias, List<int> idsgeneros)
        {
            string categorias = String.Join(",", idscategorias);
            string generos = String.Join(",", idsgeneros);
            if (palabra == null)
            {
                palabra = "";
            }
            string busqueda = palabra.Limpiar();
            string sql = "SP_SEARCH_PRODUCTOS_FILTRO @busqueda,@posicion,@ndatos,@categorias,@generos,@npaginas out";
            SqlParameter parambusqueda = new SqlParameter("@busqueda", "%" + busqueda + "%");
            SqlParameter paramposicion = new SqlParameter("@posicion", posicion);
            SqlParameter paramndatos = new SqlParameter("@ndatos", ndatos);
            SqlParameter paramcategorias = new SqlParameter("@categorias", categorias);
            SqlParameter paramgeneros = new SqlParameter("@generos", generos);
            SqlParameter paramnpaginas = new SqlParameter("@npaginas", -1);
            paramnpaginas.Direction = ParameterDirection.Output;
            var consulta = this.context.ProductosSimplesView.FromSqlRaw(sql, parambusqueda, paramposicion, paramndatos, paramcategorias, paramgeneros, paramnpaginas);
            List<ProductoSimpleView> productos = await consulta.ToListAsync();
            int registros = int.Parse(paramnpaginas.Value.ToString());
            PaginacionModel<ProductoSimpleView> model = new PaginacionModel<ProductoSimpleView>
            {
                Lista = productos,
                NumeroPaginas = registros
            };
            return model;
        }


        #endregion

        #region USUARIO
        public async Task<Usuario> FindUsuarioByIdAsync(int idusuario)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == idusuario);
        }

        public async Task<Usuario> UpdateUsuarioAsync(int idusuario, string nombre, string apellido, string email, string? imagen)
        {
            Usuario usuario = await this.FindUsuarioByIdAsync(idusuario);
            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            usuario.Email = email;
            if (imagen != null)
            {
                usuario.Imagen = imagen;
            }
            await this.context.SaveChangesAsync();
            return usuario;
        }

        public async Task<int> DeleteUsuarioAsync(int idusuario)
        {
            Usuario usuario = await this.FindUsuarioByIdAsync(idusuario);
            this.context.Usuarios.Remove(usuario);
            return await this.context.SaveChangesAsync();
        }

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

        public async Task<Usuario> UpdateUsuarioTokenAsync(int idusuario)
        {
            Usuario usuario = await this.FindUsuarioByIdAsync(idusuario);
            usuario.Token = HelperTools.GenerateTokenMail();
            await this.context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> GetUsuarioByTokenAsync(string token)
        {
            Usuario usuario = await this.context.Usuarios.FirstOrDefaultAsync(u => u.Token == token);
            if (usuario != null)
            {
                usuario.Token = "";
                await this.context.SaveChangesAsync();
            }
            return usuario;
        }


        public async Task<Usuario> UpdateUsuarioPasswordAsync(int idusuario, string password)
        {
            Usuario usuario = await this.context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == idusuario);
            usuario.Salt = HelperTools.GenerateSalt();
            usuario.Pass = HelperCryptography.EncryptPassword(password, usuario.Salt);
            await this.context.SaveChangesAsync();
            return usuario;
        }

        #endregion

        #region PEDIDO
        public int GetPedidoNextId()
        {
            int nextid = -1;
            if (this.context.Pedidos.Count() == 0)
            {
                nextid = 1;
            }
            else
            {
                nextid = this.context.Pedidos.Max(o => o.IdPedido);
            }
            return nextid;
        }
        public async Task<Pedido> InsertPedidoAsync()
        {
            //id procesando
            int estadopedido = 2;
            int nextid = GetPedidoNextId();
            Pedido pedido = new Pedido
            {
                IdPedido = nextid,
                FechaSolicitud = DateTime.Now,
                FechaEstimada = DateTime.Now.AddDays(3),
                FechaEntrega = null,
                IdEstadoPedido = estadopedido
            };
            await this.context.Pedidos.AddAsync(pedido);
            await this.context.SaveChangesAsync();
            return pedido;
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

        #region CATEGORIAS

        public async Task<List<Categoria>> GetAllCategoriasAsync()
        {
            return await this.context.Categorias.ToListAsync();
        }

        #endregion

        #region GENEROS

        public async Task<List<Genero>> GetAllGenerosAsync()
        {
            string sql = "SP_GENEROS";
            return await this.context.Generos.FromSqlRaw(sql).ToListAsync();
        }

        #endregion

        #region FORMATOS_LIBRO_VIEW

        public async Task<List<FormatoLibroView>> GetAllFormatoLibroViewByIdLibroAsync(int idlibro)
        {
            return await this.context.FormatosLibroView.Where(f => f.IdLibro == idlibro).ToListAsync();
        }

        #endregion

        #region INFORMACION_COMPRA

        public async Task<List<InformacionCompra>> GetAllInformacionComprabyIdUsuarioAsync(int idusuario)
        {
            List<InformacionCompra> info = await this.context.InformacionesCompra.Where(ic => ic.IdUsuario == idusuario).ToListAsync();
            if (info.Count == 0)
            {
                return null;
            }
            return info;
        }

        public async Task<int> GetNextIdInformacionCompraAsync()
        {
            if (this.context.InformacionesCompra.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.InformacionesCompra.MaxAsync(u => u.IdInformacionCompra) + 1;
            }
        }

        public async Task<InformacionCompra> InsertInformacionAsync(string nombre, string direccion, string indicaciones, int idmetodopago, int idusuario)
        {
            int nextid = await this.GetNextIdInformacionCompraAsync();
            InformacionCompra info = new InformacionCompra
            {
                IdInformacionCompra = nextid,
                Nombre = nombre,
                Direccion = direccion,
                Indicaciones = indicaciones,
                IdMetodoPago = idmetodopago,
                IdUsuario = idusuario
            };
            await this.context.InformacionesCompra.AddAsync(info);
            await this.context.SaveChangesAsync();
            return info;
        }

        public async Task<InformacionCompra> GetInformacionCompraByIdAsync(int id)
        {
            return this.context.InformacionesCompra.FirstOrDefault(ic => ic.IdInformacionCompra == id);
        }

        public async Task<int> DeleteInformacionCompraByIdAsync(int idinfocompra)
        {
            InformacionCompra info = await this.GetInformacionCompraByIdAsync(idinfocompra);
            this.context.Remove(info);
            return await this.context.SaveChangesAsync();
        }

        #endregion

        #region METODO_PAGO

        public async Task<List<MetodoPago>> GetMetodoPagosAsync()
        {
            return await this.context.MetodosPago.ToListAsync();
        }

        #endregion

        #region INFORMACION_COMPRA_VIEW

        public async Task<List<InformacionCompraView>> GetAllInformacionCompraViewByIdUsuarioAsync(int idusuario)
        {
            if (this.context.InformacionesCompraView == null)
            {
                return new List<InformacionCompraView>();
            }
            return await this.context.InformacionesCompraView.Where(dp => dp.IdUsuario == idusuario).ToListAsync();
        }
        #endregion

        #region 
        #endregion

    }
}
