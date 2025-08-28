using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Core;

namespace TaskFlow.Infrastructure
{
    public class TaskFlowDbContext : DbContext
    {
        public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<TaskItem> TaskItems { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Example: configure relations & constraints
            modelBuilder.Entity<User>().HasMany(u => u.Projects).WithOne(p => p.Owner).HasForeignKey(p => p.OwnerId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Project>().HasMany(p => p.Tasks).WithOne(t => t.Project).HasForeignKey(t => t.ProjectId).OnDelete(DeleteBehavior.Cascade);


            // Indexes
            modelBuilder.Entity<TaskItem>().HasIndex(t => t.Status);
            modelBuilder.Entity<TaskItem>().HasIndex(t => t.Priority);
        }
    }
}
