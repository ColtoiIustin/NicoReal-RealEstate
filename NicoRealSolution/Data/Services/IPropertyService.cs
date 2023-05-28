
using NicoRealSolution.Models;

namespace NicoRealSolution.Data.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetProperties();
        Task<Property> GetByIdAsync(int id);
        Task AddProperty(Property property);
        Task UpdateProperty(Property newProperty);
        Task DeleteAsync(int id);


    }
}
