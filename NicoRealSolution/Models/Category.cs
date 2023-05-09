namespace NicoRealSolution.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategEN { get; set; }
        public string CategRO { get; set; }
        public string CategDE { get; set; }


        //navigation property
        public ICollection<Property> Properties { get; set; }
    }
}
