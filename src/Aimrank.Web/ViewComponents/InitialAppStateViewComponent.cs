using Aimrank.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

            return View(model);
        }
    }
}