using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Infrastructure.Data;
using Aimrank.Web.Common.Infrastructure;
using Autofac;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.DataAccess
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
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<UserAccessContext>();
                    dbContextOptionsBuilder
                        .UseNpgsql(_databaseConnectionString, x => x.MigrationsAssembly("Aimrank.Web.Database.Migrator"))
                        .UseSnakeCaseNamingConvention();
                    dbContextOptionsBuilder.ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>();

                    return new UserAccessContext(dbContextOptionsBuilder.Options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}