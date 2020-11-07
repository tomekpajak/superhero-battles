using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shb.Infrastructure.Configurations;
using Microsoft.Extensions.Logging;

namespace Shb.Infrastructure.Persistence.Database
{
    internal interface IShbContextFactory : IDesignTimeDbContextFactory<ShbContext> 
    {        
    }
    internal class ShbContextFactory : IShbContextFactory
    {
        public ShbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = CreateConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<ShbContext>();
            optionsBuilder.UseSqlServer(configuration[ShbInfrastructureConst.DbContext], 
                                        builder => builder.MigrationsAssembly(typeof(ShbContext).Assembly.FullName));
                
            return new ShbContext(optionsBuilder.Options);
        }

        private IConfiguration CreateConfiguration()  
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Shb.Api"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .Build();
        }
    }
}
