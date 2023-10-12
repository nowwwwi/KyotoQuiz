using KyotoQuiz.Data;
using KyotoQuiz.Helpers;
using KyotoQuiz.Models;
using KyotoQuiz.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KyotoQuiz.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly KyotoQuizContext _context;

        public QuestionService(KyotoQuizContext context)
        {
            _context = context;
        }

        public void SaveQuestion(QuestionViewModel model)
        {
            var question = ModelHelper.GetQuestionFromViewModel(model);
            _context.Add(question);
            _context.SaveChanges();
        }

        public async Task<QuestionViewModel> AssignmentViewModel(QuestionViewModel model)
        {
            model.Implemented = await _context.Implemented.FirstOrDefaultAsync(i => i.Id == model.ImplementedId);
            model.Genre = await _context.Genre.FirstOrDefaultAsync(g => g.Id == model.GenreId);
            model.Description ??= string.Empty;

            return model;
        }
    }
}
