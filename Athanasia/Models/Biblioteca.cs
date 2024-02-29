using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("Biblioteca")]
    public class Biblioteca
    {
        [Key]
        [Column("ID_BIBLIOTECA")]
        public int IdBiblioteca { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("ID_LIBRO")]
        public int IdLibro { get; set; }

        [Column("FECHA_COMPRA")]
        public DateTime FechaCompra { get; set; }

        [Column("VALORACION")]
        public int Valoracion { get; set; }

        [Column("ID_ESTADO_LECTURA")]
        public int IdEstadoLectura { get; set; }
    }
}
