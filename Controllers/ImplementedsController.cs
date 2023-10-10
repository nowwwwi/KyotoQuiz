using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KyotoQuiz.Data;
using KyotoQuiz.Models;

namespace KyotoQuiz.Controllers
{
    public class ImplementedsController : Controller
    {
        private readonly KyotoQuizContext _context;

        public ImplementedsController(KyotoQuizContext context)
        {
            _context = context;
        }

        // GET: Implementeds
        public async Task<IActionResult> Index()
        {
              return _context.Implemented != null ? 
                          View(await _context.Implemented.OrderBy(i => i.Year).ToListAsync()) :
                          Problem("Entity set 'KyotoQuizContext.Implemented'  is null.");
        }

        // GET: Implementeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Implemented == null)
            {
                return NotFound();
            }

            var implemented = await _context.Implemented
                .FirstOrDefaultAsync(m => m.Id == id);
            if (implemented == null)
            {
                return NotFound();
            }

            return View(implemented);
        }

        // GET: Implementeds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Implementeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,Times")] Implemented implemented)
        {
            if (ModelState.IsValid)
            {
                _context.Add(implemented);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(implemented);
        }

        // GET: Implementeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Implemented == null)
            {
                return NotFound();
            }

            var implemented = await _context.Implemented.FindAsync(id);
            if (implemented == null)
            {
                return NotFound();
            }
            return View(implemented);
        }

        // POST: Implementeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year,Times")] Implemented implemented)
        {
            if (id != implemented.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(implemented);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImplementedExists(implemented.Id))
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
            return View(implemented);
        }

        // GET: Implementeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Implemented == null)
            {
                return NotFound();
            }

            var implemented = await _context.Implemented
                .FirstOrDefaultAsync(m => m.Id == id);
            if (implemented == null)
            {
                return NotFound();
            }

            return View(implemented);
        }

        // POST: Implementeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Implemented == null)
            {
                return Problem("Entity set 'KyotoQuizContext.Implemented'  is null.");
            }
            var implemented = await _context.Implemented.FindAsync(id);
            if (implemented != null)
            {
                _context.Implemented.Remove(implemented);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImplementedExists(int id)
        {
          return (_context.Implemented?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
