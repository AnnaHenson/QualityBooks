using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace QualityBooks.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
         
        [Required]
        [StringLength(150)]
        public String Title { get; set; }

        public String Description { get; set; }
   
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        [Range(minimum:0, maximum:200)]
        public decimal Price { get; set; }

        
        public Supplier Supplier { get; set; }
        [Required]
        public int SupplierId { get; set; } //FK
        public string Image { get; set; }
        [NotMapped] 
        public IFormFile BookImage { get; set; }
  
        public Category Category { get; set; }
        public int CategoryId { get; set; } //FK

    }
}
