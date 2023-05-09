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

        public static PropertyDTO ConvertToDTO(this Property p)
        {
            return new PropertyDTO
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
                Surface = p.Surface,
                DatePosted = p.DatePosted,
                Country = p.Country,
                City = p.City,
                Address = p.Address,
                CategoryEN = p.Category.CategEN,
                CategoryRO = p.Category.CategRO,
                CategoryDE = p.Category.CategDE,
                Latitude = p.Latitude,
                Longitude = p.Longitude,

            };

        }

        public static Property ConvertFromDTO(this PropertyDTO dto)
        {

            return new Property
            {

                TitleEN = dto.TitleEN,
                TitleRO = dto.TitleRO,
                TitleDE = dto.TitleDE,
                DescriptionEN = dto.DescriptionEN,
                DescriptionRO = dto.DescriptionRO,
                DescriptionDE = dto.DescriptionDE,
                FeaturesEN = dto.FeaturesEN,
                FeaturesRO = dto.FeaturesRO,
                FeaturesDE = dto.FeaturesDE,
                Price = dto.Price,
                Surface= dto.Surface,
                DatePosted = dto.DatePosted,
                Country = dto.Country,
                City = dto.City,
                Address = dto.Address,
                Category = new Category
                {
                    CategEN = dto.CategoryEN,
                    CategRO = dto.CategoryRO,
                    CategDE = dto.CategoryDE
                },
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,

            };
        }
    }
}
