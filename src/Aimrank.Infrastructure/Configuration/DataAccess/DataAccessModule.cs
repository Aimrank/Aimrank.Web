using Aimrank.Common.Application.Data;
using Aimrank.Common.Infrastructure;
using Aimrank.Infrastructure.Application.Data;
using Aimrank.Infrastructure.Domain.Users;
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
        }

        private void LoadIdentityCore(ContainerBuilder builder)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddIdentityCore<UserModel>()
                .AddEntityFrameworkStores<AimrankContext>();
            builder.Populate(serviceCollection);
        }
    }
}