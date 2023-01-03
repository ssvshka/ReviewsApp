using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models
{
    public class Work
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Title { get; set; } = default!;
        [Column(TypeName = "decimal(5, 2)")]
        public double OverallAuthorRating { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public double OverallUserRating { get; set; }
        public Category Category { get; set; } = default!;
        public int CategoryId { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<UserRating>? UserRatings { get; set; }

        public override string ToString() => Title;
    }
}
