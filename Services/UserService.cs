using CourseProject.Data;
using CourseProject.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Services
{
    public class UserService
    {
        private IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private AuthenticationStateProvider _authStateProvider;
        private UserManager<User> _userManager;

        public UserService(IDbContextFactory<ApplicationDbContext> dbContextFactory, 
            AuthenticationStateProvider authStateProvider, UserManager<User> userManager)
        {
            _dbContextFactory = dbContextFactory;
            _authStateProvider = authStateProvider;
            _userManager = userManager;
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
                .SingleAsync(u => u.UserName == user.Identity!.Name);
        }

        public async Task<string> GetCurrentUserId()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            var user = (await _authStateProvider.GetAuthenticationStateAsync()).User;
            return user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value!;
        }

        public async Task<User> GetUserById(string userId)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Users.SingleAsync(u => u.Id == userId);
        }

        public async Task AddLike(Like like)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            await ctx.Likes.AddAsync(like);
            await ctx.SaveChangesAsync();
        }

        public async Task RemoveLike(string userId, int reviewId)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            ctx.Likes.Remove(await GetLike(userId, reviewId, ctx));
            await ctx.SaveChangesAsync();
        }

        public async Task<bool> IsReviewLiked(string userId, int reviewId)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Likes
                .Where(l => l.UserId == userId)
                .Where(l => l.ReviewId == reviewId)
                .AnyAsync();
        }

        public async Task<int> GetUserLikesAmount(string userId)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Likes.CountAsync(l => l.LikedUserId == userId);
        }

        public async Task SetRole(string userName, string role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user!, role);
        }

        public async Task RemoveRole(string userName, string role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.RemoveFromRoleAsync(user!, role);
        }

        public async Task<bool> IsInRole(User user, string role) 
        { 
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<string> GetUserRole(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user!);
            return roles.FirstOrDefault()!;
        }

        public async Task<List<string>> GetRoles()
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            return await ctx.Roles.Select(r => r.Name).ToListAsync();
        }

        public async Task BlockUser(string userId)
        {
            using var ctx = _dbContextFactory.CreateDbContext();
            var user = await ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            user!.LockoutEnd = new DateTime(2999, 01, 01);
            user!.SecurityStamp = Guid.NewGuid().ToString();
            ctx.Update(user);
            await ctx.SaveChangesAsync();
        }

        public async Task UnblockUser(User user)
        {
            await _userManager.SetLockoutEnabledAsync(user, false);
        }

        public async Task DeleteUser(User user)
        {
            await _userManager.DeleteAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
        }

        private static async Task<Like> GetLike(string userId, int reviewId, ApplicationDbContext ctx)
            => await ctx.Likes
                .Where(l => l.UserId == userId)
                .SingleAsync(l => l.ReviewId == reviewId);
    }
}
