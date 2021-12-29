using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZymLabs.NSwag.FluentValidation;

namespace CleanArchitecture.WebAPI.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void SetupSwagger(this IServiceCollection services)
        {
            services.AddOpenApiDocument((options, serviceProvider) =>
            {
                options.DocumentName = "v1";
                options.Title = "CleanArchitecture API";
                options.Version = "v1";
                
                // Add JWT token authorization
                options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("auth"));
                options.DocumentProcessors.Add(new SecurityDefinitionAppender("auth", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.Http,
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    Description = "Type into the textbox: Bearer {your JWT token}."
                }));

            });
        }
    }
}
