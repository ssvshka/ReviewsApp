using CourseProject.Data;
using CourseProject.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace CourseProject.Services
{
    public class SearchService
    {
        private IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private List<Review> reviews;

        public SearchService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            reviews = new List<Review>();
        }

        public async Task<List<Review>> Search(string text)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            reviews = new();
            var foundReviews = await ctx.Reviews
                .Where(r => EF.Functions.FreeText(r.Text, text))
                .ToListAsync();
            reviews.AddRange(foundReviews.Except(reviews));
            var comments = await ctx.Comments
                .Where(c => EF.Functions.FreeText(c.Text, text))
                .Select(c => c.Review)
                .ToListAsync();
            reviews.AddRange(comments.Except(reviews));
            var tags = await ctx.Tags
                .Where(t => EF.Functions.FreeText(t.Title, text))
                .Select(t => t.ReviewsLink.Select(r => r.Review))
                .FirstOrDefaultAsync();
            if (tags is not null)
                reviews.AddRange(tags.Except(reviews));
            return reviews;
        }
    }
}
