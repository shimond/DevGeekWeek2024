using Confluent.Kafka;
using Microsoft.Extensions.Options;

public interface IEventPublisher
{
    Task PublishMessageAsync<T>(T message);
}

public class EventPublisher : IEventPublisher
{
    private readonly IProducer<string, string> _producer;

    public EventPublisher(IOptions<KafkaSettings> settings, IProducer<string, string> producer)
    {
        var config = new ProducerConfig { BootstrapServers = settings.Value.BootstrapServers };
        _producer = producer;
    }

    public async Task PublishMessageAsync<T>(T message)
    {
        var messageJson = System.Text.Json.JsonSerializer.Serialize(message);
        var res = await _producer.ProduceAsync("CourseAverageRatingChangedEvent", new Message<string, string> { Value = messageJson });
        Console.WriteLine(res.Status);
    }
}


public class KafkaSettings
{
    public string BootstrapServers { get; set; }
}