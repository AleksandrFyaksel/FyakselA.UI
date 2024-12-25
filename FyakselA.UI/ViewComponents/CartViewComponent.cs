using Microsoft.AspNetCore.Mvc;
using FyakselA.UI.Models;
using GR30323.Domain.Entities; 

namespace FyakselA.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            
            var cart = HttpContext.Session.Get<Cart>("cart"); 

           
            if (cart == null)
            {
                cart = new Cart(); 
            }

            return View(cart); 
        }
    }
}