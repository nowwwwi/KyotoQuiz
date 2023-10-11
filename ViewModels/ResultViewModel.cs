using KyotoQuiz.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KyotoQuiz.ViewModels
{
    public class ResultViewModel : Result
    {
        public ResultViewModel()
        {
            
        }

        public ResultViewModel(Question question, List<QuestionRecord> records)
        {
            QuestionId = question.Id;
            ImplementedId = question.ImplementedId;
            GenreId = question.GenreId;
            Grade = question.Grade;
            Number = question.Number;
            Content = question.Content;
            Description = Description;
            ContentOfOrderOne = records.FirstOrDefault(r => r.OrderOfQuestion == 1).Content;
            IsOrderOneAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 1).IsAnswer;
            ContentOfOrderTwo = records.FirstOrDefault(r => r.OrderOfQuestion == 2).Content;
            IsOrderTwoAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 2).IsAnswer;
            ContentOfOrderThree = records.FirstOrDefault(r => r.OrderOfQuestion == 3).Content;
            IsOrderThreeAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 3).IsAnswer;
            ContentOfOrderFour = records.FirstOrDefault(r => r.OrderOfQuestion == 4).Content;
            IsOrderFourAnswer = records.FirstOrDefault(r => r.OrderOfQuestion == 4).IsAnswer;
            Question = question;
            Implemented = question.Implemented;
            Genre = question.Genre;
        }

        public int ImplementedId { get; set; }

        public int GenreId { get; set; }

        public int Grade { get; set; }

        public int Number { get; set; }

        public string Content { get; set; }

        public string? Description { get; set; }

        [DisplayName("(ア)")]
        public string ContentOfOrderOne { get; set; }

        public bool IsOrderOneAnswer { get; set; }

        [DisplayName("(イ)")]
        public string ContentOfOrderTwo { get; set; }

        public bool IsOrderTwoAnswer { get; set; }

        [DisplayName("(ウ)")]
        public string ContentOfOrderThree { get; set; }

        public bool IsOrderThreeAnswer { get; set; }

        [DisplayName("(エ)")]
        public string ContentOfOrderFour { get; set; }

        public bool IsOrderFourAnswer { get; set; }

        public Implemented Implemented { get; set; }

        public Genre Genre { get; set; }
    }
}
