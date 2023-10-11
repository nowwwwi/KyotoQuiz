﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KyotoQuiz.Data;
using KyotoQuiz.Models;
using KyotoQuiz.ViewModels;
using KyotoQuiz.Enum;

namespace KyotoQuiz.Controllers
{
    public class ResultsController : Controller
    {
        private readonly KyotoQuizContext _context;

        private const string BindProperties = "Id,QuestionId,SelectedOrder,IsCorrect,Created,ImplementedId,GenreId,Grade,Number,Content," +
            "Description,ContentOfOrderOne,IsOrderOneAnswer,ContentOfOrderTwo,IsOrderTwoAnswer,ContentOfOrderThree,IsOrderThreeAnswer," +
            "ContentOfOrderFour,IsOrderFourAnswer";

        public ResultsController(KyotoQuizContext context)
        {
            ViewData["Status"] = AnswerStatus.Unanswered;
            ViewData["ShowDetail"] = false;
            _context = context;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var kyotoQuizContext = _context.Result.Include(r => r.Question);
            return View(await kyotoQuizContext.ToListAsync());
        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Result == null)
            {
                return NotFound();
            }

            var result = await _context.Result
                .Include(r => r.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Results/Create
        public async Task<IActionResult> Create(int? id)
        {

            var question = await _context.Question
                .Include(q => q.Genre)
                .Include(q => q.Implemented)
                .FirstOrDefaultAsync(m => m.Id == id);

            var records = await _context.QuestionRecord.Where(q => q.QuestionId == id).ToListAsync();

            if (question == null || !records.Any())
            {
                return NotFound();
            }

            var viewModel = new ResultViewModel(question, records);

            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Content");

            return View(viewModel);
        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            int? id,
            [Bind("QuestionId,SelectedOrder,IsCorrect,Created,Question")] Result result)
        {
            ModelState.Clear();

            var correctOrder = await _context.QuestionRecord.FirstOrDefaultAsync(q => q.QuestionId == id && q.IsAnswer);

            if (correctOrder == null)
            {
                return NotFound();
            }

            result.Question = await _context.Question.FirstOrDefaultAsync(q => q.Id == id);
            result.IsCorrect = result.SelectedOrder == correctOrder.OrderOfQuestion;

            if (ModelState.IsValid)
            {
                ViewData["Status"] = result.IsCorrect ? AnswerStatus.Correct : AnswerStatus.Incorrect;
                _context.Add(result);
                await _context.SaveChangesAsync();
                return await Create(id);
            }

            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", result.QuestionId);
            return View(result);
        }



        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Result == null)
            {
                return NotFound();
            }

            var result = await _context.Result.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", result.QuestionId);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuestionId,SelectedOrder,IsCorrect,Created")] Result result)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.Id))
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
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", result.QuestionId);
            return View(result);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Result == null)
            {
                return NotFound();
            }

            var result = await _context.Result
                .Include(r => r.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Result == null)
            {
                return Problem("Entity set 'KyotoQuizContext.Result'  is null.");
            }
            var result = await _context.Result.FindAsync(id);
            if (result != null)
            {
                _context.Result.Remove(result);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(int id)
        {
          return (_context.Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
