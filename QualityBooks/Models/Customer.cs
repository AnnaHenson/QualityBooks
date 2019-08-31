using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using QualityBooks.Models.Validation;


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
        [Display(Name ="Home Number")]
        [RequireOnePhoneNumber]
        public string HomeNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name="WorkNumber")]
        public string WorkNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name="MobileNumber")]
        public string MobileNumber { get; set; }

        public ICollection <Order> Orders { get; set; }
    }
}
