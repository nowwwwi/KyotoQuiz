using KyotoQuiz.Models;
using KyotoQuiz.ViewModels;

namespace KyotoQuiz.Services
{
    public interface IQuestionRecordService
    {
        public Task<bool> SaveRecords(Question question, QuestionViewModel model);
    }
}
