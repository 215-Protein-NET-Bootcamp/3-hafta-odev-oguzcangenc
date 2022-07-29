using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace JWTAuth.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppEfDbContext>
    {
        public AppEfDbContext CreateDbContext(string[] args)
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../JWTAuth.WebAPI"));
            configurationManager.AddJsonFile("appsettings.json");
            DbContextOptionsBuilder<AppEfDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(configurationManager.GetConnectionString("DefaultConnection"));
            return new AppEfDbContext(dbContextOptionsBuilder.Options, new HttpContextAccessor());
        }
    }
}
