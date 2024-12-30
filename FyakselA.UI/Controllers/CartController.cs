using FyakselA.UI.Services;
using GR30323.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FyakselA.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookService _bookService; 

        public CartController(IBookService bookService)
        {
            _bookService = bookService; 
        }

        // GET: Cart
        public IActionResult Index()
        {
            
            var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
            return View(cart.CartItems); 
        }

        [Route("[controller]/add/{id:int}")]
        public async Task<ActionResult> Add(int id, string returnUrl)
        {
            
            var book = await _bookService.GetBookByIdAsync(id);
            if (book != null) 
            {
                
                var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
                
                cart.AddToCart(book);
              
                HttpContext.Session.Set("cart", cart);
            }
            return Redirect(returnUrl); 
        }

        [Route("[controller]/remove/{id:int}")]
        public ActionResult Remove(int id)
        {
            
            var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
            
            cart.RemoveItems(id);
            
            HttpContext.Session.Set("cart", cart);
            return RedirectToAction("Index"); 
        }
    }
}