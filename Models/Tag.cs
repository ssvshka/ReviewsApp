﻿using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Title { get; set; } = default!;
        public ICollection<ReviewTag> ReviewsLink { get; set; } = default!;

        public override string ToString() => Title;
    }
}
