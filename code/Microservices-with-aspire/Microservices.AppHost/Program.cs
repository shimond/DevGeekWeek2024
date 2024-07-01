var builder = DistributedApplication.CreateBuilder(args);

var reviewsDb = builder.AddMongoDB("mongo-reviews");
var kafka = builder.AddKafka("kafka");
var sqlCatalog = builder.AddSqlServer("catalog-db");

builder.AddProject<Projects.RatingService>("ratingservice")
    .WithReference(reviewsDb)
    .WithReference(kafka);


builder.AddProject<Projects.CatalogService>("catalogservice")
    .WithReference(sqlCatalog)
    .WithReference(kafka);

builder.Build().Run();
