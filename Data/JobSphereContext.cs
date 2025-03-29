using Microsoft.EntityFrameworkCore;

namespace JobSphere.Data
{
    public class JobSphereContext(DbContextOptions<JobSphereContext> options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
