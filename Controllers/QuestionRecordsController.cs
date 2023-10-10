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
    public class QuestionRecordsController : Controller
    {
        private readonly KyotoQuizContext _context;

        public QuestionRecordsController(KyotoQuizContext context)
        {
            _context = context;
        }

        // GET: QuestionRecords
        public async Task<IActionResult> Index()
        {
            var kyotoQuizContext = _context.QuestionRecord.Include(q => q.Question);
            return View(await kyotoQuizContext.ToListAsync());
        }

        // GET: QuestionRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.QuestionRecord == null)
            {
                return NotFound();
            }

            var questionRecord = await _context.QuestionRecord
                .Include(q => q.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionRecord == null)
            {
                return NotFound();
            }

            return View(questionRecord);
        }

        // GET: QuestionRecords/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id");
            return View();
        }

        // POST: QuestionRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuestionId,OrderOfQuestion,Content,IsAnswer")] QuestionRecord questionRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", questionRecord.QuestionId);
            return View(questionRecord);
        }

        // GET: QuestionRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.QuestionRecord == null)
            {
                return NotFound();
            }

            var questionRecord = await _context.QuestionRecord.FindAsync(id);
            if (questionRecord == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", questionRecord.QuestionId);
            return View(questionRecord);
        }

        // POST: QuestionRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuestionId,OrderOfQuestion,Content,IsAnswer")] QuestionRecord questionRecord)
        {
            if (id != questionRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionRecordExists(questionRecord.Id))
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
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", questionRecord.QuestionId);
            return View(questionRecord);
        }

        // GET: QuestionRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.QuestionRecord == null)
            {
                return NotFound();
            }

            var questionRecord = await _context.QuestionRecord
                .Include(q => q.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionRecord == null)
            {
                return NotFound();
            }

            return View(questionRecord);
        }

        // POST: QuestionRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.QuestionRecord == null)
            {
                return Problem("Entity set 'KyotoQuizContext.QuestionRecord'  is null.");
            }
            var questionRecord = await _context.QuestionRecord.FindAsync(id);
            if (questionRecord != null)
            {
                _context.QuestionRecord.Remove(questionRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionRecordExists(int id)
        {
          return (_context.QuestionRecord?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
