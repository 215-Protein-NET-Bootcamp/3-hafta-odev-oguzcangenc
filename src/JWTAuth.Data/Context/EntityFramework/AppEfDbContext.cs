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
        public int CurrentUserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    var claimValue = _httpContextAccessor.HttpContext?.User?.FindFirst(t=>t.Type=="AccountId");
                    if (claimValue != null)
                    {
                        return Convert.ToInt32(claimValue.Value);
                    }
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
            set => throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
                modelBuilder.Entity<Employee>().HasQueryFilter(e => e.ApplicationUserId == CurrentUserId);
            
            base.OnModelCreating(modelBuilder);

        }


    }
}
