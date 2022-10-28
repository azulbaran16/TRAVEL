using System.ComponentModel.DataAnnotations;

namespace Travel.Models.libros
{
    public class libros
    {
        [Key]
        public int ISBN { get; set; }
        public int editoriales_id { get; set; }
        public string? titulo { get; set; }
        public string? sinopsis { get; set; }
        public string? n_paginas { get; set; }
    }
}
