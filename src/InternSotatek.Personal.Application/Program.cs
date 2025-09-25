using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace InternSotatek.Personal.Application
{
    public static class Program
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Đăng ký MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            // Đăng ký FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
