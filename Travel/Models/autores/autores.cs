using System.ComponentModel.DataAnnotations;

namespace Travel.Models.autores
{
    public class autores
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? apellidos { get; set; }
    }
}
