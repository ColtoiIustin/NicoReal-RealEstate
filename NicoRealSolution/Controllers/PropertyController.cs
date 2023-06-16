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
        public async Task<IActionResult> Index()
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            map = await _propService.CategCountMap();

            ViewBag.Houses = map["Casa"];
            ViewBag.Apartments = map["Apartament"];
            ViewBag.Lands = map["Teren"];
            ViewBag.Investments = map["Investitie"];
            ViewBag.Hotels = map["Hotel"];


            var properties = await _propService.GetProperties();
            var filteredProperties = properties.Where(p => p.IsFeatured == "Da").ToList();
            return View(filteredProperties);

        }

        public async Task<IActionResult> Details(int id)
        {
            var property = await _propService.GetByIdAsync(id);
            var properties = await _propService.GetProperties();
            if (property.Price != null && property.Surface != null)
            {
                decimal priceMp = (decimal)(property.Price / property.Surface);
                decimal roundedPriceMp = decimal.Round(priceMp, 2);
                ViewBag.priceMp = roundedPriceMp.ToString("0.00");
            }

            var viewModel = new ViewModelDetails
            {
                SingleProp = property,
                PropList = properties

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
        public async Task<IActionResult> Listings(int pg=1)
        {
            var properties = await _propService.GetProperties();
            ViewBag.Results = properties.Count();

            const int pageSize = 1;
            if (pg < 1) pg = 1;

            int propCount =properties.Count();
            var pager = new Pager(propCount, pg, pageSize);

            int propSkip = (pg-1) * pageSize;

            var data = properties.Skip(propSkip).Take(pager.PageSize).ToList();

            
            this.ViewBag.Pager = pager;

            return View(data);

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

                  return File(response.ResponseStream, response.Headers.ContentType,key);

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
