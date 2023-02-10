using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.Web.Pages
{
    public class AboutCustomerModel : PageModel
    {
        public IEnumerable<Customer>? Customers { get; set; }

        private NorthwindContext db;
        public AboutCustomerModel(NorthwindContext injectedContext)
        {
            db = injectedContext;
        }
        public Customer Customer { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public void OnGet(string id)
        {
            Customer = db.Customers
                .Where(c => c.CustomerId == id)
                .FirstOrDefault();
            Orders = db.Orders
                .Where(o => o.CustomerId == id);
        }
    }
}
