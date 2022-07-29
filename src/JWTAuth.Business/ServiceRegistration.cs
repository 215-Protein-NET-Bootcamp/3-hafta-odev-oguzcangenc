using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Business
{
    public static class ServiceRegistration
    {
        public static void AddBusinessLayerServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IAuthService, AuthService>();

        }
    }
}
