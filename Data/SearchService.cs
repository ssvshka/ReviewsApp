using CourseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Data
{
    public class SearchService
    {
        private IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public SearchService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Review>> Search(string text)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Reviews
                .Where(r => EF.Functions.FreeText(r.Text, text))
                .ToListAsync();
        }

        public async Task/*<IEnumerable<Review>>*/ Search1(string text)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            var c = await ctx.Comments
                .Where(x => EF.Functions.FreeText(x.Text, text))
                .Select(x => x.ReviewId)
                .ToListAsync();
            //foreach (var id in c)
            //{
            //    var r = await ctx.Reviews
            //    .Where(x => x.Id == id)
            //    .
            //}
        }

        public async Task<List<Review>> GetReviewsAsync()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Reviews
                .Include(r => r.Comments)
                .ToListAsync();
        }
    }
}
