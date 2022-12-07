using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models
{
    public class User 
    {
        public int UserId { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Rating { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
