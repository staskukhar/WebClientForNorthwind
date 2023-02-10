using Microsoft.AspNetCore.Mvc;
using Packt.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.Web.Pages
{
    public class CustomersModel : PageModel
    {
        private NorthwindContext db;
        public CustomersModel(NorthwindContext injectedContext)
        {
            db = injectedContext;
        }
        public IEnumerable<Customer>? Customers { get; set; }
        public void OnGet()
        {
            ViewData["Title"] = "Northwind B2B - Customers";

            Customers = db.Customers
                .OrderBy(c => c.Country);
        }
    }
}
