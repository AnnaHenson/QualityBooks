using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QualityBooks.Models
{
    public class Customer
    {
       public int CustomerID { get; set; }
       [StringLength(50)]
        public String Lastname { get; set; }
        [StringLength(50)]
        [Column("FirstName")]
        public String FirstMidName { get; set; }
        
        public String Address { get; set; }
    
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{}",ApplyFormatInEditMode = true)]
        // Need email address as well
        

        public ICollection <Order> Orders { get; set; }
    }
}
