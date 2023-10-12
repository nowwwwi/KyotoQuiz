using KyotoQuiz.Data;
using KyotoQuiz.Helpers;
using KyotoQuiz.Models;
using KyotoQuiz.ViewModels;

namespace KyotoQuiz.Services
{
    public class QuestionRecordService : IQuestionRecordService
    {
        private readonly int QuestionsNum = 4;

        private readonly KyotoQuizContext _context;

        public QuestionRecordService(KyotoQuizContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRecords(Question question, QuestionViewModel model)
        {
            var questionRecords = new List<QuestionRecord>();

            for (int i = 0; i < QuestionsNum; i++)
            {
                var record = ModelHelper.GetQuestionRecord(i + 1, question, model);
                questionRecords.Add(record);
            }

            if (!questionRecords.Any())
            {
                return false;
            }

            foreach (var record in questionRecords)
            {
                _context.Add(record);
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
