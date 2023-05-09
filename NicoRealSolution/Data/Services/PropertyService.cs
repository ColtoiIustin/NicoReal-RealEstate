using Microsoft.EntityFrameworkCore;
using NicoRealSolution.Models;

namespace NicoRealSolution.Data.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly AppDbContext _context;
        public PropertyService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddProperty(Property newProperty)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.CategEN == newProperty.Category.CategEN);
            newProperty.CategoryId = existingCategory.Id;
            _context.Properties.Add(newProperty);
            await _context.SaveChangesAsync();
            
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetProperties()
        {
            var properties = await _context.Properties.Include(p => p.Category).ToListAsync();
            return properties;
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            var property = await _context.Properties.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            return property;
        }

        public Task<Property> UpdateAsync(Property newProperty)
        {
            throw new NotImplementedException();
        }
    }
}
