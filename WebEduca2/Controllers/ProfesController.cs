using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebEduca2.Data;
using WebEduca2.Models;

namespace WebEduca2.Controllers
{
    public class ProfesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Profes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profe.ToListAsync());
        }

        // GET: Profes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profe = await _context.Profe
                .FirstOrDefaultAsync(m => m.ProfeID == id);
            if (profe == null)
            {
                return NotFound();
            }

            return View(profe);
        }

        // GET: Profes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfeID,Nombre,Materia,Telefono,Mail")] Profe profe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profe);
        }

        // GET: Profes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profe = await _context.Profe.FindAsync(id);
            if (profe == null)
            {
                return NotFound();
            }
            return View(profe);
        }

        // POST: Profes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfeID,Nombre,Materia,Telefono,Mail")] Profe profe)
        {
            if (id != profe.ProfeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfeExists(profe.ProfeID))
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
            return View(profe);
        }

        // GET: Profes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profe = await _context.Profe
                .FirstOrDefaultAsync(m => m.ProfeID == id);
            if (profe == null)
            {
                return NotFound();
            }

            return View(profe);
        }

        // POST: Profes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profe = await _context.Profe.FindAsync(id);
            _context.Profe.Remove(profe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfeExists(int id)
        {
            return _context.Profe.Any(e => e.ProfeID == id);
        }
    }
}
