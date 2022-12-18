using CourseProject.Data;
using CourseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Services
{
    public class UserService
    {
        private IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public UserService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Users.ToListAsync();
        }
    }
}
