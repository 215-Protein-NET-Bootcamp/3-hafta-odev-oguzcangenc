using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new AppEfDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
