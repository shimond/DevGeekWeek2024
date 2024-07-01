namespace CatalogService.Routes;

public static class CourseRoutes
{
    public static void MapCourses(this IEndpointRouteBuilder app)
    {
        var courseGroup = app.MapGroup("courses").WithTags("Courses");

        courseGroup.MapGet("{id}", GetById);
        courseGroup.MapGet("", GetAllCourses)
            .CacheOutput(x=>x.Expire(TimeSpan.FromMinutes(5)));
        courseGroup.MapPost("", AddNewCourse);
    }

    static async Task<Created<Course>> AddNewCourse(ICourseRepository repo, Course c)
    {
        var newItem = await repo.AddNewCourse(c);
        return TypedResults.Created($"/courses/{newItem.Id}",newItem);
    }

    static async Task<Ok<List<Course>>> GetAllCourses(ICourseRepository repo)
    {
        await Task.Delay(5000);
        var all = await repo.GetAllCourses();
        return TypedResults.Ok(all);
    }

    static async Task<Results<Ok<Course>, NotFound>> GetById(ICourseRepository repo, int id)
    {
        var c = await repo.GetById(id);
        if (c is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(c);
    }
}
