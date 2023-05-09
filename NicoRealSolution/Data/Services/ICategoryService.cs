using NicoRealSolution.Models;

namespace NicoRealSolution.Data.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> AddCategory(Category newCategory);
        Task<Category> UpdateCategory(Category newCategory);
        Task<Category> DeleteCategory(int id);
    }
}
