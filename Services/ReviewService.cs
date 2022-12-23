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
            return await ctx.Reviews
                .OrderByDescending(r => r.PostedOn)
                .ToListAsync();
        }

        public async Task<List<Review>> GetCurrentUserReviewsAsync(string userId)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Reviews
                            .Where(r => r.UserId == userId)
                            .OrderByDescending(r => r.PostedOn)
                            .ToListAsync();
        }
        public async Task AddReview(Review review)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            ctx.Reviews.Add(review);
            await ctx.SaveChangesAsync();
        }
        public async Task EditReview(Review review)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            ctx.Update(review);
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
            return await ctx.Reviews
                .Include(r => r.Work)
                .ThenInclude(w => w.Category)
                .Include(r => r.User)
                .Include(r => r.Comments)
                .Include(r => r.TagsLink)
                .ThenInclude(t => t.Tag)
                .SingleAsync(r => r.Id == id);
        }

        public async Task<List<Work>> GetWorks()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Works
                .OrderBy(w => w.Title)
                .ToListAsync();
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
            return await ctx.Tags
                .OrderBy(w => w.Title)
                .ToListAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Categories
                .OrderBy(w => w.Title)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetReviewComments(int reviewId)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Comments
                .Where(c => c.ReviewId == reviewId)
                .OrderBy(c => c.LeftOn) 
                .Include(c => c.User)
                .ToListAsync(); 
        }

        public async Task AddCommentAsync(Comment comment)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            await ctx.Comments.AddAsync(comment);
            await ctx.SaveChangesAsync();
        }
    }
}
