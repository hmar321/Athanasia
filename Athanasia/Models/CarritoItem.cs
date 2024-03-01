using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("CARRITO_ITEM")]
    public class CarritoItem
    {
        [Key]
        [Column("ID_CARRITO_ITEM")]
        public int IdCarritoItem { get; set; }

        [Column("ID_CARRITO")]
        public int IdCarrito { get; set; }

        [Column("ID_FORMATO_LIBRO")]
        public int IdFormatoLibro { get; set; }
    }
}
