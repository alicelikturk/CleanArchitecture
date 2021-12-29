using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Respawn;
using Respawn.Postgres;
using CleanArchitecture.Persistence;
using CleanArchitecture.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CleanArchitecture.Application.Common.Interfaces.CurrentUser;

namespace CleanArchitecture.Application.IntegrationTests
{
    /// <summary>
    /// Configure services and helpers
    /// </summary>
    public class BaseFixtures : IDisposable
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static PostgresCheckpoint checkpoint;
        private static Guid _currentUserId;

        private static string CONNECTIONSTRING = "";

        public BaseFixtures()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var services = new ServiceCollection();

            var startup = new Startup(_configuration);

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
            w.ApplicationName == "CleanArchitecture.WebAPI" &&
            w.EnvironmentName == "Development"));

            startup.ConfigureServices(services);

            // Replace service registration for ICurrentUserService
            // Remove existing registration
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            services.Remove(currentUserServiceDescriptor);

            // Register testing version
            services.AddTransient(provider =>
                Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId));


            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            checkpoint = new PostgresCheckpoint
            {
               // AutoCreateExtensions = true,

                //SchemasToInclude = new[] { "public" },
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };

            EnsureDatabase();
        }
        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.Migrate();

            CONNECTIONSTRING = context.Database.GetConnectionString();
        }


        public void ResetState()
        {
            Task.Run(async () => await ResetStateAsync());
        }

        public async Task ResetStateAsync()
        {
            await checkpoint.Reset(CONNECTIONSTRING);
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var resultEntity = await context.AddAsync(entity);

            await context.SaveChangesAsync();

            return resultEntity.Entity;
        }
        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public void Dispose()
        {
            //_context.Dispose();
        }
    }
}
