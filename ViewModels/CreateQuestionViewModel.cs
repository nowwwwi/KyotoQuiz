using KyotoQuiz.Models;
using System.ComponentModel;

namespace KyotoQuiz.ViewModels
{
    public class CreateQuestionViewModel : Question
    {
        [DisplayName("(ア)の回答")]
        public string ContentOfOrderOne {  get; set; }

        public bool IsOrderOneAnswer { get; set; } = false;

        [DisplayName("(イ)の回答")]
        public string ContentOfOrderTwo { get; set; }

        public bool IsOrderTwoAnswer { get; set; } = false;

        [DisplayName("(ウ)の回答")]
        public string ContentOfOrderThree { get; set; }

        public bool IsOrderThreeAnswer { get; set; } = false;

        [DisplayName("(エ)の回答")]
        public string ContentOfOrderFour { get; set; }

        public bool IsOrderFourAnswer { get; set; } = false;

        public Question ConvertToQuestion()
        {
            return new Question
            {
                Id = Id,
                ImplementedId = ImplementedId,
                GenreId = GenreId,
                Grade = Grade,
                Number = Number,
                Content = Content,
                Description = Description,
                Implemented = Implemented,
                Genre = Genre
            };
        }

        public bool ValidateAnswerCount()
        {
            var count = (IsOrderOneAnswer ? 1 : 0)
                + (IsOrderTwoAnswer ? 1 : 0)
                + (IsOrderThreeAnswer ? 1 : 0)
                + (IsOrderFourAnswer ? 1 : 0);

            return count == 1;
        }

        public QuestionRecord CreateQuestionRecord(
            int order,
            int questionId,
            Question question)
        {
            return order switch
            {
                1 => new QuestionRecord
                {
                    QuestionId = questionId,
                    OrderOfQuestion = 1,
                    Content = ContentOfOrderOne,
                    IsAnswer = IsOrderOneAnswer,
                    Question = question
                },
                2 => new QuestionRecord
                {
                    QuestionId = questionId,
                    OrderOfQuestion = 2,
                    Content = ContentOfOrderTwo,
                    IsAnswer = IsOrderTwoAnswer,
                    Question = question
                },
                3 => new QuestionRecord
                {
                    QuestionId = questionId,
                    OrderOfQuestion = 3,
                    Content = ContentOfOrderThree,
                    IsAnswer = IsOrderThreeAnswer,
                    Question = question
                },
                4 => new QuestionRecord
                {
                    QuestionId = questionId,
                    OrderOfQuestion = 4,
                    Content = ContentOfOrderFour,
                    IsAnswer = IsOrderFourAnswer,
                    Question = question
                },
                _ => new(),
            };
        }
    }
}
