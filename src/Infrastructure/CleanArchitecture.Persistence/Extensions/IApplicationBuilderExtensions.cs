using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Domain.Entities.Identity;
using CleanArchitecture.Persistence.Identity.Seed;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>true if the database is created, false if it already existed.</returns>
        public static bool EnsureIdentityDbIsCreated(this IApplicationBuilder builder)
        {
            using (var serviceScope=builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = serviceScope.ServiceProvider;

                var dbContext = service.GetRequiredService<ApplicationDbContext>();

                var databaseExist=dbContext.Database.EnsureCreated();
                //dbContext.Database.Migrate();
                return databaseExist;
            }
        }


        public static async Task SeedIdentityDataAsync(this IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = serviceScope.ServiceProvider;

                
                await ApplicationDbContextDataSeed.SeedAsync(service);
            }
        }
    }
}
