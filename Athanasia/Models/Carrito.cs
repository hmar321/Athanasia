using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("CARRITO")]
    public class Carrito
    {
        [Key]
        [Column("ID_CARRITO")]
        public int IdCarrito { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }
    }
}
