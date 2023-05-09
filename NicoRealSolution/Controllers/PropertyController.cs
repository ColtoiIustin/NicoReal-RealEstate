using Microsoft.AspNetCore.Mvc;
using NicoRealSolution.Data.Services;
using NicoRealSolution.Extensions;

namespace NicoRealSolution.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propService;
        public PropertyController(IPropertyService propService)
        {
            _propService= propService;
        }
        public async Task<IActionResult> Index()
        {    
            var properties = await _propService.GetProperties();
            var propertyDTOs = properties.ConvPropToDTOs();
            return View(propertyDTOs);
      
        }
    }
}
