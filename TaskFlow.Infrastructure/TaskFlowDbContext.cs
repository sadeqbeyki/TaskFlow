using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Core.Entities;

namespace TaskFlow.Infrastructure;

public class TaskFlowDbContext : DbContext
{
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options) { }


    //public DbSet<User> Users { get; set; } = null!;
    //public DbSet<Project> Projects { get; set; } = null!;
    //public DbSet<TaskItem> TaskItems { get; set; } = null!;

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);

            // Example: configure relations & constraints
            entity.HasMany(u => u.Projects)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Cascade); //Cascade: delete Project and TaskItems //Restrict: Prevent the deletion of a PROJECT with a TASK.

        });

        // --- Project configuration ---
        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Projects");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Title)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(p => p.Description)
                  .HasMaxLength(500);

            entity.HasMany(p => p.Tasks)
                  .WithOne(t => t.Project)
                  .HasForeignKey(t => t.ProjectId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // --- TaskItem configuration ---
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.ToTable("TaskItems");

            entity.HasKey(t => t.Id);

            entity.Property(t => t.Title)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(t => t.Description)
                  .HasMaxLength(500);

            entity.Property(t => t.Priority)
                  .HasConversion<int>();

            entity.Property(t => t.Status)
                  .HasConversion<int>();

            //Set CreatedAt to automatic
            entity.Property(t => t.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            entity.HasIndex(t => t.Status);
            entity.HasIndex(t => t.Priority);
        });



    }
}
