using RatingService.Config;
using RatingService.Model;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddKafkaProducer<string, string>("kafka");
builder.AddMongoDBClient("mongo-reviews");
builder.Services.AddScoped<IReviewsRepository, ReviewsRepository>();
builder.Services.AddScoped<IEventPublisher, EventPublisher>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("review", async (IReviewsRepository reviewRep, IEventPublisher publisher, CourseReview review) =>
{
    await reviewRep.AddReviewAsync(review);
    var reviews = await reviewRep.GetReviewsByCourseIdAsync(review.CourseId);
    var newRatingvalue = reviews.Average(x => x.RatingValue);

    CourseRatingChangedEvent ev = new () { CourseId = review.CourseId, CurrentAvg = newRatingvalue };
    await publisher.PublishMessageAsync(ev);
    return Results.Ok(review);
});


app.Run();
