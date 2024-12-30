using FyakselA.UI.Models;
using FyakselA.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using GR30323.Domain.Entities;

namespace FyakselA.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List<ListDemo> _listData;
        private readonly ISuperService _superService; 
        private readonly IEnumerable<Book> books; 

        public HomeController(ILogger<HomeController> logger, ISuperService service)
        {
            _logger = logger;
            _superService = service; 

            
            books = new List<Book>
            {
                new Book { Id = 1, Title = "Book1", Author = "Author1" },
                new Book { Id = 2, Title = "Book2", Author = "Author2" },
                new Book { Id = 3, Title = "Book3", Author = "Author3" }
            };

            
            _listData = new List<ListDemo>
            {
                new ListDemo { Id = 1, Name = "Item 1" },
                new ListDemo { Id = 2, Name = "Item 2" },
                new ListDemo { Id = 3, Name = "Item 3" }
            };
        }

        public IActionResult Index()
        {
            ViewData["Books"] = books;
            ViewData["BooksSelect"] = new SelectList(books, "BookId", "Name");
            ViewData["text"] = "Лабораторная работа №2";

            SelectList data = new SelectList(_listData, "Id", "Name");

            // Передача данных (только одной!) модели (data) в представление (@model SelectList)
            return View(data);
        }

        //[Authorize(Policy ="admin")]
        //--- Для примера: HTML-helper, Tag-helper ----------
        public IActionResult TagHelperDemo()
        {
            return View();
        }
        //--- ------------------------------------
        public IActionResult LayoutDemo()
        {
            return View();
        }

        //-------Пример:Маршрутизация --------------
        [Route("Show")]
        [Route("Show/Page_{pageNo:int}")]
        //--- ------------------------------------
        //--- Scaffolding – автоматическая генерация представления на основании модели ------
        public IActionResult ShowBooks()
        {
            return View(books);
        }
        //--- Для примера: -------
    }

    public class ListDemo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}