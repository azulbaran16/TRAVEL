using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travel.Context;
using Travel.Models.autores;

namespace Travel.Controllers
{
    public class autoresController : Controller
    {
        private readonly dbcontext _context;

        public autoresController(dbcontext context)
        {
            _context = context;
        }

        // GET: autores
        public async Task<IActionResult> Index()
        {
              return View(await _context.autores.ToListAsync());
        }

        // GET: autores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.autores == null)
            {
                return NotFound();
            }

            var autores = await _context.autores
                .FirstOrDefaultAsync(m => m.id == id);
            if (autores == null)
            {
                return NotFound();
            }

            return View(autores);
        }

        // GET: autores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: autores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,apellidos")] autores autores)
        {
            if (autores.apellidos == null) 
            {
                ViewBag.invalidform = true;
                return View(autores);
            };
            if (autores.nombre == null)
            {
                ViewBag.invalidform = true;
                return View(autores);
            };
            if (ModelState.IsValid)
            {
                _context.Add(autores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autores);
        }

        // GET: autores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.autores == null)
            {
                return NotFound();
            }

            var autores = await _context.autores.FindAsync(id);
            if (autores == null)
            {
                return NotFound();
            }
            return View(autores);
        }

        // POST: autores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,apellidos")] autores autores)
        {
            if (id != autores.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!autoresExists(autores.id))
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
            return View(autores);
        }

        // GET: autores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.autores == null)
            {
                return NotFound();
            }

            var autores = await _context.autores
                .FirstOrDefaultAsync(m => m.id == id);
            if (autores == null)
            {
                return NotFound();
            }

            return View(autores);
        }

        // POST: autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.autores == null)
            {
                return Problem("Entity set 'dbcontext.autores'  is null.");
            }
            var autores = await _context.autores.FindAsync(id);
            if (autores != null)
            {
                _context.autores.Remove(autores);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool autoresExists(int id)
        {
          return _context.autores.Any(e => e.id == id);
        }
    }
}
