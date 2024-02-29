﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Athanasia.Models
{
    [Table("EDITORIAL")]
    public class Editorial
    {
        [Key]
        [Column("ID_EDITORIAL")]
        public int IdEditorial { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("WEB")]
        public string Web { get; set; }
    }
}