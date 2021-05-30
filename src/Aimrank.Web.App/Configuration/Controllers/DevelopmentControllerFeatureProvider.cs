using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Aimrank.Web.App.Configuration.Controllers
{
    public class DevelopmentControllerFeatureProvider : ControllerFeatureProvider
    {
        private readonly IConfiguration _configuration;

        public DevelopmentControllerFeatureProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override bool IsController(TypeInfo typeInfo)
        {
            if (!base.IsController(typeInfo))
            {
                return false;
            }

            if (typeInfo.IsDefined(typeof(DevelopmentControllerAttribute)))
            {
                return _configuration.IsDevelopment() || _configuration.IsDocker();
            }
            
            return true;
        }
    }
}