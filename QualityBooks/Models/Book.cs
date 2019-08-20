using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualityBooks.Models
{
    public class Book
    {
        public int BookID { get; set; }
         
        public String Title { get; set; }
        public String Author { get; set; }
        public String Description { get; set; }
   
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public ICollection<Supplier> Suppliers { get; set; }

    }
}
