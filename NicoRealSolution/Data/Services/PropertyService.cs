using DTOs;
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

        public async Task UpdateProperty(PropertyUpdateDTO newDTO)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.Id == newDTO.CategoryId);
            var editProperty = new Property
            {
                Id = newDTO.Id,
                TitleEN = newDTO.TitleEN,
                TitleRO = newDTO.TitleRO,
                TitleDE = newDTO.TitleDE,
                DescriptionEN = newDTO.DescriptionEN,
                DescriptionRO = newDTO.DescriptionRO,
                DescriptionDE = newDTO.DescriptionDE,
                FeaturesEN = newDTO.FeaturesEN,
                FeaturesRO = newDTO.FeaturesRO,
                FeaturesDE = newDTO.FeaturesDE,
                Price = newDTO.Price,
                DatePosted = newDTO.DatePosted,
                Country = newDTO.Country,
                City = newDTO.City,
                Address = newDTO.Address,
                Latitude = newDTO.Latitude,
                Longitude = newDTO.Longitude,
                Category = existingCategory,
                CategoryId = existingCategory.Id
            };


            _context.Properties.Update(editProperty);
            await _context.SaveChangesAsync();
            
        }
    }
}
