namespace CatalogService.Contracts;

public interface ICourseRepository
{
    Task<List<Course>> GetAllCourses();
    Task<Course?> GetById(int id);
    Task<Course> AddNewCourse(Course c);
    Task UpdateCourseRating(int courseId, double currentAvg);
}
