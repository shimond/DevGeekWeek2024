using Confluent.Kafka;
using Microsoft.Extensions.Options;

public interface IEventPublisher
{
    Task PublishMessageAsync<T>(T message);
}

public class EventPublisher : IEventPublisher
{
    private readonly IProducer<Null, string> _producer;

    public EventPublisher(IOptions<KafkaSettings> settings)
    {
        var config = new ProducerConfig { BootstrapServers = settings.Value.BootstrapServers };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task PublishMessageAsync<T>(T message)
    {
        var messageJson = System.Text.Json.JsonSerializer.Serialize(message);
        var res = await _producer.ProduceAsync("CourseAverageRatingChangedEvent", new Message<Null, string> { Value = messageJson });
        Console.WriteLine(res.Status);
    }
}


public class KafkaSettings
{
    public string BootstrapServers { get; set; }
}