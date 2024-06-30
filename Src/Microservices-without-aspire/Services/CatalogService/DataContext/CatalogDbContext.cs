namespace CatalogService.DataContext;
public class CatalogDbContext : DbContext
{

    public CatalogDbContext(DbContextOptions<CatalogDbContext> dbContextOptions) 
        :base(dbContextOptions)
    {
            
    }

    public DbSet<Course> Courses { get; set; }

}
