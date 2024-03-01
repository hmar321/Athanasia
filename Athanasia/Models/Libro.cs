using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Athanasia.Models
{
    [Table("LIBRO")]
    public class Libro
    {
        [Key]
        [Column("ID_LIBRO")]
        public int IdLibro { get; set; }
        [Column("ISBN")]
        public int Isbn { get; set; }
        [Column("TITULO")]
        public string? Titulo { get; set; }
        [Column("SINOPSIS")]
        public string? Sinopsis { get; set; }
        [Column("FECHA_PUBLICACION")]
        public DateTime FechaPublicacion { get; set; }
        [Column("PORTADA")]
        public string Portada { get; set; }
        [Column("STOCK")]
        public bool Stock { get; set; }
        [Column("ID_AUTOR")]
        public int IdAutor { get; set; }
        [Column("ID_EDITORIAL")]
        public int IdEditorial { get; set; }
        [Column("ID_GENERO")]
        public int IdGenero { get; set; }
    }
}
