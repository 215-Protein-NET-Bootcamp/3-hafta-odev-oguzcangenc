﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Core
{
    public static class ServiceRegistration
    {
        public static void AddCoreLayerServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<ITokenHelper, JwtHelper>();

        }
    }
}
