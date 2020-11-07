using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shb.Infrastructure.Persistence.Database;
using Shb.Domain.Abstractions;
using System.Threading;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Shb.Api.Extensions
{
    internal static class HostExtensionMigrateDB 
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<ShbContext>())
            {                    
                var logger = scope.ServiceProvider.GetRequiredService<IAppLogger<ShbContext>>();

                try
                {
                    logger.LogInformation("Checking migrations...");
                    context.Database.Migrate();
                }
                catch (SqlException ex) 
                {
                    if (ex.Message.StartsWith("A network-related or instance-specific error occurred while establishing a connection to SQL Server")) 
                    {
                        logger.LogDebug("A network-related or instance-specific error occurred while establishing a connection to SQL Server...");
                        logger.LogWarning("Replaying migration in 3 seconds.");
                        Thread.Sleep(3000);
                        MigrateDatabase(host);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Could not migrate database.");
                    throw;
                }
            }
            return host;
        }
    }
}