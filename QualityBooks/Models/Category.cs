using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualityBooks.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public String Name { get; set; }

        public ICollection< Book > Books { get; set; }
    }
}
