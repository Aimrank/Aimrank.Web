using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using System;

namespace Aimrank.Web.App.Configuration.Swagger
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.IsDevelopment() || configuration.IsDocker())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Aimrank.Web.App",
                        Version = "v1"
                    });
                    
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    
                    c.IncludeXmlComments(xmlPath);
                });
                
                return services;
            }

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder builder, IConfiguration configuration)
        {
            if (configuration.IsDevelopment() || configuration.IsDocker())
            {
                builder.UseSwagger();
                builder.UseSwaggerUI();
                
                return builder;
            }

            return builder;
        }
    }
}