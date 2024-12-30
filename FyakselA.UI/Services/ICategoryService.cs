using GR30323.Domain.Entities;
using GR30323.Domain.Models;

namespace FyakselA.UI.Services
{
    public interface ICategoryService
    {
            Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
