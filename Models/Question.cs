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

        [Required]
        public string Content { get; set; }

        public string? Description {  get; set; }

        public Implemented Implemented { get; set; }

        public Genre Genre { get; set; }
    }
}
