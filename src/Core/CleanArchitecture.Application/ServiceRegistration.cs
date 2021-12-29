using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Application.Common.Behaviours;
using System.Reflection;

namespace CleanArchitecture.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            var assembly = Assembly.GetExecutingAssembly();
            service.AddAutoMapper(assembly);
            service.AddMediatR(assembly);
            service.AddValidatorsFromAssembly(assembly);
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

            return service;
        }
    }
}
