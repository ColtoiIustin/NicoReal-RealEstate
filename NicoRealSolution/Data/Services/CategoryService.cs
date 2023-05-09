using Microsoft.EntityFrameworkCore;
using NicoRealSolution.Models;

namespace NicoRealSolution.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context) 
        {
            _context = context;
        }
        public Task<Category> AddCategory(Category newCategory)
        {
            throw new NotImplementedException();
        }

        public Task<Category> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
           var categories = _context.Categories.ToListAsync();
            return categories;
        }

        public Task<Category> UpdateCategory(Category newCategory)
        {
            throw new NotImplementedException();
        }
    }
}
