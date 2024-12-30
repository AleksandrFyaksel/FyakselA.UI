using GR30323.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace FyakselA.UI.Controllers
{
    public class ProductController : Controller
    {
        IEnumerable<Book> books2;
        public IActionResult Index()
        {
            books2 = [
                new Book{ BookId=1, Name="Idiot", Author="Dostoevskiy"},
                new Book{ BookId=2, Name="DogHeart", Author="Bulgakov"},
                new Book{ BookId=3, Name="Foust", Author="Gyote"}
                ];
            return View(books2.First());
        }
    }
}
