using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GR30323.Domain.Entities;

namespace FyakselA.UI.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;

        public EditModel(ICategoryService categoryService, IBookService bookService)
        {
            _categoryService = categoryService;
            _bookService = bookService;
        }

        [BindProperty]
        public Book Book { get; set; } = new Book(); // Инициализация по умолчанию
        public string? ErrorMessage { get; set; }
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await _bookService.GetBookByIdAsync(id.Value);
            if (response == null || !response.Success || response.Data == null)
            {
                return NotFound();
            }
            Book = response.Data;

            var categoryListData = await _categoryService.GetCategoryListAsync();
            ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _bookService.UpdateBookAsync(id.Value, Book, Image);
            return RedirectToPage("./Index");
        }
    }
}