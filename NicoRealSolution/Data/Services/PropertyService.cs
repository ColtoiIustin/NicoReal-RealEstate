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
            _context.Properties.Add(newProperty);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Properties.FirstOrDefaultAsync(x => x.Id == id);
            _context.Properties.Remove(result);
            await _context.SaveChangesAsync();
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

        public async Task UpdateProperty( Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
            
        }
    }
}
