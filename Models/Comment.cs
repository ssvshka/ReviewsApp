using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Text { get; set; } = default!;
        public DateTime LeftOn { get; set; } = default;
        public User User { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public Review Review { get; set; } = default!;
        public int ReviewId { get; set; }
    }
}
