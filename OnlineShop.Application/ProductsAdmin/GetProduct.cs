using OnlineShop.Domain.Infrastructure;

namespace OnlineShop.Application.ProductsAdmin
{
    [Service]
    public class GetProduct
    {

        private readonly IProductManager _productmanager;

        public GetProduct(IProductManager productManager)
        {
            _productmanager = productManager;
        }
        public ProductViewModel Do(int id) =>

            _productmanager.GetProductById(id, x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = x.Value
            });

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }

}
