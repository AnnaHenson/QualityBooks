using System.Collections.Generic;
using QualityBooks.Areas.ShoppingCart.Models;

namespace QualityBooks.Models.ShoppingCartViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}
