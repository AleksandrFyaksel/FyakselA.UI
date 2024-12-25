using Microsoft.AspNetCore.Mvc;

namespace FyakselA.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public IActionResult Index()
        {
            return View(); 
        }
    }
}