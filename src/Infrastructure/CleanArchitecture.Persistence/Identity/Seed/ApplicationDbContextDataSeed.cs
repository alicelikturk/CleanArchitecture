using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities.Identity;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Identity.Seed
{
    public class ApplicationDbContextDataSeed
    {
        public static async Task SeedAsync(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<User>>();
            var roleManager = service.GetRequiredService<RoleManager<UserRole>>();
           

            await roleManager.CreateAsync(new UserRole("Administrator"));
            await roleManager.CreateAsync(new UserRole("Manager"));
            await roleManager.CreateAsync(new UserRole("User") { IsDefault = true });

            string adminUserName = "celikturkali@test.com";
            var adminUser = new User
            {
                UserName = adminUserName,
                Email = adminUserName,
                IsEnabled = true,
                EmailConfirmed = true,
                FirstName = "Ali",
                LastName = "Celikturk"
            };

            await userManager.CreateAsync(adminUser, "Password@1");
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, "Administrator");
            await userManager.AddToRoleAsync(adminUser, "User");
        }
    }
}
