using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travel.Context;
using Travel.Models.autores;
using Travel.Models.autores_has_libros;
using Travel.Models.auxiliar;
using Travel.Models.libros;
using Travel.Services;

namespace Travel.Controllers
{
    public class librosController : Controller
    {
        private readonly dbcontext _context;
        librosService service;
        editorialesService service2;
        autoresService service3;

        public librosController(dbcontext context)
        {
            _context = context;
            service = new librosService(context);
            service2 = new editorialesService(context);
            service3 = new autoresService(context);
        }

        // GET: libros
        public async Task<IActionResult> Index()
        {
            var libros = await _context.libros.ToListAsync();
            List<libroAutor> lista = new List<libroAutor>();
            foreach (var item in libros)
            {
                var aut_libros = await _context.autores_has_libros.FirstAsync(x => x.libros_ISBN == item.ISBN);
                var autor = await _context.autores.FirstAsync(x => x.id == aut_libros.autores_id);
                var editorial = await _context.editoriales.FirstAsync(x => x.id == item.editoriales_id);
                libroAutor libroAutor = new libroAutor()
                {
                    ISBN = item.ISBN,
                    editoriales_id = item.editoriales_id,
                    nombre_autor = autor.nombre + autor.apellidos,
                    titulo = item.titulo,
                    autor_id = aut_libros.autores_id,
                    sinopsis = item.sinopsis,
                    n_paginas = item.n_paginas,
                    nombre_editorial = editorial.nombre
                };
                lista.Add(libroAutor);
            }
              return View(lista);
        }

        // GET: libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.libros == null)
            {
                return NotFound();
            }

            var libros = await _context.libros.FindAsync(id);
            var aut_libros = await _context.autores_has_libros.FirstAsync(x => x.libros_ISBN == libros.ISBN);
            var autor = await _context.autores.FirstAsync(x => x.id == aut_libros.autores_id);
            var editorial = await _context.editoriales.FirstAsync(x => x.id == libros.editoriales_id);
            libroAutor libroAutor = new libroAutor()
            {
                ISBN = libros.ISBN,
                editoriales_id = libros.editoriales_id,
                titulo = libros.titulo,
                nombre_autor = autor.nombre.Trim() + " " + autor.apellidos.Trim(),
                autor_id = aut_libros.autores_id,
                sinopsis = libros.sinopsis,
                n_paginas = libros.n_paginas,
                nombre_editorial = editorial.nombre
            };
            if (libros == null)
            {
                return NotFound();
            }

            return View(libroAutor);
        }

        // GET: libros/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.editoriales = await service2.getAll();
            ViewBag.autores = await service3.getAll();
            return View();
        }

        // POST: libros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ISBN,editoriales_id,titulo,sinopsis,n_paginas,autor_id")] libroAutor libroAutor)
        {
            libros libros = new libros()
            {
                ISBN = libroAutor.ISBN,
                editoriales_id = libroAutor.editoriales_id,
                titulo = libroAutor.titulo, 
                sinopsis = libroAutor.sinopsis,
                n_paginas = libroAutor.n_paginas 
            };

            if (ModelState.IsValid)
            {
                _context.Add(libros);
                await _context.SaveChangesAsync();
                autores_has_libros aut_lib = new autores_has_libros()
                {
                    autores_id = libroAutor.autor_id,
                    libros_ISBN = libros.ISBN
                };
                _context.Add(aut_lib);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libros);
        }

        // GET: libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.libros == null)
            {
                return NotFound();
            }

            var libros = await _context.libros.FindAsync(id);
            var aut_libros = await _context.autores_has_libros.FirstAsync(x => x.libros_ISBN == libros.ISBN);
            var editorial = await _context.editoriales.FirstAsync(x => x.id == libros.editoriales_id);
            libroAutor libroAutor = new libroAutor()
            {
                ISBN = libros.ISBN,
                editoriales_id = libros.editoriales_id,
                titulo = libros.titulo,
                autor_id = aut_libros.autores_id,
                sinopsis = libros.sinopsis,
                n_paginas = libros.n_paginas,
                nombre_editorial = editorial.nombre
            };
            if (libros == null)
            {
                return NotFound();
            }
            ViewBag.editoriales = await service2.getAll();
            ViewBag.autores = await service3.getAll();
            return View(libroAutor);
        }

        // POST: libros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ISBN,editoriales_id,titulo,sinopsis,n_paginas,autor_id")] libroAutor libroAutor)
        {
            libros libros = new libros()
            {
                ISBN = libroAutor.ISBN,
                editoriales_id = libroAutor.editoriales_id,
                titulo = libroAutor.titulo,
                sinopsis = libroAutor.sinopsis,
                n_paginas = libroAutor.n_paginas
            };
            if (id != libros.ISBN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libros);
                    await _context.SaveChangesAsync();
                    autores_has_libros aut_lib = new autores_has_libros()
                    {
                        autores_id = libroAutor.autor_id,
                        libros_ISBN = libros.ISBN
                    };
                    _context.Update(aut_lib);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!librosExists(libros.ISBN))
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
            return View(libros);
        }

        // GET: libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.libros == null)
            {
                return NotFound();
            }

            var libros = await _context.libros.FindAsync(id);
            var aut_libros = await _context.autores_has_libros.FirstAsync(x => x.libros_ISBN == libros.ISBN);
            var autor = await _context.autores.FirstAsync(x => x.id == aut_libros.autores_id);
            var editorial = await _context.editoriales.FirstAsync(x => x.id == libros.editoriales_id);
            libroAutor libroAutor = new libroAutor()
            {
                ISBN = libros.ISBN,
                editoriales_id = libros.editoriales_id,
                titulo = libros.titulo,
                nombre_autor = autor.nombre.Trim() + " " + autor.apellidos.Trim(),
                autor_id = aut_libros.autores_id,
                sinopsis = libros.sinopsis,
                n_paginas = libros.n_paginas,
                nombre_editorial = editorial.nombre
            };
            if (libros == null)
            {
                return NotFound();
            }

            return View(libroAutor);
        }

        // POST: libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.libros == null)
            {
                return Problem("Entity set 'dbcontext.libros'  is null.");
            }
            var libros = await _context.libros.FindAsync(id);
            if (libros != null)
            {
                _context.libros.Remove(libros);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool librosExists(int id)
        {
          return _context.libros.Any(e => e.ISBN == id);
        }
    }
}
