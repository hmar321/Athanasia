using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athanasia.Models.Tables
{
    [Table("PEDIDOS_PRODUCTOS")]
    public class PedidosProductos
    {
        [Key]
        [Column("ID_PEDIDO_PRODUCTO")]
        public int IdPedidoProducto { get; set; }
        [Column("ID_PEDIDO")]
        public int? IdPedido { get; set; }
        [Column("ID_PRODUCTO")]
        public int? IdProducto { get; set; }
    }
}
