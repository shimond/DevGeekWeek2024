using RatingService.Model;

public interface IReviewsRepository
{
    Task AddReviewAsync(CourseReview review);
    Task<List<CourseReview>> GetReviewsByCourseIdAsync(int courseId);
}