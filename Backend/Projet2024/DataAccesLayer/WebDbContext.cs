using Domaine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseNote> CourseNotes { get; set; }
        public DbSet<CourseUser> CourseUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasKey(n => n.CourseId);

            modelBuilder.Entity<CourseUser>()
                .HasKey(cu => cu.CourseUserId);

            modelBuilder.Entity<CourseUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.CourseUsers)
                .HasForeignKey(cu => cu.UserId);

            modelBuilder.Entity<CourseUser>()
               .HasOne(cu => cu.Course)
               .WithMany(c => c.CourseUsers)
               .HasForeignKey(cu => cu.CourseId);

            base.OnModelCreating(modelBuilder);
        }
    }
} 
