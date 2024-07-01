using CatalogService.Consumers.Messages;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace CatalogService.Consumers;


public class KafkaConsumerService : BackgroundService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly IServiceProvider _provider;

    public KafkaConsumerService(IConsumer<string, string>  consumer , IServiceProvider provider)
    {

        _provider = provider;
        //var config = new ConsumerConfig
        //{
        //    GroupId = "subscribers",
        //    BootstrapServers = settings.Value.BootstrapServers,
        //    AutoOffsetReset = AutoOffsetReset.Earliest
        //};

        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            _consumer.Subscribe("CourseAverageRatingChangedEvent");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    ConsumeResult<string, string> consumeResult = await GetMessage(cancellationToken);
                    var ratingChangeEvent = System.Text.Json.JsonSerializer.Deserialize<CourseRatingChangedEvent>(consumeResult.Message.Value);

                    Console.WriteLine($"CourseId: {ratingChangeEvent.CourseId}, AverageRating: {ratingChangeEvent.CurrentAvg}");
                    using (var scope = _provider.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetRequiredService<ICourseRepository>();
                        await repository.UpdateCourseRating(ratingChangeEvent.CourseId, ratingChangeEvent.CurrentAvg);
                    }
                    await Task.Delay(500, cancellationToken);
                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Prevent throwing if the operation is canceled
        }
        finally
        {
            _consumer.Close();
        }
    }

    private Task<ConsumeResult<string, string>> GetMessage(CancellationToken cancellationToken)
    {
        return Task.Factory.StartNew(()=> _consumer.Consume(cancellationToken));
    }

}
