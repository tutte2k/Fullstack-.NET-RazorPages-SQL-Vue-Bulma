using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Cart;
using System.Linq;

namespace OnlineShop.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly GetCart _getCart;

        public CartViewComponent(GetCart getCart)
        {
            _getCart = getCart;
        }
        public IViewComponentResult Invoke(string view = "Default")
        {
            if (view == "Small")
            {
                var totalValue = _getCart.Do().Sum(x => x.RealValue * x.Qty);
                return View(view, $"{totalValue}kr");
            }
            return View(view, _getCart.Do());
        }
    }
}
