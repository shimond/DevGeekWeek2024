using CatalogService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddKafkaConsumer<string, string>("kafka", x=>x.Config.GroupId = "subsribers-for-rating");

builder.Services.Configure<HostOptions>(o =>
{
    o.ServicesStartConcurrently = true;
});

builder.Services.AddDbContext<CatalogDbContext>(o => o.UseInMemoryDatabase("catalog"));
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<KafkaConsumerService>();
var app = builder.Build();

app.MapDefaultEndpoints();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapCourses();
app.Run();

