﻿using Athanasia.Extension;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athanasia.Models.Views
{
    [Table("V_PRODUCTO_SIMPLE")]
    public class ProductoSimpleView
    {
        [Key]
        [Column("ID_PRODUCTO")]
        public int IdProducto { get; set; }
        [Column("TITULO")]
        public string Titulo { get; set; }
        [Column("PORTADA")]
        public string? Portada { get; set; }
        [Column("AUTOR")]
        public string Autor { get; set; }
        [Column("PRECIO")]
        public double Precio { get; set; }
        [Column("ID_FORMATO")]
        public int IdFormato { get; set; }
        [Column("UNIDADES")]
        public int Unidades { get; set; }
    }
}
