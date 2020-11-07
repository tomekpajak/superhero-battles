using System;
using Microsoft.AspNetCore.Builder;

namespace Shb.Api.Extensions
{
    internal static class ApplicatioBuilderExtensionSwagger    
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI(o => {
                o.SwaggerEndpoint($"/swagger/v1.0/swagger.json", "v1");
                o.RoutePrefix = string.Empty;
            });
        }
    }
}
