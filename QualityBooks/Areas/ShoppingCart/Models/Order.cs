using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using QualityBooks.Models;

namespace QualityBooks.Areas.ShoppingCart.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string City { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public ApplicationUser User { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal GST { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]

        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; set; }

        [Required]
        public OrderStatus Status { get; set; }
        
    }
} 

