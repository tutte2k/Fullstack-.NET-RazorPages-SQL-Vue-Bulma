using OnlineShop.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Application.Products
{
    [Service]
    public class GetProducts
    {

        private readonly IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public IEnumerable<ProductViewModel> Do() =>
            _productManager.GetProductsWithStock(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = x.Value.GetValueString(),
                Price = x.Value,
                StockCount = x.Stock.Sum(y => y.Qty)
            });

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public decimal Price { get; set; }
            public int StockCount { get; set; }

        }
    }

}
