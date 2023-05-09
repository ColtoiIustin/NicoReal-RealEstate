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
        public Task AddAsync(Property property)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Property>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Property> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Property> UpdateAsync(Property newProperty)
        {
            throw new NotImplementedException();
        }
    }
}
