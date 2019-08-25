using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace QualityBooks.Models
{
    public class Customer : IdentityUser
    {
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public String LastName { get; set; }

        [Required]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }
        
        public String Address { get; set; }
    
        [DataType(DataType.PhoneNumber)]
        public string HomeNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string WorkNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }

        public ICollection <Order> Orders { get; set; }
    }
}
