using Athanasia.Models.Tables;
using Athanasia.Models.Util;
using Athanasia.Models.Views;
using Athanasia.Repositories;

namespace Athanasia.Services
{
    public class ServiceAthanasia : IRepositoryAthanasia
    {
        public Task<Usuario> ActivarUsuarioAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteInformacionCompraByIdAsync(int idinfocompra)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteUsuarioAsync(int idusuario)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> FindUsuarioByIdAsync(int idusuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<Categoria>> GetAllCategoriasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<FormatoLibroView>> GetAllFormatoLibroViewByIdLibroAsync(int idlibro)
        {
            throw new NotImplementedException();
        }

        public Task<List<Genero>> GetAllGenerosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<InformacionCompra>> GetAllInformacionComprabyIdUsuarioAsync(int idusuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<InformacionCompraView>> GetAllInformacionCompraViewByIdUsuarioAsync(int idusuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<PedidoView>> GetAllPedidoViewByIdUsuario(int idusuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductoSimpleView>> GetAllProductoSimpleViewAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductoSimpleView>> GetAllProductoSimpleViewByIds(List<int> idsproductos)
        {
            throw new NotImplementedException();
        }

        public Task<PaginacionModel<ProductoSimpleView>> GetAllProductoSimpleViewSearchPaginacionAsync(string palabra, int posicion, int ndatos)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductoView>> GetAllProductoViewAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Genero> GetGeneroByNombreAsync(string nombre)
        {
            throw new NotImplementedException();
        }

        public Task<InformacionCompra> GetInformacionCompraByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MetodoPago>> GetMetodoPagosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNextIdInformacionCompraAsync()
        {
            throw new NotImplementedException();
        }

        public int GetPedidoNextId()
        {
            throw new NotImplementedException();
        }

        public int GetPedidoProductoNextId()
        {
            throw new NotImplementedException();
        }

        public Task<List<PedidoProductoView>> GetPedidoProductoViewsByIdPedidoAsync(int idpedido)
        {
            throw new NotImplementedException();
        }

        public Task<ProductoView> GetProductoByIdAsync(int idproducto)
        {
            throw new NotImplementedException();
        }

        public Task<ProductoSimpleView> GetProductoSimpleByIdAsync(int idproducto)
        {
            throw new NotImplementedException();
        }

        public Task<PaginacionModel<ProductoSimpleView>> GetProductoSimpleViewsCategoriasGeneroAsync(string palabra, int posicion, int ndatos, List<int> idscategorias, List<int> idsgeneros)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductoSimpleView>> GetProductoSimpleViewTituloAutorAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PaginacionModel<ProductoSimpleView>> GetProductosSimplesPaginacionAsyn(int posicion, int ndatos)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductoView>> GetProductoViewByFormatoAsync(string formato)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetUsuarioByTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<InformacionCompra> InsertInformacionAsync(string nombre, string direccion, string indicaciones, int idmetodopago, int idusuario)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertListPedidoProductosAsync(int idpedido, List<PedidoProducto> productos)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> InsertPedidoAsync(int idusuario)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> LogInUserAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> RegistrarUsuarioAsync(string nombre, string apellido, string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePedidoEstadoCancelarAsync(int idpedido)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> UpdateUsuarioAsync(int idusuario, string nombre, string apellido, string email, string? imagen)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> UpdateUsuarioPasswordAsync(int idusuario, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> UpdateUsuarioTokenAsync(int idusuario)
        {
            throw new NotImplementedException();
        }
    }
}
