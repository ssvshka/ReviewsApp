using CourseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace CourseProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ReviewTag> ReviewTags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ReviewTag>()
                 .HasKey(x => new { x.ReviewId, x.TagId });

            builder.Entity<Review>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            builder.Entity<Category>()
                .HasData(new Category { Id = 1, Title = "Album" },
                new Category { Id = 2, Title = "Book" },
                new Category { Id = 3, Title = "Movie" },
                new Category { Id = 4, Title = "Game" });
            builder.Entity<Tag>()
                .HasData(new Tag { Id = 1, Title = "Great Plot" },
                new Tag { Id = 2, Title = "Upbeat" },
                new Tag { Id = 3, Title = "Fantastic" },
                new Tag { Id = 4, Title = "Short" });
            builder.Entity<Work>()
                .HasData(new Work { Id = 1, Title = "Cars", CategoryId = 1 });
            builder.Entity<UserRating>()
                .HasNoKey();
        }
    }
}