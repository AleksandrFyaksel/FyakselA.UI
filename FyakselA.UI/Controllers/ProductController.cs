using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FyakselA.UI.Models;

namespace FyakselA.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IEnumerable<Book> _books; 

        public ProductController() 
        {
            
            _books = new List<Book>
            {
                new Book { BookId = 1, Name = "Евгений Онегин", Author = "Александр Пушкин" },
                new Book { BookId = 2, Name = "Герой нашего времени", Author = "Михаил Лермонтов" },
                new Book { BookId = 3, Name = "Реквием", Author = "Анна Ахматова" }
            };
        }

        public IActionResult Index()
        {
            return View(_books); 
        }

        public IActionResult Details(int id)
        {
            var book = _books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound(); 
            }
            return View(book); 
        }
    }
}