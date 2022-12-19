using CourseProject.Data;
using CourseProject.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Services
{
    public class UserService
    {
        private IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private AuthenticationStateProvider _authStateProvider;

        public UserService(IDbContextFactory<ApplicationDbContext> dbContextFactory, AuthenticationStateProvider authStateProvider)
        {
            _dbContextFactory = dbContextFactory;
            _authStateProvider = authStateProvider;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Users.ToListAsync();
        }

        public async Task<User> GetCurrentUserAsync()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return await ctx.Users
                .Include(x => x.UserName)
                .SingleAsync(u => u.UserName == user.Identity!.Name);
        }
        public async Task<string> GetCurrentUserId()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            var user = (await _authStateProvider.GetAuthenticationStateAsync()).User;
            return user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value!;
        }
    }
}
