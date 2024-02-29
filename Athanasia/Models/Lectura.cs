using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("LECTURA")]
    public class Lectura
    {
        [Key]
        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
    }
}
