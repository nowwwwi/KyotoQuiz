using KyotoQuiz.Models;
using System.ComponentModel;

namespace KyotoQuiz.ViewModels
{
    public class QuestionViewModel : Question
    {
        [DisplayName("(ア)")]
        public string ContentOfOrderOne {  get; set; }

        public bool IsOrderOneAnswer { get; set; } = false;

        [DisplayName("(イ)")]
        public string ContentOfOrderTwo { get; set; }

        public bool IsOrderTwoAnswer { get; set; } = false;

        [DisplayName("(ウ)")]
        public string ContentOfOrderThree { get; set; }

        public bool IsOrderThreeAnswer { get; set; } = false;

        [DisplayName("(エ)")]
        public string ContentOfOrderFour { get; set; }

        public bool IsOrderFourAnswer { get; set; } = false;
    }
}
