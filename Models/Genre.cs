using System.ComponentModel.DataAnnotations;

namespace KyotoQuiz.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        public string Content {  get; set; }
    }
}
