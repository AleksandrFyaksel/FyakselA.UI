using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GR30323.API.Data;
using GR30323.Domain.Entities;
using GR30323.Domain.Models;

namespace GR30323.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BooksController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<ResponseData<ListModel<Book>>>> GetBooks(string? category, int pageNo = 1, int pageSize = 3)
        {
            var result = new ResponseData<ListModel<Book>>();
            var data = _context.Books.Include(d => d.Category)
                .Where(d => string.IsNullOrEmpty(category) || d.Category.NormalizedName.Equals(category));

            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);
            if (pageNo > totalPages) pageNo = totalPages;

            var listData = new ListModel<Book>()
            {
                Items = await data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            result.Data = listData;

            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }

            return result;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseData<Book>>> GetBook(int id)
        {
            var result = new ResponseData<Book>();
            var book = await _context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);

            result.Data = book;

            if (result.Data == null)
            {
                result.Success = false;
                result.ErrorMessage = "Данные не найдены";
            }

            return result;
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            // Удалить старый файл, если он существует
            if (!string.IsNullOrEmpty(book.Image))
            {
                var oldFileName = Path.GetFileName(new Uri(book.Image).LocalPath);
                var oldFilePath = Path.Combine(imagesPath, oldFileName);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Books/{id}/save-image
        [HttpPost("{id}/save-image")]
        public async Task<IActionResult> SaveImage(int id, IFormFile image)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            var randomName = Path.GetRandomFileName();
            var extension = Path.GetExtension(image.FileName);
            var fileName = Path.ChangeExtension(randomName, extension);
            var filePath = Path.Combine(imagesPath, fileName);

            using var stream = System.IO.File.OpenWrite(filePath);
            await image.CopyToAsync(stream);

            var host = "https://" + Request.Host;
            var url = $"{host}/Images/{fileName}";
            book.Image = url;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Books/{id}/upload-image
        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UpdateImage(int id, IFormFile image)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            if (!string.IsNullOrEmpty(book.Image))
            {
                var oldFileName = Path.GetFileName(new Uri(book.Image).LocalPath);
                var oldFilePath = Path.Combine(imagesPath, oldFileName);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            var randomName = Path.GetRandomFileName();
            var extension = Path.GetExtension(image.FileName);
            var fileName = Path.ChangeExtension(randomName, extension);
            var filePath = Path.Combine(imagesPath, fileName);

            using var stream = System.IO.File.OpenWrite(filePath);
            await image.CopyToAsync(stream);

            var host = "https://" + Request.Host;
            var url = $"{host}/Images/{fileName}";
            book.Image = url;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}