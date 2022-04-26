using OnlineShop.Domain;
using OnlineShop.Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Application.ProductsAdmin
{
    [Service]
    public class CreateProduct
    {

        private readonly IProductManager _productmanager;

        public CreateProduct(IProductManager productManager)
        {
            _productmanager = productManager;
        }
        public async Task<Response> Do(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value
            };

            if (await _productmanager.CreateProduct(product) <= 0)
            {
                throw new Exception("Failed to create product");
            }

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }
        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }

}
