using JWTAuth.Data;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.WebAPI
{
    public static class ApplicationDbContextExtension
    {
        public static void AddDbContextDependecyInjection(this IServiceCollection services,WebApplicationBuilder builder) 
        {
            services.AddDbContext<AppEfDbContext>(opt =>
            {
                opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


        }
    }
}
