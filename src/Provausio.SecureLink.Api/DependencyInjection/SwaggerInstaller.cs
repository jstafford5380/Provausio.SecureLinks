using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Provausio.SecureLink.Api.DependencyInjection
{
    public static class SwaggerInstaller
    {
        public static void InstallSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Secured Links",
                    Version = "1.0"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
        }

        public static void Swagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config => { config.SwaggerEndpoint("/swagger/v1/swagger.json", "Secured Links"); });
        }
    }
}
