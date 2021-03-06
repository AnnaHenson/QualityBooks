﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace QualityBooks.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Address { get; set; }
   
        public string HomePhone { get; set; }
      
        public string WorkPhone { get; set; }

        public string MobilePhone { get; set; }
    }
}
