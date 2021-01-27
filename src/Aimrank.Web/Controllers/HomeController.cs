using Microsoft.AspNetCore.Mvc;

namespace Aimrank.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}