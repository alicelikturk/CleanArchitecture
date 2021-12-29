using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Application.Common.Dto.Authentication;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Application.Common.Interfaces.Token;
using CleanArchitecture.Domain.Entities.Identity;
using CleanArchitecture.Persistence.Identity.Services;
using CleanArchitecture.Persistence.Repositories;
using CleanArchitecture.Persistence.Security;

namespace CleanArchitecture.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection service, IConfiguration configuration)
        {
            //service.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseNpgsql("..connection string...");
            //    options.EnableSensitiveDataLogging(true);
            //}, ServiceLifetime.Transient);

            service.AddDbContext<ApplicationDbContext>(options=>options.UseInMemoryDatabase("memorydb"));
            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IPermissionStoreRepository, PermissionStoreRepository>();


            service.Configure<Token>(configuration.GetSection("token"));

            service.AddIdentity<User, UserRole>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<User>>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            service.Configure<IdentityOptions>(options => {
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                // Identity: Default password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            service.AddScoped<ITokenService, TokenService>();
            service.AddTransient<IIdentityService, IdentityService>();

            // DEPRECATED
            // "service.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));" is used instead of this

            //service.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
            //service.AddSingleton<IAuthorizationHandler, CustomAuthorizationPolicyHandler>();
        }
    }
}
