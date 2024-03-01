using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athanasia.Models
{
    [Table("REVIEW")]
    public class Review
    {
        [Key]
        [Column("ID_REVIEW")]
        public int IdReview { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("ID_LIBRO")]
        public int IdLibro { get; set; }

        [Column("PUNTUACION")]
        public int Puntuacion { get; set; }

        [Column("COMENTARIO")]
        public string Comentario { get; set; }

    }
}
