using Aimrank.Modules.UserAccess.Application.Services;
using Microsoft.AspNetCore.Http;
using System;

namespace Aimrank.Web.Configuration.UrlFactory
{
    public class ApplicationUrlFactory : IUrlFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationUrlFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CreateEmailConfirmationLink(Guid userId, string token)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            
            return $"{request.Scheme}://{request.Host.ToUriComponent()}/api/user/verification?userId={userId}&token={token}";
        }
    }
}