using KyotoQuiz.Models;
using KyotoQuiz.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace KyotoQuiz.Helpers
{
    public static class ModelHelper
    {
        public static Question GetQuestionFromViewModel(QuestionViewModel model)
        {
            return new Question
            {
                Id = model.Id,
                ImplementedId = model.ImplementedId,
                GenreId = model.GenreId,
                Grade = model.Grade,
                Number = model.Number,
                Content = model.Content,
                Description = model.Description,
                Implemented = model.Implemented,
                Genre = model.Genre
            };
        }

        public static void UpdateQuestion(Question oldQuestion, Question newQuestion)
        {
            if (oldQuestion != null && newQuestion != null)
            {
                oldQuestion.ImplementedId = newQuestion.ImplementedId;
                oldQuestion.GenreId = newQuestion.GenreId;
                oldQuestion.Grade = newQuestion.Grade;
                oldQuestion.Number = newQuestion.Number;
                oldQuestion.Content = newQuestion.Content;
                oldQuestion.Description = newQuestion.Description;
            }
        }

        public static QuestionRecord GetQuestionRecord(
            int order,
            Question question,
            QuestionViewModel model)
        {
            return order switch
            {
                1 => new QuestionRecord
                {
                    QuestionId = question.Id,
                    OrderOfQuestion = 1,
                    Content = model.ContentOfOrderOne,
                    IsAnswer = model.IsOrderOneAnswer,
                    Question = question
                },
                2 => new QuestionRecord
                {
                    QuestionId = question.Id,
                    OrderOfQuestion = 2,
                    Content = model.ContentOfOrderTwo,
                    IsAnswer = model.IsOrderTwoAnswer,
                    Question = question
                },
                3 => new QuestionRecord
                {
                    QuestionId = question.Id,
                    OrderOfQuestion = 3,
                    Content = model.ContentOfOrderThree,
                    IsAnswer = model.IsOrderThreeAnswer,
                    Question = question
                },
                4 => new QuestionRecord
                {
                    QuestionId = question.Id,
                    OrderOfQuestion = 4,
                    Content = model.ContentOfOrderFour,
                    IsAnswer = model.IsOrderFourAnswer,
                    Question = question
                },
                _ => new(),
            };
        }
    }
}
