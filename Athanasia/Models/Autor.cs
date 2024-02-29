using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("AUTOR")]
    public class Autor
    {
        [Key]
        [Column("ID_AUTOR")]
        public int IdAutor { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("APELLIDO")]
        public string Apellido { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("IMAGEN")]
        public string Imagen { get; set; }
    }
}
