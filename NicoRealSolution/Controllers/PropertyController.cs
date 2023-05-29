using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NicoRealSolution.Data.Services;
using NicoRealSolution.Models;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;
using System.Web;


namespace NicoRealSolution.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PropertyController(IPropertyService propService, IWebHostEnvironment webHostEnvironment)
        {
            _propService = propService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var properties = await _propService.GetProperties();
            return View(properties);

        }

        public async Task<IActionResult> Details(int id)
        {
            var property = await _propService.GetByIdAsync(id);
            if (property.Price != null && property.Surface != null)
            {
                decimal priceMp = (decimal)(property.Price / property.Surface);
                decimal roundedPriceMp = decimal.Round(priceMp, 2);
                ViewBag.priceMp = roundedPriceMp.ToString("0.00");
            }
            return View(property);

        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(Property property)
        {
            List<string> guidList = new List<string>();
            var images = Request.Form.Files;
            if (!ModelState.IsValid)
            {
                return View("Create", property);
            }

            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    var fileExtension = Path.GetExtension(image.FileName);
                    var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                    var filePath = Path.Combine(uploadPath, uniqueFileName);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                    guidList.Add(uniqueFileName);
                }
            }
            string guidsString = string.Join(",", guidList);
            property.PhotoGuids = guidsString;
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

        [HttpGet]
        public async Task<IActionResult> EditPage(int id)
        {
            var property = await _propService.GetByIdAsync(id);
            if (property == null) return View("NotFound");
            return View(property);
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Property property)
        {

            await _propService.UpdateProperty(property);
            return RedirectToAction("Details", new { id = property.Id });

        }



    }
}
