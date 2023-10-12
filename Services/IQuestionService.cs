using KyotoQuiz.Models;
using KyotoQuiz.ViewModels;

namespace KyotoQuiz.Services
{
    public interface IQuestionService
    {
        public void SaveQuestion(QuestionViewModel model);

        public Task<QuestionViewModel> AssignmentViewModel(QuestionViewModel model);
    }
}
