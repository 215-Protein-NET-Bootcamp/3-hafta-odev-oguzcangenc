using FluentValidation;
using JWTAuth.Business.Validations.FluentValidation;
using JWTAuth.Entities;
using JWTAuth.Entities.Dto;
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

            services.AddSingleton<IValidator<UserForLoginDto>, UserForLoginDtoValidator>();
            services.AddSingleton<IValidator<UserForRegisterDto>, UserForRegisterDtoValidator>();
            services.AddSingleton<IValidator<UserForChangePasswordDto>, UserForChangePasswordDtoValidator>();
            services.AddSingleton<IValidator<UserForEditDto>, UserForEditDtoValidator>();

            services.AddSingleton<IValidator<EmployeeAddDto>, EmployeeAddDtoValidator>();
        }
    }
}
