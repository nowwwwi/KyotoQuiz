using KyotoQuiz.Models;
using KyotoQuiz.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace KyotoQuiz.Helpers
{
    public static class ViewModelHelper
    {
        public static QuestionViewModel GetQuestionViewModel(
            Question question, 
            List<QuestionRecord> records)
        {
            if (records.Any())
            {
                return new()
                {
                    Id = question.Id,
                    ImplementedId = question.ImplementedId,
                    GenreId = question.GenreId,
                    Grade = question.Grade,
                    Number = question.Number,
                    Content = question.Content,
                    Description = question.Description,
                    ContentOfOrderOne = records.FirstOrDefault(r => r.OrderOfQuestion == 1).Content,
                    IsOrderOneAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 1).IsAnswer,
                    ContentOfOrderTwo = records.FirstOrDefault(r => r.OrderOfQuestion == 2).Content,
                    IsOrderTwoAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 2).IsAnswer,
                    ContentOfOrderThree = records.FirstOrDefault(r => r.OrderOfQuestion == 3).Content,
                    IsOrderThreeAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 3).IsAnswer,
                    ContentOfOrderFour = records.FirstOrDefault(r => r.OrderOfQuestion == 4).Content,
                    IsOrderFourAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 4).IsAnswer,
                };
            }

            return new();
        }

        public static ResultViewModel GetResultViewModel(
            Question question, 
            List<QuestionRecord> records)
        {
            return new ResultViewModel()
            {
                QuestionId = question.Id,
                ImplementedId = question.ImplementedId,
                GenreId = question.GenreId,
                Grade = question.Grade,
                Number = question.Number,
                Content = question.Content,
                Description = question.Description,
                ContentOfOrderOne = records.FirstOrDefault(r => r.OrderOfQuestion == 1).Content,
                IsOrderOneAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 1).IsAnswer,
                ContentOfOrderTwo = records.FirstOrDefault(r => r.OrderOfQuestion == 2).Content,
                IsOrderTwoAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 2).IsAnswer,
                ContentOfOrderThree = records.FirstOrDefault(r => r.OrderOfQuestion == 3).Content,
                IsOrderThreeAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 3).IsAnswer,
                ContentOfOrderFour = records.FirstOrDefault(r => r.OrderOfQuestion == 4).Content,
                IsOrderFourAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 4).IsAnswer,
                Question = question,
                Implemented = question.Implemented,
                Genre = question.Genre,
            };
        }
    }
}
