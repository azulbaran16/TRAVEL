using Microsoft.EntityFrameworkCore;
using Travel.Context;
using Travel.Models.editoriales;

namespace Travel.Services
{
    public class editorialesService
    {
        dbcontext con;
        public editorialesService(dbcontext db)
        {
            con = db;
        }

        public async Task<List<editoriales>> getAll()
        {
            return await con.editoriales.ToListAsync();
        }
        public async Task<editoriales> getById(int id)
        {
            return await con.editoriales.FirstAsync(f => f.id == id);
        }
        public async Task<int> create(editoriales editorial)
        {
            await con.editoriales.AddAsync(editorial);
            int rs = await con.SaveChangesAsync();
            return editorial.id;
        }
        public async Task<bool> update(editoriales libro)
        {
            con.Entry(libro).State = EntityState.Modified;
            int rs = await con.SaveChangesAsync();
            return rs > 0 ? true : false;
        }
        public async Task<bool> delete(int id)
        {
            editoriales editorial = await con.editoriales.FirstAsync(f => f.id == id);
            con.editoriales.Remove(editorial);
            int rs = await con.SaveChangesAsync();
            return rs > 0 ? true : false;
        }
    }
}
