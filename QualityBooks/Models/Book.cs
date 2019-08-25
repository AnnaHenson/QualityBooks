﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public decimal Price { get; set; }

        [Required]
        public Supplier Supplier { get; set; }
        public string Image { get; set; }

        public Category Category { get; set; }

    }
}
