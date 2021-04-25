using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Web.Common.Infrastructure
{
    public interface IModuleStartup
    {
        void Register(IServiceCollection services);
        void Initialize(IApplicationBuilder builder, IConfiguration configuration);
    }
}