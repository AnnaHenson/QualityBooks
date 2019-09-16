using System.ComponentModel.DataAnnotations;
using QualityBooks.Models;


namespace QualityBooks.Areas.ShoppingCart.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}
