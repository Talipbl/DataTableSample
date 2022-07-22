using DataTableSample.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DataTableSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<string> GetProducts()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://dummyjson.com/products");
            var result = await response.Content.ReadAsStringAsync();
            //ProductsDto products = JsonConvert.DeserializeObject<ProductsDto>(result);
            return result;
        }
        [HttpGet]
        public async Task<string> GetProduct(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://dummyjson.com/products/"+id);
            var result = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(result);
            result = JsonConvert.SerializeObject(product);
            return result;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}