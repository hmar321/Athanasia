using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("GENERO")]
    public class Genero
    {
        [Key]
        [Column("ID_GENERO_LIBRO")]
        public int IdGeneroLibro { get; set; }
        [Column("ID_GENERO")]
        public int IdGenero { get; set; }
        [Column("ID_LIBRO")]
        public int IdLibro { get; set; }
    }
}
