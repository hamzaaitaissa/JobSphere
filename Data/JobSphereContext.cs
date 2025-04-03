using JobSphere.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobSphere.Data
{
    public class JobSphereContext(DbContextOptions<JobSphereContext> options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<JobTag> jobTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobTag>().
                HasOne(jt => jt.Job)
                .WithMany(j => j.JobTags)
                .HasForeignKey(jt => jt.JobId);
            modelBuilder.Entity<JobTag>()
                .HasOne(jt => jt.Tag)
                .WithMany(t => t.JobTags)
                .HasForeignKey(jt =>jt.TagId);

        }
    }
}
