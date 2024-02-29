using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("FAVORITO")]
    public class Favorito
    {
        [Key]
        [Column("ID_FAVORITO")]
        public int IdFavorito { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("ISBN_LIBRO")]
        public int IsbnLibro { get; set; }
    }
}
