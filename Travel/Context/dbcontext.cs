using Microsoft.EntityFrameworkCore;
using Travel.Models.autores;
using Travel.Models.autores_has_libros;
using Travel.Models.editoriales;
using Travel.Models.libros;

namespace Travel.Context
{
    public partial class dbcontext: DbContext
    {
        public dbcontext(DbContextOptions<dbcontext> opt) : base(opt)
        {

        }

        public DbSet<autores> autores { get; set; }
        public DbSet<autores_has_libros> autores_has_libros { get; set; }
        public DbSet<editoriales> editoriales { get; set; }
        public DbSet<libros> libros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<autores_has_libros>().HasKey(x => new { x.libros_ISBN, x.autores_id });
        }
    }
}
