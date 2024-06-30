using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RatingService.Model;


public class CourseRatingChangedEvent
{
    public double CurrentAvg { get; set; }
    public int CourseId { get; set; } 
}