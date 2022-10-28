using System.ComponentModel.DataAnnotations;

namespace Travel.Models.editoriales
{
    public class editoriales
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? sede { get; set; }
    }
}
