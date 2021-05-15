using Aimrank.Web.Common.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System;

namespace Aimrank.Web.App.Configuration
{
    public static class ModulesExtensions
    {
        private static List<IModuleStartup> _modules;

        private static IEnumerable<IModuleStartup> Modules
        {
            get
            {
                if (_modules is not null)
                {
                    return _modules;
                }

                LoadAssemblies();
                LoadModules();

                return _modules;
            }
        }
        
        public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
        {
            foreach (var module in Modules)
            {
                module.Register(services, configuration);
            }

            return services;
        }

        public static IApplicationBuilder UseModules(this IApplicationBuilder builder, IConfiguration configuration)
        {
            foreach (var module in Modules)
            {
                module.Initialize(builder, configuration);
            }
            
            return builder;
        }
        
        private static void LoadAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var locations = assemblies.Where(a => !a.IsDynamic).Select(a => a.Location).ToList();
            Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .ToList()
                .ForEach(x =>
                {
                    try
                    {
                        AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x));
                    }
                    catch
                    {
                    }
                });
        }

        private static void LoadModules()
        {
            _modules = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IModuleStartup).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IModuleStartup>()
                .ToList();
        }
    }
}