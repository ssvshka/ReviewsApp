using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

    }
}
