using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetMatchesHistory;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aimrank.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAimrankModule _aimrankModule;

        public HomeController(IAimrankModule aimrankModule)
        {
            _aimrankModule = aimrankModule;
        }

        public async Task<IActionResult> Index()
        {
            var matches = await _aimrankModule.ExecuteQueryAsync(new GetMatchesHistoryQuery());
            
            return View(matches);
        }
    }
}