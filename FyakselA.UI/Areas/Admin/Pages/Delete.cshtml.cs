using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GR30323.Domain.Entities;

namespace FyakselA.UI.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IBookService _bookService;

        public DeleteModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        [BindProperty]
        public Book Book { get; set; } = new Book(); // Инициализация по умолчанию

        public string? ErrorMessage { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _bookService.DeleteBookAsync(id.Value);
            return RedirectToPage("./Index");
        }
    }
}