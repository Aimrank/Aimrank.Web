using Aimrank.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var matches = await _context.Matches.OrderByDescending(m => m.CreatedAt).ToListAsync();
            
            return View(matches);
        }
    }
}