using OnlineShop.Domain.Infrastructure;
using System.Threading.Tasks;

namespace OnlineShop.Application.StockAdmin
{
    [Service]
    public class DeleteStock
    {

        private readonly IStockManager _stockManager;

        public DeleteStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }
        public Task<int> Do(int id)
        {

            return _stockManager.DeleteStock(id);
        }

    }
}
