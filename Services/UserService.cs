using CourseProject.Data;
using CourseProject.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System;

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

        private static async Task<Like> GetLike(string userId, int reviewId, ApplicationDbContext ctx)
            => await ctx.Likes
                .Where(l => l.UserId == userId)
                .SingleAsync(l => l.ReviewId == reviewId);
    }
}
