using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RatingService.Config;
using RatingService.Model;
using System.Collections;

public class ReviewsRepository : IReviewsRepository
{
    private readonly IMongoCollection<CourseReview> _reviewsCollection;

    public ReviewsRepository(IOptions<MongoConfig> options)
    {
        var config = options.Value;
        var client = new MongoClient(config.ConnectionString);
        var database = client.GetDatabase(config.DatabaseName);
        _reviewsCollection = database.GetCollection<CourseReview>(config.CollectionName);
    }

    public async Task AddReviewAsync(CourseReview review)
    {
        await _reviewsCollection.InsertOneAsync(review);
    }

    public async Task<List<CourseReview>> GetReviewsByCourseIdAsync(int courseId)
    {
        var filter = Builders<CourseReview>.Filter.Eq(r => r.CourseId, courseId);
        return await _reviewsCollection.Find(filter).ToListAsync();
    }
}
