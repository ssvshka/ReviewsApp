using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class Tag
    {
        [Required]
        [StringLength(128)]
        public string TagId { get; set; } = default!;
    }
}
