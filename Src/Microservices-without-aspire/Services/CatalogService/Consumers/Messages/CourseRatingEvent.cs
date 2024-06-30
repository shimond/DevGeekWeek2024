namespace CatalogService.Consumers.Messages;

public class CourseRatingChangedEvent
{
    public double CurrentAvg { get; set; }
    public int CourseId { get; set; }
}
