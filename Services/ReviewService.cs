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

        public async Task<List<Review>> GetReviewsAsync()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Reviews.ToListAsync();
        }

        public async Task<List<Review>> GetCurrentUserReviewsAsync(string userId)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Reviews
                            .Where(u => u.UserId == userId)
                            .ToListAsync();
        }
        public async Task AddReview(Review review)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            ctx.Reviews.Add(review);
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteReview(Review review)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            ctx.Reviews.Remove(review);
            await ctx.SaveChangesAsync();   
        }

        public async Task<Review> GetReviewByIdAsync(int? id)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Reviews.SingleAsync(r => r.Id == id);
        }

        public async Task<List<Work>> GetWorks()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Works.ToListAsync();
        }



        public async Task AddWork(Work work)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            await ctx.Works.AddAsync(work);
            await ctx.SaveChangesAsync();
        }

        public async Task AddTags(List<Tag> tags)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            foreach (var t in tags)
            {
                if (!ctx.Tags.Contains(t))
                    await ctx.Tags.AddAsync(t);
            }
            await ctx.SaveChangesAsync();
        }

        public async Task<List<Tag>> GetTags()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Tags.ToListAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Categories.ToListAsync();
        }
    }
}
