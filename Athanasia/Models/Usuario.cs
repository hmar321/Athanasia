using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("APELLIDO")]
        public string Apellido { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("FECHA_NACIMIENTO")]
        public DateTime FechaNacimiento { get; set; }

        [Column("IMAGEN")]
        public string Imagen { get; set; }

        [Column("ID_ROL")]
        public int IdRol { get; set; }
    }

}
