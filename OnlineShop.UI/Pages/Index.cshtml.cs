using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.Products;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }


        public void OnGet([FromServices] GetProducts getProducts)
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                Products = getProducts.Do();
            }
            else
            {
                Products = getProducts.Do().Where(product => product.Name.ToLower().Contains(SearchString.ToLower()));
            }

        }
        public void OnPostName([FromServices] GetProducts getProducts)
        {
            Products = getProducts.Do().OrderBy(product => product.Name);
        }
        public void OnPostPrice([FromServices] GetProducts getProducts)
        {
            Products = getProducts.Do().OrderBy(product => product.Price);
        }
    }
}