using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("ESTADO")]
    public class Estado
    {
        [Key]
        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
    }
}
