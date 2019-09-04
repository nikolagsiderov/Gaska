using Gaska.Data.Models;
using Gaska.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gaska.Data
{
    public class DataAccessContext : IdentityDbContext<ApplicationUser>
    {
        public DataAccessContext(DbContextOptions<DataAccessContext> options) : base(options)
        {
        }
        
        public DbSet<ServiceBookLog> ServiceBookLogs { get; set; }
        public DbSet<ServiceBook> ServiceBooks { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ServiceBookLog>().ToTable("ServiceBookLog");
            modelBuilder.Entity<ServiceBook>().ToTable("ServiceBook");
            modelBuilder.Entity<Car>().ToTable("Car");
        }
    }
}
