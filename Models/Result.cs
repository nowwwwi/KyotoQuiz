using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KyotoQuiz.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Question")]
        public int QuestionId {  get; set; }

        public int SelectedOrder {  get; set; }

        public bool IsCorrect {  get; set; }

        public DateTime Created {  get; set; }

        public Question Question { get; set; }
    }
}
