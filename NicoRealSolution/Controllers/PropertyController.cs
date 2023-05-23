using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NicoRealSolution.Data.Services;
using NicoRealSolution.Models;
using System.Linq.Expressions;

namespace NicoRealSolution.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propService;

        public PropertyController(IPropertyService propService)
        {
            _propService = propService;
        }
        public async Task<IActionResult> Index()
        {    
            var properties = await _propService.GetProperties();
            return View(properties);
      
        }

        public async Task<IActionResult> Details(int id)
        {
            var property = await _propService.GetByIdAsync(id);       
            return View(property); 
            
        }

        public async Task<IActionResult> Create()
        {   
            return View();
        }

        
        public async Task<IActionResult> AddProperty(Property property)
        {

            if (!ModelState.IsValid)
            {
                return View("Create", property);
            }
            await _propService.AddProperty(property);

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
            return View(property);

        }

        public async Task<IActionResult> ConfirmEdit( Property property)
        {
            if (!ModelState.IsValid)
            {
                return View("EditPage", property);
            }

            await _propService.UpdateProperty(property);          
            return RedirectToAction(nameof(Details), new { id = property.Id });

        }



    }
}
