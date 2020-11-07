using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shb.Domain.Abstractions;
using Shb.Domain.Repositories;
using Shb.Domain.Repositories.Abstraction;
using Shb.Infrastructure.Logging;
using Shb.Infrastructure.Persistence.Database;
using Shb.Infrastructure.Persistence.Database.Repositories;
using Shb.Infrastructure.Persistence.Database.Repositories.Abstraction;

namespace Shb.Infrastructure.Configurations
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(MicrosoftAppLoggerAdapter<>));

            services.AddDbContext<ShbContext>(options => 
                options.UseSqlServer(configuration[ShbInfrastructureConst.DbContext], 
                                     builder => builder.MigrationsAssembly(typeof(ShbContext).Assembly.FullName)));

            services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
            services.AddScoped<ISuperheroRepository, SuperheroRepository>();
            services.AddTransient<IShbContextFactory, ShbContextFactory>();

            return services;
        }
    }
}
