using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KyotoQuiz.Data;
using KyotoQuiz.Models;
using KyotoQuiz.ViewModels;

namespace KyotoQuiz.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly KyotoQuizContext _context;
        private readonly List<int> Grades = new() { 1, 2, 3 };
        private const int QuestionsNum = 4;
        private const string BindProperties = "Id,ImplementedId,GenreId,Grade,Number,Content,ContentOfOrderOne,IsOrderOneAnswer,ContentOfOrderTwo,IsOrderTwoAnswer,ContentOfOrderThree,IsOrderThreeAnswer,ContentOfOrderFour,IsOrderFourAnswer,Description";

        public QuestionsController(KyotoQuizContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var kyotoQuizContext = _context.Question.Include(q => q.Genre).Include(q => q.Implemented);
            return View(await kyotoQuizContext.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Question == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .Include(q => q.Genre)
                .Include(q => q.Implemented)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Content");
            ViewData["ImplementedId"] = new SelectList(_context.Implemented, "Id", "Times");
            ViewData["Grade"] = new SelectList(Grades);

            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(BindProperties)] CreateQuestionViewModel model)
        {
            ModelState.Clear();

            model = await AssignValuesAsync(model);

            if (ModelState.IsValid)
            {
                var question = model.ConvertToQuestion();
                _context.Add(question);
                _context.SaveChanges();

                var questionId = _context.Question.FirstOrDefault(q => q.Content == model.Content).Id;

                for (int i=0; i< QuestionsNum; i++)
                {
                    var record = model.CreateQuestionRecord(i+1, questionId, question);
                    _context.Add(record);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Content", model.GenreId);
            ViewData["ImplementedId"] = new SelectList(_context.Implemented, "Id", "Times", model.ImplementedId);

            return View();
        }

        private async Task<CreateQuestionViewModel> AssignValuesAsync(CreateQuestionViewModel model)
        {
            model.Implemented = await _context.Implemented.FirstOrDefaultAsync(i => i.Id == model.ImplementedId);
            model.Genre = await _context.Genre.FirstOrDefaultAsync(g => g.Id == model.GenreId);
            model.Description ??= string.Empty;

            return model;
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Question == null)
            {
                return NotFound();
            }

            var question = await _context.Question.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", question.GenreId);
            ViewData["ImplementedId"] = new SelectList(_context.Implemented, "Id", "Id", question.ImplementedId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImplementedId,GenreId,Grade,Content,Description")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", question.GenreId);
            ViewData["ImplementedId"] = new SelectList(_context.Implemented, "Id", "Id", question.ImplementedId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Question == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .Include(q => q.Genre)
                .Include(q => q.Implemented)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Question == null)
            {
                return Problem("Entity set 'KyotoQuizContext.Question'  is null.");
            }
            var question = await _context.Question.FindAsync(id);
            if (question != null)
            {
                _context.Question.Remove(question);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
          return (_context.Question?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
