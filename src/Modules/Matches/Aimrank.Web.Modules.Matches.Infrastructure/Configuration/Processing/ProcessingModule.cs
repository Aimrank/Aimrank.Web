using Aimrank.Web.Common.Infrastructure;
using Autofac;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing
{
    internal class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assemblies.Application, Assemblies.Infrastructure)
                .AsClosedTypesOf(typeof(IValidator<>))
                .SingleInstance();

            builder.RegisterMediatR(Assemblies.Domain, Assemblies.Application, Assemblies.Infrastructure);
            
            builder.RegisterGeneric(typeof(ValidationPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(UnitOfWorkPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<DomainEventDispatcher>().InstancePerLifetimeScope();
            builder.RegisterType<DomainEventAccessor>().As<IDomainEventAccessor>().InstancePerLifetimeScope();
        }
    }
}