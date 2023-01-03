using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class UserRating
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; } = default!;
        public int WorkId { get; set; }
        public User User { get; set; } = default!;
        public Work Work { get; set; } = default!;
    }
}
