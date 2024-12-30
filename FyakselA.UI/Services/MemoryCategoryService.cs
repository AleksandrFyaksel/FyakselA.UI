using GR30323.Domain.Entities;
using GR30323.Domain.Models;

namespace FyakselA.UI.Services
{      public class MemoryCategoryService : ICategoryService
        {
            public Task<ResponseData<List<Category>>> GetCategoryListAsync()
            {
                var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Фантастика", NormalizedName = "fantasy" },
                new Category { Id = 2, Name = "Научная литература", NormalizedName = "science" },
                new Category { Id = 3, Name = "Роман", NormalizedName = "novel" },
                new Category { Id = 4, Name = "Детская литература", NormalizedName = "children" }
            };

                var result = new ResponseData<List<Category>> { Data = categories };
                return Task.FromResult(result);
            }
        }

}
