namespace CourseProject.Models
{
    public class ReviewTag
    {
        public int ReviewId { get; set; }
        public int TagId { get; set; }
        public Review Review { get; set; } = default!;
        public Tag Tag { get; set; } = default!;
    }
}
