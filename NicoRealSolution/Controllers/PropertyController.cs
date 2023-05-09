using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICategoryService _categService;
        public PropertyController(IPropertyService propService, ICategoryService categService)
        {
            _propService = propService;
            _categService = categService; 
        }
        public async Task<IActionResult> Index()
        {    
            var properties = await _propService.GetProperties();
            var propertyDTOs = properties.ConvPropToDTOs();
            return View(propertyDTOs);
      
        }

        public async Task<IActionResult> Details(int id)
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

        public async Task<IActionResult> Delete(int id)
        {
            var property = await _propService.GetByIdAsync(id);
            if (property == null) return View("NotFound");

            await _propService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> EditPage(int id)
        {
            var property = await _propService.GetByIdAsync(id);
            if (property == null) return View("NotFound");
            var result = property.ConvertToDTO();
            return View(property);

        }

        public async Task<IActionResult> ConfirmEdit([FromBody] PropertyUpdateDTO newDTO)
        {
            if (!ModelState.IsValid)
            {
                return View("EditPage", newDTO);
            }

            await _propService.UpdateProperty(newDTO);          
            return RedirectToAction(nameof(Details), new { id = newDTO.Id });

        }

        public async Task<IActionResult> CategoryPage()
        {
            var categories = await _categService.GetCategories();
            
            return View(categories);
           
        }


    }
}
