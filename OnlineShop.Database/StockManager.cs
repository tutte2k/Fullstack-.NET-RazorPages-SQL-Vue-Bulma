using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Infrastructure;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Database
{
    public class StockManager : IStockManager
    {
        private readonly ApplicationDbContext _context;

        public StockManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> CreateStock(Stock stock)
        {
            _context.Stocks.Add(stock);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteStock(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
            _context.Stocks.Remove(stock);
            return _context.SaveChangesAsync();
        }
        public Task<int> UpdateStockRange(List<Stock> stockList)
        {
            _context.UpdateRange(stockList);
            return _context.SaveChangesAsync();
        }

        public bool EnoughStock(int stockId, int qty)
        {
            if (stockId == 0 || qty == 0)
            {
                return false;
            }
            var result = _context.Stocks.FirstOrDefault(x => x.Id == stockId).Qty >= qty; ;
            return result;
        }

        public Stock GetStockWithProduct(int stockId)
        {
            return _context.Stocks
            .Include(x => x.Product)
            .FirstOrDefault(x => x.Id == stockId);
        }

        public Task PutStockOnHold(int stockId, int qty, string sessionId)
        {


            _context.Stocks.FirstOrDefault(x => x.Id == stockId).Qty -= qty;

            var stockOnHold = _context.StockOnHolds.Where(x => x.SessionId == sessionId).ToList();

            if (stockOnHold.Any(x => x.StockId == stockId))
            {
                stockOnHold.Find(x => x.StockId == stockId).Qty += qty;
            }
            else
            {
                _context.StockOnHolds.Add(new StockOnHold
                {
                    StockId = stockId,
                    SessionId = sessionId,
                    Qty = qty,
                    ExpiryDate = DateTime.Now.AddMinutes(20)
                });
            }

            foreach (var stock in stockOnHold)
            {
                stock.ExpiryDate = DateTime.Now.AddMinutes(20);
            }


            return _context.SaveChangesAsync();

        }

        public Task RemoveStockFromHold(string sessionId)
        {
            var stockOnHold = _context.StockOnHolds.Where(x => x.SessionId == sessionId).ToList();
            _context.StockOnHolds.RemoveRange(stockOnHold);
            return _context.SaveChangesAsync();
        }
        public Task RemoveStockFromHold(int stockId, int qty, string sessionId)
        {
            var stockOnHold = _context.StockOnHolds
                .FirstOrDefault(x => x.StockId == stockId && x.SessionId == sessionId);
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == stockId);
            stock.Qty += qty;
            stockOnHold.Qty -= qty;
            if (stockOnHold.Qty <= 0)
            {
                _context.Remove(stockOnHold);
            }
            return _context.SaveChangesAsync();

        }

        public Task RetrieveExpiredStockOnHold()
        {
            var stocksOnHold = _context.StockOnHolds.Where(x => x.ExpiryDate < DateTime.Now).ToList();

            if (stocksOnHold.Count >= 0)
            {
                var stockToReturn = _context.StockOnHolds.Where(x => stocksOnHold.Select(y => y.StockId).Contains(x.Id)).ToList();

                //var stockToReturn = _context.StockOnHolds.Where(x => stocksOnHold.Any(y => y.StockId == x.Id)).ToList();
                foreach (var stock in stockToReturn)
                {
                    stock.Qty = stock.Qty + stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id).Qty;
                }
                _context.StockOnHolds.RemoveRange(stocksOnHold);
                return _context.SaveChangesAsync();
            }
            return Task.CompletedTask;
        }


    }


}
