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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name="SupplierName")]
        [StringLength(100)]
        public String SupplierName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string HomeNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string WorkNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        public ICollection<Book> Books { get; set; }

    }
}
