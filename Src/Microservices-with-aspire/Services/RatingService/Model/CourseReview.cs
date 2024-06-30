using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RatingService.Model;


public class CourseReview
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("CourseId")]
    public int CourseId { get; set; }

    [BsonElement("RatingValue")]
    public int RatingValue { get; set; }

    [BsonElement("StudentId")]
    public string StudentId { get; set; }
}
