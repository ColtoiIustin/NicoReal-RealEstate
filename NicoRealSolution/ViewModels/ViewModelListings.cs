using NicoRealSolution.Models;

namespace NicoRealSolution.ViewModels
{
    public class ViewModelListings
    {
        public IEnumerable<Property> PropList { get; set; }
        public string SelectCateg { get; set; }
        public string SelectLocation { get; set; }
       
    }
}
