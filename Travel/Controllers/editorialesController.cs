using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travel.Context;
using Travel.Models.editoriales;

namespace Travel.Controllers
{
    public class editorialesController : Controller
    {
        private readonly dbcontext _context;

        public editorialesController(dbcontext context)
        {
            _context = context;
        }

        // GET: editoriales
        public async Task<IActionResult> Index()
        {
              return View(await _context.editoriales.ToListAsync());
        }

        // GET: editoriales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.editoriales == null)
            {
                return NotFound();
            }

            var editoriales = await _context.editoriales
                .FirstOrDefaultAsync(m => m.id == id);
            if (editoriales == null)
            {
                return NotFound();
            }

            return View(editoriales);
        }

        // GET: editoriales/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,sede")] editoriales editoriales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editoriales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editoriales);
        }

        // GET: editoriales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.editoriales == null)
            {
                return NotFound();
            }

            var editoriales = await _context.editoriales.FindAsync(id);
            if (editoriales == null)
            {
                return NotFound();
            }
            return View(editoriales);
        }

        // POST: editoriales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,sede")] editoriales editoriales)
        {
            if (id != editoriales.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editoriales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!editorialesExists(editoriales.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(editoriales);
        }

        // GET: editoriales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.editoriales == null)
            {
                return NotFound();
            }

            var editoriales = await _context.editoriales
                .FirstOrDefaultAsync(m => m.id == id);
            if (editoriales == null)
            {
                return NotFound();
            }

            return View(editoriales);
        }

        // POST: editoriales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.editoriales == null)
            {
                return Problem("Entity set 'dbcontext.editoriales'  is null.");
            }
            var editoriales = await _context.editoriales.FindAsync(id);
            if (editoriales != null)
            {
                _context.editoriales.Remove(editoriales);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool editorialesExists(int id)
        {
          return _context.editoriales.Any(e => e.id == id);
        }
    }
}
