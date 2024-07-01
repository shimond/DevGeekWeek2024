namespace CatalogService.Model;

public record Course(int Id, string Name, string? Description = null, double? ratingAvg = null);


public class KafkaSettings
{
    public string BootstrapServers { get; set; }
}