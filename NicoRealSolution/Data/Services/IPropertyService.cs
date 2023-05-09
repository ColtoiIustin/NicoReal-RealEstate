

using NicoRealSolution.Models;

namespace NicoRealSolution.Data.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(int id);
        Task AddAsync(Property property);
        Task<Property> UpdateAsync( Property newProperty);
        Task DeleteAsync(int id);
        
    }
}
