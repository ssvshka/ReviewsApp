using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Title { get; set; } = default!;
        public virtual ICollection<Work>? Works { get; set; }

        public override string ToString() => Title;
    }
}
