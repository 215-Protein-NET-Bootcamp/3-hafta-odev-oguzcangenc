using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Data
{
    public static class ServiceRegistration
    {
        public static void AddDataLayerServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
