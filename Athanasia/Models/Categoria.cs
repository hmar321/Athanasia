using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        [Column("ID_CATEGORIA")]
        public int IdCategoria { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
    }
}
