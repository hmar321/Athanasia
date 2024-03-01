using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("ESTADO_LECTURA")]
    public class EstadoLectura
    {
        [Key]
        [Column("ID_ESTADO_LECTURA")]
        public int IdEstadoLectura { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
    }
}
