using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Task5.Models;
using Task5.Models;

namespace WepApp.DataStore.SQL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Risk> Risks { get; set; }

    }
}
