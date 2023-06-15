using System.ComponentModel.DataAnnotations;

namespace NicoRealSolution.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? IsInvestment { get; set; }
        public string? IsFeatured { get; set; }
        public string? Description { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Introduceti un numar cu pana la 2 cifre dupa virgula('.')")]
        public decimal? Price { get; set; }

        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Introduceti un numar natural")]
        public int? Surface { get; set; }

        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Introduceti un numar natural")]
        public int? GardenSurface { get; set; }
        public string? DatePosted { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Introduceti un numar natural")]
        public int? Rooms { get; set; }

        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Introduceti un numar natural")]
        public int? Bathrooms { get; set; }
        public string? ConstructionYear { get; set; }

        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Introduceti un numar natural")]
        public int? Floors { get; set; }
        public string? Features { get; set; }
        public string? PhotoGuids { get; set; }
        public string? Nearby1 { get; set; }
        public string? Nearby2 { get; set; }
        public string? Nearby3 { get; set; }
        public string? IsSold { get; set; }



    }
}
