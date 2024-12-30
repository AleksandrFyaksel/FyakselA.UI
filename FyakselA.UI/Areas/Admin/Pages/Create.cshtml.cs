using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GR30323.Domain.Entities;
using System.IO;
using System.Threading.Tasks;
using FyakselA.UI.Services;

namespace FyakselA.UI.Areas.Admin.Pages
{
    public class CreateModel(ICategoryService categoryService, IBookService productService) : PageModel
    {
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IBookService _productService = productService;

        [BindProperty]
        public Book Book { get; set; } = new Book(); 

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

            
            if (Image != null)
            {
                var filePath = Path.Combine("wwwroot/images", Image.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                Book.Image = "/images/" + Image.FileName; 
            }

            await _productService.CreateBookAsync(Book, Image);
            return RedirectToPage("./Index");
        }
    }
}