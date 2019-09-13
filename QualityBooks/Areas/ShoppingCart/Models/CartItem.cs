using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualityBooks.Models;

namespace QualityBooks.Areas.ShoppingCart.Models
{
    public class CartItem
    {
   
        public int ID { get; set; }
        public string CartID { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public Book Book { get; set; }    
    }
}
