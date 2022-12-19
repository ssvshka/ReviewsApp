using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Title { get; set; } = default!;
        [Required]
        [MaxLength(4096)]
        public string Text { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public DateTime PostedOn { get; set; } = default;
        [ForeignKey("User")]
        [MaxLength(450)]
        public string UserId { get; set; } = default!;
        public User User { get; set; } = default!;
        public int WorkId { get; set; }
        public Work Work { get; set; } = default!;
        public ICollection<ReviewTag> TagsLink { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = default!;
    }
}
