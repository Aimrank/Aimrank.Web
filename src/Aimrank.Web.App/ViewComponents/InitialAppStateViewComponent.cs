using Aimrank.Web.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Security.Claims;

namespace Aimrank.Web.App.ViewComponents
{
    public class InitialAppStateViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new InitialAppStateViewModel
            {
                Error = (string) TempData["Error"]
            };
            
            AttachUserToViewModelIfAuthenticated(model);

            return View(model);
        }

        private void AttachUserToViewModelIfAuthenticated(InitialAppStateViewModel model)
        {
            var isAuthenticated = HttpContext.User.Identity?.IsAuthenticated ?? false;
            if (isAuthenticated)
            {
                var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var username = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                var roles = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value);

                if (id is not null && email is not null && username is not null)
                {
                    model.User = new InitialAppStateUser
                    {
                        Id = Guid.Parse(id.Value).ToString("N"),
                        Email = email.Value,
                        Username = username.Value,
                        Roles = roles
                    };
                }
            }
        }
    }
}