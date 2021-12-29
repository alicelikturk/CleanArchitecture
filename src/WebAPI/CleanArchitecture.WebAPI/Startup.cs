using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NSwag;
using NSwag.Generation.Processors.Security;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Common.Interfaces.CurrentUser;
using CleanArchitecture.Persistence;
using CleanArchitecture.Persistence.Extensions;
using CleanArchitecture.WebAPI.Configuration;
using CleanArchitecture.WebAPI.Extensions;
using CleanArchitecture.WebAPI.Filters;
using CleanArchitecture.WebAPI.Services;
using System;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using OpenApiSecurityScheme = NSwag.OpenApiSecurityScheme;

namespace CleanArchitecture.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddPersistenceServices(Configuration);

            services.AddSingleton<ICurrentUserService, CurrentUserService>();


            services.SetupAuthentication(Configuration);
            services.SetAuthorization();


            // HealthCheck
            services.AddHealthChecks();

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            })
                .AddFluentValidation(x => x.DisableDataAnnotationsValidation = true)
                .AddNewtonsoftJson(options =>
                {
                    //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    //options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;
                });

            services.SetupSwagger();



            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            // NSwag Swagger
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                if (app.EnsureIdentityDbIsCreated())
                    app.SeedIdentityDataAsync().Wait();
                app.UseHttpsRedirection();
            }

            // HealthCheck
            app.UseCustomHealthCheck();

            // Response Caching
            app.UseResponseCaching();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //serviceProvider.GetService<ApplicationDbContext>().Database.Migrate();
        }
    }
}
