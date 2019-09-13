using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityBooks.Areas.ShoppingCart.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Books Books { get; set; }
        public Order Order { get; set; }
    }
}
