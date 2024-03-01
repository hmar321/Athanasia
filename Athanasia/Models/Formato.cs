using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athanasia.Models
{
    [Table("FORMATO_LIBRO")]
    public class Formato
    {
        [Key]
        [Column("ID_FORMATO")]
        public int IdFormato { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

    }
}
