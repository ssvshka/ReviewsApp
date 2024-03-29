﻿namespace CourseProject.Models
{
    public class Like
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public int ReviewId { get; set; }
        public string LikedUserId { get; set; } = default!;
        public User User { get; set; } = default!;
        public Review Review { get; set; } = default!;
    }
}
