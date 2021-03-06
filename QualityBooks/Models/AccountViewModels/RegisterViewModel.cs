﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Spatial;
using System.Threading.Tasks;
using QualityBooks.Models.Validation;

namespace QualityBooks.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name="Address")]
        public string Address { get; set; }

        [Display(Name="Home Phone")]
        [RequireOnePhoneNumberCustomer]
        public string HomePhone { get; set; }
        [Display(Name="Work Phone")]
        [RequireOnePhoneNumberCustomer]
        public string WorkPhone { get; set; }
        [Display(Name="Mobile Phone")]
        [RequireOnePhoneNumberCustomer]
        public string MobilePhone { get; set; }
    }

}
