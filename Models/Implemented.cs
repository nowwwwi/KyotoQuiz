using System.ComponentModel.DataAnnotations;

namespace KyotoQuiz.Models
{
    public class Implemented
    {
        [Key]
        public int Id { get; set; }

        public int Year {  get; set; }

        public int Times {  get; set; }
    }
}
