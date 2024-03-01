using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athanasia.Models
{
    [Table("FORMATO_LIBRO")]
    public class FormatoLibro
    {
        [Key]
        [Column("ID_FORMATO_LIBRO")]
        public int IdFormatoLibro { get; set; }
        [Column("ID_FORMATO")]
        public int IdFormato { get; set; }
        [Column("ID_LIBRO")]
        public int IdLibro { get; set; }
        [Column("PRECIO")]
        public double Precio { get; set; }

    }
}
