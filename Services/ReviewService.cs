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

        public async Task<List<Review>> GetReviewsAsync(string userId = "")
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            var reviews = await ctx.Reviews
                .Include(r => r.Work)
                .ThenInclude(w => w.Category)
                .Include(r => r.User)
                .Include(r => r.Comments)
                .Include(r => r.TagsLink)
                .ThenInclude(t => t.Tag)
                .OrderByDescending(r => r.PostedOn)
                .ToListAsync();
            if (!string.IsNullOrEmpty(userId))
                reviews = reviews.Where(r => r.UserId == userId).ToList();
            return reviews;
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

        public async Task<List<Work>> GetWorksByCategory(Category category)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Works
                .Where(w => w.Category == category)
                .OrderBy(w => w.Title)
                .ToListAsync();
        }

        public async Task<List<Work>> GetAllWorks()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Works
                .OrderBy(w => w.Title)
                .ToListAsync();
        }
        public async Task DeleteWork(Work work)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            ctx.Works.Remove(work);
            await ctx.SaveChangesAsync();
        }


        public async Task AddWork(Work work)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            await ctx.Works.AddAsync(work);
            await ctx.SaveChangesAsync();
        }

        public async Task<bool> FindWorkByTitle(string title)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Works
                .Where(w => w.Title == title)
                .AnyAsync();
        }

        public async Task<Work> GetWorkByTitle(string title)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Works
                .Where(w => w.Title == title)
                .Include(w => w.Category)
                .SingleAsync();
        }

        public async Task CalculateAuthorRating(int workId, int rating)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            var c = GetReviewsNumber(workId, ctx);
            ctx.Works
                .Single(w => w.Id == workId)
                .OverallAuthorRating += rating / (c + 1);
            await ctx.SaveChangesAsync();
        }

        public async Task CalculateUserRating(int workId, int rating)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            
            var g = ctx.Works
                .Single(w => w.Id == workId)
                .GradeAmount;
            ctx.Works
                .Single(w => w.Id == workId)
                .OverallUserRating += rating / (g + 1);
            await ctx.SaveChangesAsync();
        }

        private static int GetReviewsNumber(int workId, ApplicationDbContext ctx)
            => ctx.Reviews
                .Where(w => w.WorkId == workId)
                .Count();

        public async Task AddTag(Tag tag)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            if (!ctx.Tags.Contains(tag))
                await ctx.Tags.AddAsync(tag);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<Tag>> GetTags()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Tags
                .OrderBy(w => w.Title)
                .ToListAsync();
        }

        public async Task<List<Tag>> GetTagsByValue(string value)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Tags
                .OrderBy(w => w.Title)
                .Where(x => x.Title.Contains(value))
                .ToListAsync();
        }

        public async Task<bool> FindTagByTitle(string title)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Tags
                .AnyAsync(x => x.Title == title);
        }

        public async Task<Tag> GetTagByTitle(string title)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Tags
                .SingleAsync(x => x.Title == title);
        }

        public Tag GetTagById(int tagId)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return ctx.Tags
                .Single(x => x.Id == tagId);
        }

        public async Task<List<Category>> GetCategories()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Categories
                .OrderBy(w => w.Title)
                .ToListAsync();
        }
        
        //Consider delete
        public async Task<Category> GetCategoryById(int catId)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Categories
                .SingleAsync(w => w.Id == catId);
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

        public async Task<List<ReviewTag>> GetReviewTags(int reviewId)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.ReviewTags
                .Where(r => r.ReviewId == reviewId)
                .ToListAsync();
        }

        public async Task AddTagToReview(ReviewTag reviewTag)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            if (!ctx.ReviewTags.Contains(reviewTag))
                await ctx.ReviewTags.AddAsync(reviewTag);
            await ctx.SaveChangesAsync();
        }

        public async Task RemoveTagFromReview(int reviewId, string tagTitle)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            var t = await ctx.ReviewTags
                .Where(w => w.ReviewId == reviewId)
                .SingleAsync(w => w.Tag.Title == tagTitle);
            ctx.ReviewTags.Remove(t!);
            await ctx.SaveChangesAsync();
        }

        /// <summary>
        /// These are quite useless methods
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task DeleteTag(string title)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            var t = await ctx.Tags
                .Where(w => w.Title == title)
                .FirstOrDefaultAsync();
            ctx.Tags.Remove(t!);
            await ctx.SaveChangesAsync();
        }
    }
}
