using System;
using System.Collections.Generic; 
using System.Linq; 
using FyakselA.UI.Models; 
using FyakselA.UI.Data;
using GR30323.Domain.Entities;


namespace FyakselA.UI.Services
{
   
    public class TestService : ISuperService
    {
        private readonly ApplicationDbContext _context; 

        public TestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DoSomething()
        {
            Console.WriteLine("TestService is doing something.");

            List<Book> books = _context.Books.ToList(); 

            Console.WriteLine("Список книг в TestService:");
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }
    }

    public class ProductionService : ISuperService
    {
        private readonly ApplicationDbContext _context; 

        public ProductionService(ApplicationDbContext context)
        {
            _context = context;
        }

     
        public void DoSomething()
        {
            
            Console.WriteLine("ProductionService is doing something.");

           
            List<Book> books = _context.Books.ToList(); 

            Console.WriteLine("Список книг в ProductionService:");
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }
    }
}