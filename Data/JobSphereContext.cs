﻿using JobSphere.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobSphere.Data
{
    public class JobSphereContext(DbContextOptions<JobSphereContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<JobTag> jobTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<JobTag>().
                HasOne(jt => jt.Job)
                .WithMany(j => j.JobTags)
                .HasForeignKey(jt => jt.JobId);
            modelBuilder.Entity<JobTag>()
                .HasOne(jt => jt.Tag)
                .WithMany(t => t.JobTags)
                .HasForeignKey(jt => jt.TagId);

            modelBuilder.Entity<Job>()
                .Property(j => j.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, Title = "Software Development" ,CreatedAt = new DateTime(2025,1,1)},
                new Tag { Id = 2, Title = "Marketing" , CreatedAt = new DateTime(2025, 1, 1) },
                new Tag { Id = 3, Title = "Remote" , CreatedAt = new DateTime(2025, 1, 1) },
                new Tag { Id = 4, Title = "Finance" , CreatedAt = new DateTime(2025, 1, 1) },
                new Tag { Id = 5, Title = "Sales" , CreatedAt = new DateTime(2025, 1, 1) },
                new Tag { Id = 6, Title = "Healthcare" , CreatedAt = new DateTime(2025, 1, 1)   },
                new Tag { Id = 7, Title = "Customer Support"  , CreatedAt = new DateTime(2025, 1, 1) },
                new Tag { Id = 8, Title = "Project Management" , CreatedAt = new DateTime(2025, 1, 1) },
                new Tag { Id = 9, Title = "Engineering" , CreatedAt = new DateTime(2025, 1, 1) },
                new Tag { Id = 10, Title = "Education" , CreatedAt = new DateTime(2025, 1, 1) }
                );
        }
    }
}
