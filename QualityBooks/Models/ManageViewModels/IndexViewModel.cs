using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QualityBooks.Models.Validation;

namespace QualityBooks.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Home phone number")]
        [RequireOnePhoneNumberCustomerIndex]
        public string HomePhone { get; set; }

        [Phone]
        [Display(Name = "Work phone number")]
        [RequireOnePhoneNumberCustomerIndex]
        public string WorkPhone { get; set; }

        [Phone]
        [Display(Name = "Mobile phone number")]
        [RequireOnePhoneNumberCustomerIndex]
        public string MobilePhone { get; set; }

        public string Address { get; set; }

        public string StatusMessage { get; set; }
    }
}
