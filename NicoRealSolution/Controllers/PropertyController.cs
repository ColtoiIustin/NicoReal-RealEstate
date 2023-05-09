using Microsoft.AspNetCore.Mvc;
using NicoRealSolution.Data.Services;

namespace NicoRealSolution.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propService;
        public PropertyController(IPropertyService propService)
        {
            _propService= propService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
