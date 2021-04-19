using Autofac;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Mediator
{
    internal class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assemblies.Application, Assemblies.Infrastructure)
                .AsClosedTypesOf(typeof(IValidator<>))
                .SingleInstance();
            
            builder.RegisterMediatR(Assemblies.Application, Assemblies.Infrastructure);
            
            builder.RegisterGeneric(typeof(LoggingPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidationPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(UnitOfWorkPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}