namespace CatalogService.Services;

public class CourseRepository(CatalogDbContext context) : ICourseRepository
{
    
    public async Task<Course> AddNewCourse(Course c)
    {
        context.Courses.Add(c);
        await context.SaveChangesAsync();
        return c;
    }

    public async Task<List<Course>> GetAllCourses()
    {
        var courses = await context.Courses.ToListAsync();
        return courses;
    }

    public async Task<Course?> GetById(int id)
    {
        var item = await context.Courses.SingleOrDefaultAsync(c => c.Id == id);
        return item;
    }

    public async Task UpdateCourseRating(int courseId, double currentAvg)
    {
        var currentItem = await context.Courses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == courseId);
        if (currentItem != null)
        {
            var itemToUpdate = currentItem with { ratingAvg = currentAvg }; 
            context.Attach(itemToUpdate).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

    }
}
