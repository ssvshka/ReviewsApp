using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models
{
    public class ReviewSubject
    {
        public int ReviewSubjectId { get; set; }
        [Required]
        [StringLength(128)]
        public string Title { get; set; } = default!;
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Rating { get; set; }
        public virtual Group Group { get; set; } = default!;
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
