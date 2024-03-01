using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athanasia.Models
{
    [Table("PRODUCTO")]
    public class Producto
    {
        [Key]
        [Column("ID_PRODUCTO")]
        public int IdFormatoLibro { get; set; }
        [Column("ID_FORMATO")]
        public int IdFormato { get; set; }
        [Column("ID_LIBRO")]
        public int IdLibro { get; set; }
        [Column("PRECIO")]
        public double Precio { get; set; }

    }
}
