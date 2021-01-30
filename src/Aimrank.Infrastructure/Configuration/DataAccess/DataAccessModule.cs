using Aimrank.Application;
using Aimrank.Common.Infrastructure;
using Aimrank.Domain.Users;
using Aimrank.Infrastructure.Application;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Infrastructure.Configuration.DataAccess
{
    internal class DataAccessModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;

        public DataAccessModule(string databaseConnectionString)
        {
            _databaseConnectionString = databaseConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            LoadIdentityCore(builder);
            
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<AimrankContext>();
                    dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString,
                        x => x.MigrationsAssembly("Aimrank.Database.Migrator"));
                    dbContextOptionsBuilder.ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>();

                    return new AimrankContext(dbContextOptionsBuilder.Options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }

        private void LoadIdentityCore(ContainerBuilder builder)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddIdentityCore<User>()
                .AddEntityFrameworkStores<AimrankContext>();
            builder.Populate(serviceCollection);
        }
    }
}