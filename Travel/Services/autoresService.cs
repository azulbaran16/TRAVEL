using Microsoft.EntityFrameworkCore;
using Travel.Context;
using Travel.Models.autores;
using Travel.Models.editoriales;

namespace Travel.Services
{
    public class autoresService
    {
        dbcontext con;
        public autoresService(dbcontext db)
        {
            con = db;
        }

        public async Task<List<autores>> getAll()
        {
            return await con.autores.ToListAsync();
        }
        public async Task<autores> getById(int id)
        {
            return await con.autores.FirstAsync(f => f.id == id);
        }
        public async Task<int> create(autores autor)
        {
            await con.autores.AddAsync(autor);
            int rs = await con.SaveChangesAsync();
            return autor.id;
        }
        public async Task<bool> update(autores autor)
        {
            con.Entry(autor).State = EntityState.Modified;
            int rs = await con.SaveChangesAsync();
            return rs > 0 ? true : false;
        }
        public async Task<bool> delete(int id)
        {
            autores autor = await con.autores.FirstAsync(f => f.id == id);
            con.autores.Remove(autor);
            int rs = await con.SaveChangesAsync();
            return rs > 0 ? true : false;
        }
    }
}
