using GR30323.Domain.Entities;

namespace FyakselA.UI.Services
{
    public interface IBookService
{
    Task<List<Book>> GetBookListAsync(string? categoryNormalizedName, int pageNo);
    Task CreateBookAsync(Book book, IFormFile? image);
    Task DeleteBookAsync(int bookId);
    Task<Book> GetBookByIdAsync(int bookId);
    Task UpdateBookAsync(int value, Book book, IFormFile? image);
}
}