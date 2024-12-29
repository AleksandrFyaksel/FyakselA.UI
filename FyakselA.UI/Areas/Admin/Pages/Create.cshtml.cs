using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GR30323.Domain.Entities;

namespace FyakselA.UI.Areas.Admin.Pages
{
    public class CreateModel(ICategoryService categoryService, IBookService productService) : PageModel
    {
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IBookService _productService = productService;

        [BindProperty]
        public Book Book { get; set; } = new Book(); // Инициализация по умолчанию

        [BindProperty]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var categoryListData = await _categoryService.GetCategoryListAsync();
            ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _productService.CreateBookAsync(Book, Image);
            return RedirectToPage("./Index");
        }
    }
}