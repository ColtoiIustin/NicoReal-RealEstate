﻿namespace NicoRealSolution.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public bool IsInvestment { get; set; }  
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Surface { get; set; }
        public DateTime DatePosted { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Features { get; set; }


    }
}
