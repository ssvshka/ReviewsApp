﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models
{
    public class User : IdentityUser
    {
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<UserRating>? UserRatings { get; set; }
    }
}
