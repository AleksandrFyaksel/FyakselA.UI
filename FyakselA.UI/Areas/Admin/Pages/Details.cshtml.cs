using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GR30323.Domain.Entities;
using FyakselA.UI.Services;


namespace FyakselA.UI.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IBookService _bookService;

        public DetailsModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public Book Book { get; set; } = default!;
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
                if (id == null)
                {
                    return NotFound();
                }
                var response = await _bookService.GetBookByIdAsync(id.Value);
                if (response == null)
                {
                    return NotFound();
                }
                if (response.Success)
                {
                    if (response.Data == null)
                    {
                        return NotFound();
                    }
                    Book = (Book)response.Data;
                }
                else
                {
                    ErrorMessage = response.ErrorMessage ?? "Unknown error.";
                    return Page();
                }
                return Page();
            
        }
    }
}
