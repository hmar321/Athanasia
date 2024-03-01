using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athanasia.Models
{
    [Table("CATEGORIA_LIBRO")]
    public class CategoriaLibro
    {
        [Key]
        [Column("ID_CATEGORIA_LIBRO")]
        public int IdCategoriaLibro { get; set; }
        [Column("ID_CATEGORIA")]
        public int IdCategoria { get; set; }
        [Column("ID_LIBRO")]
        public int IdLibro { get; set; }
    }
}
