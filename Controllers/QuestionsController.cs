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
using System.Runtime.InteropServices;

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
            var questions = await kyotoQuizContext.ToListAsync();
            var viewModels = new List<CreateQuestionViewModel>();

            foreach(var question in questions)
            {
                var records = await _context.QuestionRecord.Where(q => q.QuestionId == question.Id).ToListAsync();
                var viewModel = question.ConvertToViewModel(records);
                await AssignValuesAsync(viewModel);
                viewModels.Add(viewModel);
            }

            return View(viewModels);
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
            ReadyForCreateView(false);
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

            if (!model.ValidateAnswerCount())
            {
                ReadyForCreateView(true);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var question = model.ConvertToQuestion();
                _context.Add(question);
                _context.SaveChanges();

                var questionId = _context.Question.FirstOrDefault(q => q.Content == model.Content).Id;

                var questionRecords = new List<QuestionRecord>();

                for (int i=0; i< QuestionsNum; i++)
                {
                    var record = model.CreateQuestionRecord(i+1, questionId, question);
                    questionRecords.Add(record);
                }

                foreach(var record in questionRecords)
                {
                    _context.Add(record);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ReadyForCreateView(false, model);
            return View();
        }

        // GET: Results/CreateFromCsv
        public IActionResult CreateFromCsv()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFromCsv(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                ViewData["UploadSuccess"] = false;
                return View();
            }

            var viewModels = new List<CreateQuestionViewModel>();

            using (var streamReader = new StreamReader(csvFile.OpenReadStream()))
            {
                var csvContent = await streamReader.ReadToEndAsync();
                var lines = csvContent.Split('\n');

                foreach (var line in lines)
                {
                    var columns = line.Split(',');
                    var viewModel = new CreateQuestionViewModel()
                    {

                    };

                    viewModels.Add(viewModel);
                }
            }

            return View();
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Question == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .Include(q => q.Genre)
                .Include(q => q.Implemented)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            var records = await _context.QuestionRecord.Where(q => q.QuestionId == id).ToListAsync();
            if (!records.Any())
            {
                return NotFound();
            }

            var viewModel = question.ConvertToViewModel(records);
            ReadyForCreateView(false);
            return View(viewModel);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, 
            [Bind(BindProperties)] CreateQuestionViewModel model)
        {
            ModelState.Clear();

            if (id != model.Id)
            {
                return NotFound();
            }

            model = await AssignValuesAsync(model);

            if (!model.ValidateAnswerCount())
            {
                ReadyForCreateView(true);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldQuestion = _context.Question.FirstOrDefault(q => q.Id == id);
                    var newQuestion = model.ConvertToQuestion();

                    if (oldQuestion != null && newQuestion != null)
                    {
                        oldQuestion.ImplementedId = newQuestion.ImplementedId;
                        oldQuestion.GenreId = newQuestion.GenreId;
                        oldQuestion.Grade = newQuestion.Grade;
                        oldQuestion.Number = newQuestion.Number;
                        oldQuestion.Content = newQuestion.Content;
                        oldQuestion.Description = newQuestion.Description;
                    }

                    _context.Update(oldQuestion);
                    _context.SaveChanges();

                    var oldQuestionRecords = _context.QuestionRecord.Where(q => q.QuestionId == id);
                    var newQuestionRecords = new List<QuestionRecord>();

                    for (int i = 0; i < QuestionsNum; i++)
                    {
                        var record = model.CreateQuestionRecord(i + 1, id, oldQuestion);
                        newQuestionRecords.Add(record);
                    }

                    foreach (var record in oldQuestionRecords)
                    {
                        var correspondingNewRecord = newQuestionRecords
                            .FirstOrDefault(q => q.OrderOfQuestion == record.OrderOfQuestion);

                        if (correspondingNewRecord != null)
                        {
                            record.Content = correspondingNewRecord.Content;
                            record.IsAnswer = correspondingNewRecord.IsAnswer;
                        }

                        _context.Update(record);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(model.Id))
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

            ReadyForCreateView(false, model);
            return View(model);
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

        private async Task<CreateQuestionViewModel> AssignValuesAsync(CreateQuestionViewModel model)
        {
            model.Implemented = await _context.Implemented.FirstOrDefaultAsync(i => i.Id == model.ImplementedId);
            model.Genre = await _context.Genre.FirstOrDefaultAsync(g => g.Id == model.GenreId);
            model.Description ??= string.Empty;

            return model;
        }

        private void ReadyForCreateView(
            bool haveSomeAnswer, 
            CreateQuestionViewModel? model = null)
        {
            if (model == null)
            {
                ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Content");
                ViewData["ImplementedId"] = new SelectList(_context.Implemented, "Id", "Times");
                ViewData["Grade"] = new SelectList(Grades);
                ViewData["HaveSomeAnswer"] = haveSomeAnswer;

                return;
            }

            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Content", model.GenreId);
            ViewData["ImplementedId"] = new SelectList(_context.Implemented, "Id", "Times", model.ImplementedId);
            ViewData["Grade"] = new SelectList(Grades);
            ViewData["HaveSomeAnswer"] = haveSomeAnswer;
        }
    }
}
