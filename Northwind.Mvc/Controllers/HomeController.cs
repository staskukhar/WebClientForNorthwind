using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Mvc.Models;
using System.Diagnostics;
using Packt.Shared;
using Humanizer;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext db;

        public HomeController(ILogger<HomeController> logger, NorthwindContext injectedContext)
        {
            _logger = logger;
            db = injectedContext;
        }
        public async Task<IActionResult> Index()
        {
            HomeIndexViewModel model = new
            (
                VisitorCount : (new Random()).Next(1, 1001),
                Categories: await db.Categories.ToListAsync(),
                Products: await db.Products.ToListAsync()
            );
            return View(model);
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

        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("You must pass a product ID in the route, forexample, / Home / ProductDetail / 21");
            }
            Product? model = await db.Products.SingleOrDefaultAsync(p => p.ProductId == id);
            if (model is null)
            {
                return NotFound($"ProductId {id} not found.");
            }
            return View(model);
        }

        public IActionResult ModelBinding()
        {
            return View(); // the page with a form to submit
        }
        [HttpPost]
        public IActionResult ModelBinding(Thing thing)
        {
            HomeModelBindingViewModel model = new(
                Thing: thing, HasErrors: !ModelState.IsValid,
                ValidationErrors: ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage)
                );
            return View(model);
        }
        public IActionResult ProductsThatCostMoreThan(decimal? price)
        {
            if(!price.HasValue)
            {
                return NotFound("You must pass a product price in the query string, for example, /Home/ProductsThatCostMoreThan?price=50");
            }

            IEnumerable<Product> model = db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.UnitPrice > price);
            if(model.Count() == 0)
            {
                return NotFound($"No products cost more than {price:C}");
            }
            ViewData["MaxPrice"] = price.Value.ToString("C");
            return View(model);
        }
        [Route("category")]
        public IActionResult Category(int? id)
        {
            if (id == null) return NotFound("Maybe you missed parameter id: category/id");
            
            var category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            return View(category);
        }
    }
}