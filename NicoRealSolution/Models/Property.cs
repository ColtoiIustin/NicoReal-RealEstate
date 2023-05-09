﻿namespace NicoRealSolution.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleRO { get; set; }
        public string TitleDE { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionRO { get; set; }
        public string DescriptionDE { get; set; }
        public string FeaturesEN { get; set; }
        public string FeaturesRO { get; set; }
        public string FeaturesDE { get; set; }
        public decimal Price { get; set; }
        public int Surface { get; set; }
        public DateTime DatePosted { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int CategoryId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }


        //navigation property
        public Category Category { get; set; }
    }
}
