using KyotoQuiz.Models;
using System.Diagnostics;

namespace KyotoQuiz.Helpers
{
    public static class FilterHelper
    {
        public static List<Question> GetFiltered(List<Question> questions, QuizOption option)
        {
            if (option.ImplementedId == -1 && option.Grade == -1 && option.GenreId == -1)
            {
                return questions;
            }

            if (option.ImplementedId != -1)
            {
                questions = questions.Where(q => q.ImplementedId == option.ImplementedId).ToList();
            }

            if (option.Grade != -1)
            {
                questions = questions.Where(q => q.Grade == option.Grade).ToList();
            }

            if (option.GenreId != -1)
            {
                questions = questions.Where(q => q.GenreId == option.GenreId).ToList();
            }

            return questions;
        }
    }
}
