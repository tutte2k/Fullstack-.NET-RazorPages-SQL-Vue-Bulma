using OnlineShop.Domain.Infrastructure;
using System.Threading.Tasks;

namespace OnlineShop.Application.ProductsAdmin
{
    [Service]
    public class DeleteProduct
    {
        private readonly IProductManager _productManager;

        public DeleteProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public Task<int> Do(int id)
        {
            return _productManager.DeleteProduct(id);
        }

    }

}
