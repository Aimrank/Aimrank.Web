using Microsoft.AspNetCore.Mvc;

namespace Aimrank.Web.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}