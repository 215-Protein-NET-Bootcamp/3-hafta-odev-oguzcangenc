using JWTAuth.Entities;
using Microsoft.EntityFrameworkCore;
namespace JWTAuth.Data
{
    public class AppEfDbContext : DbContext
    {
        public AppEfDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
