using CourseProject.Data;
using KnowledgePicker.WordCloud.Primitives;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Services
{
    public class TagCloudService
    {
        private IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public TagCloudService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        
        public async Task<IEnumerable<WordCloudEntry>> CollectWordFrequencies()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            var tags = await ctx.ReviewTags.Include(t => t.Tag).ToListAsync();
            var frequencies = new Dictionary<string, int>();
            foreach (var tag in tags)
            {
                if (frequencies.ContainsKey(tag.Tag.Title))
                    frequencies[tag.Tag.Title]++;
                else frequencies[tag.Tag.Title] = 1;
            }
            return frequencies.Select(p => new WordCloudEntry(p.Key, p.Value));
        }
    }
}
