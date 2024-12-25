using System.Collections.Generic; 
using System.Linq; 
using Microsoft.EntityFrameworkCore; 
using FyakselA.UI.Models; 
using FyakselA.UI.Data; 

namespace FyakselA.UI.Services
{
   
    public interface ISuperService
    {
        void DoSomething();
    }

    public class SuperService : ISuperService
    {
        private readonly ApplicationDbContext _context; 

        public SuperService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DoSomething()
        {
            
            List<Book> books = _context.Books.ToList(); 

            
            Console.WriteLine("Список книг:");
            foreach (var book in books)
            {
                Console.WriteLine(book.Name);
            }

           
            var authorCounts = books
                .GroupBy(b => b.Author) 
                .Select(g => new
                {
                    Author = g.Key,
                    Count = g.Count() 
                });

            
            Console.WriteLine("\nКоличество книг по авторам:");
            foreach (var authorCount in authorCounts)
            {
                Console.WriteLine($"Автор: {authorCount.Author}, Количество книг: {authorCount.Count}");
            }
        }
    }
}


