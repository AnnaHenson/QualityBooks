using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualityBooks.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public String Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [DisplayFormat(DataFormatString = "{}", ApplyFormatInEditMode = true)]
        // need phone number as well
        
        public ICollection< Book > Books { get; set; }

    }
}
