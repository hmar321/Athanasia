using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athanasia.Models
{
    [Table("OPINION")]
    public class Opinion
    {
        [Key]
        [Column("ID_OPINION")]
        public int Id_Opinion { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
    }
}
