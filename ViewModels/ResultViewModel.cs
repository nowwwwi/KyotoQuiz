using KyotoQuiz.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KyotoQuiz.ViewModels
{
    public class ResultViewModel : Result
    {
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
