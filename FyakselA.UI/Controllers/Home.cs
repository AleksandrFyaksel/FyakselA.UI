using Microsoft.AspNetCore.Mvc;

namespace FyakselA.UI.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
