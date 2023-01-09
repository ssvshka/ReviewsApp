using CourseProject.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CourseProject.Services
{
    public class ViewService
    {
        public ReviewService _reviewService;
        public UserService _userService;

        public List<Review>? AllReviews { get; private set; }
        public List<Review>? UserReviews { get; private set; }
        public List<User>? Users { get; private set; }
        public List<Comment>? Comments { get; private set; }
        public List<Tag>? Tags { get; private set; }
        public List<string> CurrentReviewTags { get; private set; } = new();
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
            AllReviews = await _reviewService.GetReviewsAsync();
            NotifyListChanged(AllReviews, EventArgs.Empty);
        }

        public void OrderReviewsByRating()
        {
            AllReviews = AllReviews!.OrderByDescending(x => x.Work.OverallAuthorRating).ToList();
            NotifyListChanged(AllReviews!, EventArgs.Empty);
        }

        public async Task GetUserReviewsAsync(string id)
        {
            UserReviews = await _reviewService.GetReviewsAsync(id);
            NotifyListChanged(UserReviews, EventArgs.Empty);
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

        public async Task GetCurrentReviewTagsAsync(int reviewId)
        {
            var tags = await _reviewService.GetReviewTags(reviewId);
            foreach (var t in tags) 
            {
                CurrentReviewTags.Add(t.Tag.Title);
            }
            NotifyListChanged(CurrentReviewTags, EventArgs.Empty);
        }

        public async Task AddTagToReview(ReviewTag reviewTag)
        {
            await _reviewService.AddTagToReview(reviewTag);
            ReviewTags!.Add(reviewTag);
            NotifyListChanged(ReviewTags, EventArgs.Empty);
        }

        public async Task RemoveTagFromReviewAsync(string title)
        {
            //await _reviewService.RemoveTagFromReview(reviewId, tagTitle);
            var t = await _reviewService.GetTagByTitle(title);
            //ReviewTags!.Remove(t);
            NotifyListChanged(ReviewTags!, EventArgs.Empty);
        }

        public async Task GetWorksByCategoryAsync(Category category)
        {
            Works = await _reviewService.GetWorksByCategory(category);
            NotifyListChanged(Works, EventArgs.Empty);
        }

        public void AddTagToView(string tag)
        {
            if (!CurrentReviewTags.Contains(tag))
                CurrentReviewTags.Add(tag);
            NotifyListChanged(CurrentReviewTags, EventArgs.Empty);
        }

        public void RemoveTagFromView(string tag)
        {
            CurrentReviewTags.Remove(tag);
            NotifyListChanged(CurrentReviewTags, EventArgs.Empty);
        }

        public event EventHandler<EventArgs>? ListChanged;

        public void NotifyListChanged(object sender, EventArgs e)
            => ListChanged?.Invoke(sender, e);
    }
}
