    using global::GR30323.Blazor.Services;
    using GR30323.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace GR30323.Blazor.Services
    {
        public class BookService : IBookService<Book>
        {
            private List<Book> _books;
            private int _currentPage;
            private const int _pageSize = 3;

            public BookService()
            {
                
                _books = new List<Book>
            {
                new Book { Id = 1, Title = "Книга 1", Description = "Описание 1", Price = 100, Author = "Автор 1" },
                new Book { Id = 2, Title = "Книга 2", Description = "Описание 2", Price = 200, Author = "Автор 2" },
                new Book { Id = 3, Title = "Книга 3", Description = "Описание 3", Price = 300, Author = "Автор 3" },
                new Book { Id = 4, Title = "Книга 4", Description = "Описание 4", Price = 400, Author = "Автор 4" },
                new Book { Id = 5, Title = "Книга 5", Description = "Описание 5", Price = 500, Author = "Автор 5" }
            };
                _currentPage = 1; 
            }

            public event Action ListChanged;

            public IEnumerable<Book> Books => _books.Skip((_currentPage - 1) * _pageSize).Take(_pageSize);

            public int CurrentPage => _currentPage;

            public int TotalPages => (int)Math.Ceiling((double)_books.Count / _pageSize);

            public async Task GetBooks(int pageNo = 1, int pageSize = 3)
            {
                
                await Task.Delay(100);
                _currentPage = pageNo;

               
                ListChanged?.Invoke();
            }
        }
    }
}
