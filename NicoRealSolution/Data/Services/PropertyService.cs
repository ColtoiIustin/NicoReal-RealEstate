using Microsoft.EntityFrameworkCore;
using NicoRealSolution.Models;
using System.Linq.Expressions;


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
            newProperty.DatePosted = DateTime.Today.ToString("dd.MM.yyyy");
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
            var properties = await _context.Properties.ToListAsync();
            return properties;
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(p => p.Id == id);
            return property;
        }

        public async Task UpdateProperty( Property property)
        {
            try
            {
                _context.Properties.Update(property);
                await _context.SaveChangesAsync();
            }
            catch ( Exception ex) { }
            
        }


        public async Task<Dictionary<string, int>> CategCountMap()
        {
            Dictionary<string, int> categoryCountMap = new Dictionary<string, int>
            {
                { "Casa", 0 },
                { "Apartament", 0 },
                { "Teren", 0 },
                { "Investitie", 0 },
                { "Hotel", 0 }
            };

            var products = await _context.Properties.ToListAsync();

            foreach (var product in products)
            {
                if (product.Category != null)
                {
                    string category = product.Category;

                    if (categoryCountMap.ContainsKey(category))
                    {
                        categoryCountMap[category]++;
                    }
                }
                if (product.IsInvestment == "Da")
                {
                    categoryCountMap["Investitie"]++;
                }

            }

            return categoryCountMap;
        }
    }
}
