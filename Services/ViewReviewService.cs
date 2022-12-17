using CourseProject.Models;
using Microsoft.AspNetCore.Components;

namespace CourseProject.Services
{
    public class ViewReviewService
    {
        public ReviewService _reviewService;

        public List<Review>? Reviews { get; private set; }

        public ViewReviewService(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task GetReviewsAsync()
        {
            Reviews = await _reviewService.GetReviewsAsync();
            NotifyListChanged(Reviews, EventArgs.Empty);
        }

        public async Task DeleteReviewAsync(Review review)
        {
            await _reviewService.DeleteReview(review);
            await GetReviewsAsync();
        }
        
        public event EventHandler<EventArgs>? ListChanged;

        public void NotifyListChanged(object sender, EventArgs e)
            => ListChanged?.Invoke(sender, e);
    }
}
