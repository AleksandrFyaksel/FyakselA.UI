using FyakselA.UI.Session;
using GR30323.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GR30323.API.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookingService _bookingService; 

        //public CartController(IBookingService bookingService) 
        //{
        //    _bookingService = bookingService; 
        //}

        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
            return View(cart.CartItems);
        }

        [Route("[controller]/add/{id:int}")]
        public async Task<ActionResult> Add(int id, string returnUrl)
        {
            var data = await _bookingService.GetProductByIdAsync(id); 
            if (data.Success)
            {
                var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
                cart.AddToCart(data.Data);
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