using Aimrank.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace Aimrank.Web.ViewComponents
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
                var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
                var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "email");
                var username = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name");

                if (id is not null && email is not null && username is not null)
                {
                    model.User = new InitialAppStateUser
                    {
                        Id = Guid.Parse(id.Value).ToString("N"),
                        Email = email.Value,
                        Username = username.Value
                    };
                }
            }
        }
    }
}