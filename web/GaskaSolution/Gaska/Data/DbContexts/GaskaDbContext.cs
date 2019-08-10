using Gaska.Data.Models;
using Gaska.Models;
using Gaska.Models.CreateGuildViewModels;
using Gaska.Models.CreateMembers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gaska.Data.DbContexts
{
    public class GaskaDbContext : IdentityDbContext<ApplicationUser>
    {
        public GaskaDbContext(DbContextOptions<GaskaDbContext> options) : base(options)
        {
        }

        public DbSet<CreateGuildViewModel> CreateGuildViewModel { get; set; }
        public DbSet<CreateMemberViewModel> CreateMemberViewModel { get; set; }
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
