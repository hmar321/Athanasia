using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("GENERO")]
    public class Genero
    {
        [Key]
        [Column("ID_GENERO")]
        public int IdGenero { get; set; }

        [Column("NOMBRE")]
        public string? Nombre { get; set; }

        [Column("DESCRIPCION")]
        public string? Descripcion { get; set; }
    }
}
