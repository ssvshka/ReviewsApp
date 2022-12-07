using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class Group
    {
        [Required]
        [StringLength(128)]
        public string GroupId { get; set; } = default!;
    }
}
