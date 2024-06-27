using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebApp.Models;
using WepApp.Models;

namespace WepApp.DataStore.SQL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Resume> Resumes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "candidate" }, 
                new Role { Id = 2, Name = "employer" }
                );
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "Kyiv" },
                new Location { LocationId = 2, Name = "Zaporizhzhia" },
                new Location { LocationId = 3, Name = "Dnipro" }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Accounting & Finance" },
                new Category { CategoryId = 2, Name = "Administrative & Office Support" },
                new Category { CategoryId = 3, Name = "Advertising & Marketing" },
                new Category { CategoryId = 4, Name = "Architecture & Engineering" },
                new Category { CategoryId = 5, Name = "Arts, Entertainment & Media" },
                new Category { CategoryId = 6, Name = "Customer Service & Call Center" },
                new Category { CategoryId = 7, Name = "Education & Training" },
                new Category { CategoryId = 8, Name = "Healthcare & Medical" },
                new Category { CategoryId = 9, Name = "Hospitality & Travel" },
                new Category { CategoryId = 10, Name = "Human Resources" }
                );
        }
    }
}
