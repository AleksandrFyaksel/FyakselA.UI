using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FyakselA.UI.Models;
using FyakselA.UI.Data;
using Microsoft.Extensions.Logging;
using FyakselA.UI.Services;
using GR30323.Domain.Entities;

namespace FyakselA.UI.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public BooksController(ApplicationDbContext context, ILogger<BooksController> logger, IBookService bookService, ICategoryService categoryService)
        {
            _context = context;
            _logger = logger;
            _bookService = bookService;
            _categoryService = categoryService;
        }

        // GET: Books
        public async Task<IActionResult> Index(string? category, int pageNo = 1) 
        {
            var bookResponse = await _bookService.GetBookListAsync(category, pageNo);
            if (!bookResponse.Success)
                return NotFound(bookResponse.ErrorMessage);

            return View(bookResponse.Data.Items);
        }

        private IActionResult NotFound(object errorMessage)
        {
            throw new NotImplementedException();
        }

        private IActionResult View(object items)
        {
            throw new NotImplementedException();
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Name,Author")] Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Ошибка при создании книги: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "Произошла ошибка при создании книги. Пожалуйста, попробуйте еще раз.");
                }
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Name,Author")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}