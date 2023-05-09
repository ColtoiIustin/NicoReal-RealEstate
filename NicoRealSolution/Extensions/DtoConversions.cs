using NicoRealSolution.DTOs;
using NicoRealSolution.Models;

namespace NicoRealSolution.Extensions
{
    public static class DtoConversions
    {

        public static IEnumerable<PropertyDTO> ConvPropToDTOs(this IEnumerable<Property> properties)
        {
            return (from p in properties
                    select new PropertyDTO
                    {
                        Id = p.Id,
                        TitleEN = p.TitleEN,
                        TitleRO = p.TitleRO,
                        TitleDE = p.TitleDE,
                        DescriptionEN = p.DescriptionEN,
                        DescriptionRO = p.DescriptionRO,
                        DescriptionDE = p.DescriptionDE,
                        FeaturesEN = p.FeaturesEN,
                        FeaturesRO = p.FeaturesRO,
                        FeaturesDE = p.FeaturesDE,
                        Price = p.Price,
                        Surface= p.Surface,
                        DatePosted = p.DatePosted,
                        Country = p.Country,
                        City = p.City,
                        Address = p.Address,
                        CategoryEN = p.Category.CategEN,
                        CategoryRO = p.Category.CategRO,
                        CategoryDE = p.Category.CategDE,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,

                    }).ToList();

        }
    }
}
