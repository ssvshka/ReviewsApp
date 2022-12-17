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
        public virtual User Author { get; set; } = default!;
        public int UserId { get; set; }
        public virtual Work Work { get; set; } = default!;
        public int WorkId { get; set; }
        public ICollection<ReviewTag> TagsLink { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = default!;
    }
}
