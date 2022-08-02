using JWTAuth.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JWTAuth.Data
{
    public class AppEfDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppEfDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging(false);
        }

        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<Employee> Employees { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
