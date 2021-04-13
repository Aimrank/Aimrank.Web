using Autofac;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration.DataAccess
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
            builder.Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<CSGOContext>();
                    dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString,
                        x => x.MigrationsAssembly("Aimrank.Database.Migrator"));

                    return new CSGOContext(dbContextOptionsBuilder.Options);
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