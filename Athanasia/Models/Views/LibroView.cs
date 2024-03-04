using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athanasia.Models.Views
{
    [Table("V_LIBRO")]
    public class LibroView
    {
        [Key]
        [Column("ID_LIBRO")]
        public int IdLibro { get; set; }
        [Column("TITULO")]
        public string? Titulo { get; set; }
        [Column("SINOPSIS")]
        public string? Sinopsis { get; set; }
        [Column("FECHA_PUBLICACION")]
        public DateTime? FechaPublicacion { get; set; }
        [Column("PORTADA")]
        public string? Portada { get; set; }
        [Column("CATEGORIA")]
        public string? Categoria { get; set; }
        [Column("AUTOR")]
        public string? Autor { get; set; }
        [Column("GENEROS")]
        public string? Generos { get; set; }
        [Column("SAGA")]
        public string? Saga { get; set; }
    }
}
