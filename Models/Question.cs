using KyotoQuiz.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace KyotoQuiz.Models
{
    public class Question
    {
        public int Id { get; set; }

        [ForeignKey("Implemented")]
        public int ImplementedId {  get; set; }

        [ForeignKey("Genre")]
        public int GenreId {  get; set; }

        public int Grade {  get; set; }

        public int Number {  get; set; }

        [Required]
        public string Content { get; set; }

        public string? Description {  get; set; }

        public Implemented Implemented { get; set; }

        public Genre Genre { get; set; }

        public CreateQuestionViewModel ConvertToViewModel(List<QuestionRecord> records)
        {
            return new()
            {
                Id = Id,
                ImplementedId = ImplementedId,
                GenreId = GenreId,
                Grade = Grade,
                Number = Number,
                Content = Content,
                Description = Description,
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
    }
}
