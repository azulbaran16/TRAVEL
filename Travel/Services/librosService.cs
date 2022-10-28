using Microsoft.EntityFrameworkCore;
using Travel.Context;
using Travel.Models.autores_has_libros;
using Travel.Models.libros;

namespace Travel.Services
{
    public class librosService
    {
        dbcontext con;
        public librosService(dbcontext db)
        {
            con = db;
        }

        public async Task<List<libros>> getAll()
        {
            return await con.libros.ToListAsync();
        }
        public async Task<libros> getById(int id)
        {
            return await con.libros.FirstAsync(f => f.ISBN == id);
        }
        public async Task<int> create(libros libro)
        {
            await con.libros.AddAsync(libro);
            int rs = await con.SaveChangesAsync();
            return libro.ISBN;
        }
        public async Task<bool> update(libros libro)
        {
            con.Entry(libro).State = EntityState.Modified;
            int rs = await con.SaveChangesAsync();
            return rs > 0 ? true : false;
        }
        public async Task<bool> delete(int id)
        {
            libros libro = await con.libros.FirstAsync(f => f.ISBN == id);
            con.libros.Remove(libro);
            int rs = await con.SaveChangesAsync();
            return rs > 0 ? true : false;
        }
        public async Task<bool> associateAutor(int id_autor, int ISBN)
        {
            autores_has_libros aut_lib = new autores_has_libros()
            {
                autores_id= id_autor,
                libros_ISBN = ISBN
            };
            await con.autores_has_libros.AddAsync(aut_lib);
            int rs = await con.SaveChangesAsync();
            return rs > 0 ? true : false;
        }
        public async Task<bool> disassociateAutor(int id_autor, int ISBN)
        {
            autores_has_libros aut_lib = await con.autores_has_libros.Where(x => x.autores_id == id_autor
            && x.libros_ISBN == ISBN).FirstAsync();

            con.autores_has_libros.Remove(aut_lib);

            int rs = await con.SaveChangesAsync();
            return rs > 0 ? true : false;
        }
    }
}
