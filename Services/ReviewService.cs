using CourseProject.Data;
using CourseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Services
{
    public class ReviewService
    {
        private IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public ReviewService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void AddReview(Review review)
        {
            using (var ctx = _dbContextFactory.CreateDbContext())
            {
                ctx.Reviews.Add(review);
                ctx.SaveChanges();
            }
        }

        public async Task<List<Category>> GetCategories()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Categories.ToListAsync();
        }

        public async Task<List<Tag>> GetTags()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Tags.ToListAsync();
        }

        public async Task<List<Work>> GetWorks()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Works.ToListAsync();
        }

        public async Task<List<Review>> GetReviews()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Reviews.ToListAsync();
        }

        public async Task AddTags(List<Tag> tags)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            foreach (var t in tags)
            {
                if(!ctx.Tags.Contains(t))
                    await ctx.Tags.AddAsync(t);
            }
            await ctx.SaveChangesAsync();
        }

        public async Task AddWork(Work work)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            if (!ctx.Works.Contains(work))
                await ctx.Works.AddAsync(work);
            await ctx.SaveChangesAsync();
        }
    }
}
