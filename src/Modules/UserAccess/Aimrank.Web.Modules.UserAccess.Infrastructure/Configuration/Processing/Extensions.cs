using Aimrank.Web.Common.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal static class Extensions
    {
        public static IServiceCollection AddProcessing(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(Assemblies.Application, Assemblies.Infrastructure)
                .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            services.AddMediatR(Assemblies.Domain, Assemblies.Application, Assemblies.Infrastructure);
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DomainEventDispatcher>();
            services.AddScoped<IDomainEventAccessor, DomainEventAccessor>();

            return services;
        }
    }
}