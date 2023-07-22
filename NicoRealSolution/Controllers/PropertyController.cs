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
using NicoRealSolution.ViewModels;
using NicoRealSolution.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.Runtime;
using Newtonsoft.Json.Linq;
using Amazon.Auth.AccessControlPolicy.ActionIdentifiers;

namespace NicoRealSolution.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public PropertyController(IPropertyService propService, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _propService = propService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string language = "Română")
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            map = await _propService.CategCountMap();

            ViewData["Houses"] = map["Casa"];
            ViewData["Apartments"] = map["Apartament"];
            ViewData["Lands"] = map["Teren"];
            ViewData["Investments"] = map["Investitie"];
            ViewData["Hotels"] = map["Hotel"];

            ViewData["language"] = language;

            var properties = await _propService.GetProperties();
            var filteredProperties = properties.Where(p => p.IsFeatured == "Da").ToList();

            var viewModel = new ViewModelIndex
            {
                PropList = filteredProperties,

            };
            return View(viewModel);

        }

        public async Task<IActionResult> Details(int id)
        {
            var property = await _propService.GetByIdAsync(id);
            var properties = await _propService.GetProperties();
            if (property.Price == null && property.Surface == null)
            {
                decimal priceMp = 0;
                ViewBag.priceMp = 0;
            } else if (property.Price != null && property.Surface != null)
            {
                decimal priceMp = (decimal)(property.Price / property.Surface);
                decimal roundedPriceMp = decimal.Round(priceMp, 2);
                ViewBag.priceMp = roundedPriceMp.ToString("0.00");
            }

            var viewModel = new ViewModelDetails
            {
                SingleProp = property,
                PropList = properties,

            };
            return View(viewModel);

        }

        [Authorize]
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
            var accessKeyID = "AKIAQ6FH3UCG4LCO4RE3";
            var secretKey = "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";
            var credentials = new BasicAWSCredentials(accessKeyID, secretKey);


            using (var amazons3client = new AmazonS3Client(credentials, RegionEndpoint.USEast1))
            {
                foreach (var image in images)
                {
                    var fileExtension = Path.GetExtension(image.FileName);
                    var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

                    using (var memorystream = new MemoryStream())
                    {
                        image.CopyTo(memorystream);

                        var request = new TransferUtilityUploadRequest
                        {
                            InputStream = memorystream,
                            Key = uniqueFileName,
                            BucketName = "s3bucketnicoreal",
                            ContentType = image.ContentType,
                        };
                        var transferUtility = new TransferUtility(amazons3client);
                        await transferUtility.UploadAsync(request);
                        guidList.Add(uniqueFileName);
                    }
                }
            }
            string guidsString = string.Join(",", guidList);
            property.PhotoGuids = guidsString;
            if (property.Price == null) { property.Price = 0; }
            if (property.Surface == null) { property.Surface = 0; }
            await _propService.AddProperty(property);

            return RedirectToAction(nameof(Index));


        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {

            var property = await _propService.GetByIdAsync(id);
            if (property == null) return View("NotFound");

            await _propService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        [Authorize]
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


        [HttpGet]
        public async Task<IActionResult> Listings(int pg = 1, string sortOrder = "New", string searchString = "",
            string category = "", decimal minPrice = 0, decimal maxPrice = 5000000, string location = "", decimal minSurface = 0,
            decimal maxSurface = 999999999)
        {
            var properties = await _propService.GetProperties();

            switch (sortOrder)
            {
                case "New":
                    properties = properties.Reverse();
                    break;
                case "Price":
                    properties = properties.OrderBy(p => p.Price);
                    break;
                case "PriceDesc":
                    properties = properties.OrderByDescending(p => p.Price);
                    break;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                properties = properties.Where(p => p.Title.Contains(searchString.Trim()));
            }
            if (!string.IsNullOrEmpty(category))
            {
                if (category == "Investiție")
                {
                    properties = properties.Where(p => p.IsInvestment == "Da");
                }
                else if (category != "Toate")
                {
                    properties = properties.Where(p => p.Category == category);
                }
            }
            properties = properties.Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.Surface >= minSurface && p.Surface <= maxSurface);
            if (!string.IsNullOrEmpty(location) && location != "Toate")
            {
                if (location == "Romania")
                {
                    properties = properties.Where(p => p.Country == "Romania" || p.Country == "România");
                }
                else properties = properties.Where(p => p.Country == location);

            }

            ViewBag.Results = properties.Count();

            const int pageSize = 5;
            if (pg < 1) pg = 1;

            int propCount = properties.Count();
            var pager = new Pager(propCount, pg, pageSize);

            int propSkip = (pg - 1) * pageSize;

            var data = properties.Skip(propSkip).Take(pager.PageSize).ToList();

            var viewModel = new ViewModelListings
            {
                PropList = data,
                SelectCateg = category,
                SelectLocation = location,

            };

            if (maxPrice < minPrice) minPrice = maxPrice;
            if (maxSurface < minSurface) minSurface = maxSurface;

            this.ViewBag.Pager = pager;
            ViewData["sortOrder"] = sortOrder;
            ViewData["searchString"] = searchString;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;
            ViewData["minSurface"] = minSurface;
            ViewData["maxSurface"] = maxSurface;

            return View(viewModel);

        }

        public async Task<ActionResult> GetImage(string key)
        {

            var accessKeyID = "AKIAQ6FH3UCG4LCO4RE3";
            var secretKey = "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";
            var credentials = new BasicAWSCredentials(accessKeyID, secretKey);

            using (var amazons3client = new AmazonS3Client(credentials, RegionEndpoint.USEast1))
            {
                var transferUtility = new TransferUtility(amazons3client);

                var response = await transferUtility.S3Client.GetObjectAsync(new GetObjectRequest()
                {
                    BucketName = "s3bucketnicoreal",
                    Key = key
                });

                return File(response.ResponseStream, response.Headers.ContentType, key);

            }
        }

        public async Task<ActionResult> GetImagesDetails(string key)
        {

            var accessKeyID = "AKIAQ6FH3UCG4LCO4RE3";
            var secretKey = "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";
            var credentials = new BasicAWSCredentials(accessKeyID, secretKey);

            using (var amazons3client = new AmazonS3Client(credentials, RegionEndpoint.USEast1))
            {
                var transferUtility = new TransferUtility(amazons3client);

                var response = await transferUtility.S3Client.GetObjectAsync(new GetObjectRequest()
                {
                    BucketName = "s3bucketnicoreal",
                    Key = key
                });

                return File(response.ResponseStream, response.Headers.ContentType, key);

            }
        }



    }
}
