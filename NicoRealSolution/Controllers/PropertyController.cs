using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NicoRealSolution.Data.Services;
using NicoRealSolution.DTOs;
using NicoRealSolution.Extensions;
using System.Linq.Expressions;

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

        public async Task<IActionResult> Property(int id)
        {
            var property = await _propService.GetByIdAsync(id);       
            var propertyDTO = property.ConvertToDTO();
            return View(propertyDTO);
            
            
        }

        public async Task<IActionResult> Create([FromBody] PropertyDTO properyDTO)
        {

            if (!ModelState.IsValid)
            {
                return View("Create", properyDTO);
            }
            var newProperty = properyDTO.ConvertFromDTO();

            await _propService.AddProperty(newProperty);

            return RedirectToAction(nameof(Index));

        }
    }
}
