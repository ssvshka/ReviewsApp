using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        [Required]
        [StringLength(128)]
        public string Title { get; set; } = default!;
        [Required]
        [StringLength(4096)]
        public string Text { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public virtual ReviewSubject Subject { get; set; } = default!;
        public virtual User Author { get; set; } = default!;
        public virtual ICollection<Tag> Tags { get; set; } = default!;

        public int ReviewSubjectId { get; set; }
        public int UserId { get; set; }
    }
}
