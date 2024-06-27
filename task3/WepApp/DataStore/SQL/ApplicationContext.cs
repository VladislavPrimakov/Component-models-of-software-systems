using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WepApp.Models;

namespace WepApp.DataStore.SQL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Placement> Placements { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "admin" }, 
                new Role { Id = 2, Name = "user" }
                );
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "admin", Password = "admin", RoleId = 1 },
                new User { UserId = 2, UserName = "user", Password = "user", RoleId = 2 }
                );
        }
    }
}
