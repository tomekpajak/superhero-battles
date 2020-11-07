using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Shb.Api.Extensions
{
    internal static class ServiceCollectionExtensionSwagger
    {
        public static void AddSwaggerExtension(this IServiceCollection services) 
        {
            services.AddSwaggerGen(o => {
                o.SwaggerDoc("v1.0", new OpenApiInfo{
                    Version = "v1.0",
                    Title = "Superhero battles API",
                    Description = "API for Superhero battles"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));                
            });
        }
    }
}
