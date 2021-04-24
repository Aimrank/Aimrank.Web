using Aimrank.Web.Modules.UserAccess.Application.Services;
using Microsoft.AspNetCore.Http;
using System;

namespace Aimrank.Web.App.Configuration.UrlFactory
{
    public class ApplicationUrlFactory : IUrlFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UrlFactorySettings _urlFactorySettings;

        public ApplicationUrlFactory(IHttpContextAccessor httpContextAccessor, UrlFactorySettings urlFactorySettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _urlFactorySettings = urlFactorySettings;
        }

        public string CreateEmailConfirmationLink(Guid userId, string token)
            => string.Concat(
                BasePath,
                "/api/user/verification?",
                $"userId={Uri.EscapeDataString(userId.ToString())}&",
                $"token={Uri.EscapeDataString(token)}");

        public string CreateResetPasswordLink(Guid userId, string token)
            => string.Concat(
                BasePath,
                "/reset-password?",
                $"userId={Uri.EscapeDataString(userId.ToString())}&",
                $"token={Uri.EscapeDataString(token)}");

        private string BasePath => _httpContextAccessor.HttpContext?.Request is null
            ? _urlFactorySettings.BasePath
            : $"{_httpContextAccessor.HttpContext.Request}://{_httpContextAccessor.HttpContext.Request.Host.ToUriComponent()}";
    }
}