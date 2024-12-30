using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GR30323.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using FyakselA.UI.Services;

namespace FyakselA.UI.Areas.Admin.Pages
{
    [Authorize(Policy = "admin")]

    public class IndexModel : PageModel
    {
        private readonly IBookService _bookService;

        public IndexModel(IBookService bookService) => _bookService = bookService;

        public List<Book> Book { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public async Task OnGetAsync(int? pageNo = 1)
        {
            var response = await _bookService.GetBookListAsync(null, pageNo.Value);
            if (response.Success)
            {
                Book = response.Data.Items;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }
        }
    }
}
