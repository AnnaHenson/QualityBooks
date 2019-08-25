using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualityBooks.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0: {yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOrdered { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal Subtotal { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal GST { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal GrandTotal { get; set; }

        public Customer Customer { get; set; }
        

        
    }
}