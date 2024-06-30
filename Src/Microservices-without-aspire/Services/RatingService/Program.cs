using RatingService.Config;
using RatingService.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection("Mongo"));
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("KafkaSettings"));

builder.Services.AddScoped<IReviewsRepository, ReviewsRepository>();
builder.Services.AddScoped<IEventPublisher, EventPublisher>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
