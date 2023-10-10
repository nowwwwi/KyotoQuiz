using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KyotoQuiz.Models
{
    public class QuestionRecord
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Question")]
        public int QuestionId {  get; set; }

        public int OrderOfQuestion {  get; set; }

        public string Content {  get; set; }

        public bool IsAnswer {  get; set; }

        public Question Question { get; set; }
    }
}
