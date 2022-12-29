using CourseProject.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CourseProject.Services
{
    public class ViewService
    {
        public ReviewService _reviewService;
        public UserService _userService;

        public List<Review>? Reviews { get; private set; }
        public List<Review>? CurrentUserReviews { get; private set; }
        public List<User>? Users { get; private set; }
        public List<Comment>? Comments { get; private set; }
        public List<Tag>? Tags { get; private set; }
        public List<Work>? Works { get; private set; }
        public List<Category>? Categories { get; private set; }
        public List<ReviewTag>? ReviewTags { get; private set; }

        public ViewService(ReviewService reviewService, UserService userService)
        {
            _reviewService = reviewService;
            _userService = userService;
        }

        public async Task GetReviewsAsync()
        {
            Reviews = await _reviewService.GetReviewsAsync();
            NotifyListChanged(Reviews, EventArgs.Empty);
        }

        public void OrderReviewsByRating()
        {
            Reviews = Reviews!.OrderByDescending(x => x.Work.OverallAuthorRating).ToList();
            NotifyListChanged(Reviews!, EventArgs.Empty);
        }

        public async Task GetUserReviewsAsync(string id)
        {
            CurrentUserReviews = await _reviewService.GetReviewsAsync(id);
            NotifyListChanged(CurrentUserReviews, EventArgs.Empty);
        }

        public async Task GetUsersAsync()
        {
            Users = await _userService.GetAllUsersAsync();
            NotifyListChanged(Users, EventArgs.Empty);
        }

        public async Task DeleteReviewAsync(Review review, string userId)
        {
            await _reviewService.DeleteReview(review);
            await GetUserReviewsAsync(userId);
        }

        public async Task UpdateUserRating(int workId, int rating)
        {
            await _reviewService.CalculateUserRating(workId, rating);
        }

        public async Task GetCommentsAsync(int reviewId)
        {
            Comments = await _reviewService.GetReviewComments(reviewId);
            NotifyListChanged(Comments, EventArgs.Empty);
        }

        public async Task GetCategoriesAsync()
        {
            Categories = await _reviewService.GetCategories();
            NotifyListChanged(Categories, EventArgs.Empty);
        }

        public async Task GetTagsAsync()
        {
            Tags = await _reviewService.GetTags();
            NotifyListChanged(Tags, EventArgs.Empty);
        }

        public async Task<List<Tag>> FindTagsByValueAsync(string value)
        {
            return await _reviewService.GetTagsByValue(value);
        }

        public async Task GetCurrentReviewTagsAsync(int reviewId)
        {
            ReviewTags = await _reviewService.GetReviewTags(reviewId);
            NotifyListChanged(ReviewTags, EventArgs.Empty);
        }

        public async Task AddTagToReview(ReviewTag reviewTag)
        {
            await _reviewService.AddTagToReview(reviewTag);
            ReviewTags!.Add(reviewTag);
            NotifyListChanged(ReviewTags, EventArgs.Empty);
        }

        public async Task RemoveTagFromReviewAsync(int reviewId, string tagTitle)
        {
            await _reviewService.RemoveTagFromReview(reviewId, tagTitle);
            NotifyListChanged(ReviewTags, EventArgs.Empty);
        }

        public async Task GetWorksByCategoryAsync(Category category)
        {
            Works = await _reviewService.GetWorksByCategory(category);
            NotifyListChanged(Works, EventArgs.Empty);
        }

        public async Task GetAllWorksAsync()
        {
            Works = await _reviewService.GetAllWorks();
            NotifyListChanged(Works, EventArgs.Empty);
        }

        public async Task DeleteWorkAsync(Work work)
        {
            await _reviewService.DeleteWork(work);
            await GetAllWorksAsync();
        }

        public event EventHandler<EventArgs>? ListChanged;

        public void NotifyListChanged(object sender, EventArgs e)
            => ListChanged?.Invoke(sender, e);
    }
}
