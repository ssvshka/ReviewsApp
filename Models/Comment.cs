using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Text { get; set; } = default!;
        public DateTime LeftOn { get; set; } = default;
        public virtual User User { get; set; } = default!;
        public virtual Review? Review { get; set; } = default!;
        public int ReviewId { get; set; }
    }
}
