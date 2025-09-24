using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternSotatek.Personal.Infrastructure;
public static class PersonalDbContextConfigure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<PersonalDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultDbContext")));

        services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));

        return services;
    }
}

