using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class UserRating
    {
        public int Rating { get; set; }
        public string UserId { get; set; } = default!;
        public int WorkId { get; set; }
    }
}
