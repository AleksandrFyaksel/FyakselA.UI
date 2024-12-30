using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FyakselA.UI.Services;
using GR30323.Domain.Entities;
using GR30323.Domain.Models;

namespace FyakselA.UI.Services
{
    public class MemoryBookService : IBookService
    {
        private List<GR30323.Domain.Entities.Book> _books;
        private List<Category> _categories;

        public MemoryBookService(ICategoryService categoryService)
        {
            InitializeAsync(categoryService).Wait();
        }

        private async Task InitializeAsync(ICategoryService categoryService)
        {
            _categories = (await categoryService.GetCategoryListAsync()).Data;
            SetupData();
        }

        private void SetupData()
        {
            _books = new List<GR30323.Domain.Entities.Book>
            {
                new GR30323.Domain.Entities.Book { Id = 1, Title = "Властелин колец", Author = "Дж. Р. Р. Толкин", Description = "Эпическая история о кольце власти.", Price = 500, Image = "Images/Властелин_колец.jpg", CategoryId = _categories.Find(c => c.NormalizedName.Equals("fantasy"))?.Id ?? 0 },
                new GR30323.Domain.Entities.Book { Id = 2, Title = "1984", Author = "Джордж Оруэлл", Description = "Дистопия о тоталитарном обществе.", Price = 300, Image = "Images/1984.jpg", CategoryId = _categories.Find(c => c.NormalizedName.Equals("science"))?.Id ?? 0 },
                new GR30323.Domain.Entities.Book { Id = 3, Title = "Гарри Поттер и философский камень", Author = "Дж. К. Роулинг", Description = "Приключения молодого волшебника.", Price = 400, Image = "Images/Гарри_Поттер.jpg", CategoryId = _categories.Find(c => c.NormalizedName.Equals("fantasy"))?.Id ?? 0 },
                new GR30323.Domain.Entities.Book { Id = 4, Title = "Приключения Шерлока Холмса", Author = "Артур Конан Дойл", Description = "Истории о знаменитом детективе.", Price = 350, Image = "Images/Шерлок_Холмс.jpg", CategoryId = _categories.Find(c => c.NormalizedName.Equals("novel"))?.Id ?? 0 }
            };
        }

        public async Task<List<GR30323.Domain.Entities.Book>> GetBookListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var filteredBooks = string.IsNullOrEmpty(categoryNormalizedName)
                ? _books
                : _books.Where(b => b.CategoryId == _categories.Find(c => c.NormalizedName.Equals(categoryNormalizedName))?.Id).ToList();

            return await Task.FromResult(filteredBooks);
        }

        public async Task CreateBookAsync(Book book)
        {
            
            _books.Add(book);
            await Task.CompletedTask;
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var bookToRemove = _books.FirstOrDefault(b => b.Id == bookId);
            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
            }
            await Task.CompletedTask; 
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            return await Task.FromResult(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Description = book.Description;
                existingBook.Price = book.Price;
                existingBook.Image = book.Image;
                existingBook.CategoryId = book.CategoryId;
            }
            await Task.CompletedTask; 
        }

        public Task CreateBookAsync(Book book, IFormFile? image)
        {
            throw new NotImplementedException();
        }
    }
}