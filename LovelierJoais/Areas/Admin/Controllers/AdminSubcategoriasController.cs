using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LovelierJoais.Context;
using LovelierJoais.Models;

namespace LovelierJoais.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSubcategoriasController : Controller
    {
        private readonly AppDbContext _context;

        public AdminSubcategoriasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminSubcategorias
        public async Task<IActionResult> Index()
        {
              return View(await _context.Subcategorias.ToListAsync());
        }

        // GET: Admin/AdminSubcategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subcategorias == null)
            {
                return NotFound();
            }

            var subcategoria = await _context.Subcategorias
                .FirstOrDefaultAsync(m => m.SubcategoriaId == id);
            if (subcategoria == null)
            {
                return NotFound();
            }

            return View(subcategoria);
        }

        // GET: Admin/AdminSubcategorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminSubcategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubcategoriaId,SubcategoriaNome,Descricao,Link")] Subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subcategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subcategoria);
        }

        // GET: Admin/AdminSubcategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subcategorias == null)
            {
                return NotFound();
            }

            var subcategoria = await _context.Subcategorias.FindAsync(id);
            if (subcategoria == null)
            {
                return NotFound();
            }
            return View(subcategoria);
        }

        // POST: Admin/AdminSubcategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubcategoriaId,SubcategoriaNome,Descricao,Link")] Subcategoria subcategoria)
        {
            if (id != subcategoria.SubcategoriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subcategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubcategoriaExists(subcategoria.SubcategoriaId))
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
            return View(subcategoria);
        }

        // GET: Admin/AdminSubcategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subcategorias == null)
            {
                return NotFound();
            }

            var subcategoria = await _context.Subcategorias
                .FirstOrDefaultAsync(m => m.SubcategoriaId == id);
            if (subcategoria == null)
            {
                return NotFound();
            }

            return View(subcategoria);
        }

        // POST: Admin/AdminSubcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subcategorias == null)
            {
                return Problem("Entity set 'AppDbContext.Subcategorias'  is null.");
            }
            var subcategoria = await _context.Subcategorias.FindAsync(id);
            if (subcategoria != null)
            {
                _context.Subcategorias.Remove(subcategoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubcategoriaExists(int id)
        {
          return _context.Subcategorias.Any(e => e.SubcategoriaId == id);
        }
    }
}
