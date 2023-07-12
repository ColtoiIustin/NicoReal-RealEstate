using NicoRealSolution.Models;

namespace NicoRealSolution.ViewModels
{
    public class ViewModelDetails
    {
        public Property SingleProp { get; set; }
        public IEnumerable<Property> PropList { get; set; }
        public string Language { get; set; }

    }
}
