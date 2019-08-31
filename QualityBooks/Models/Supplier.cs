using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QualityBooks.Models.Validation;

namespace QualityBooks.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name="Supplier Name")]
        [StringLength(100)]
        public String SupplierName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Home Number")]
        [RequireOnePhoneNumber]
        public string HomeNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name="Work Number")]
        public string WorkNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        public ICollection<Book> Books { get; set; }

    }
}
