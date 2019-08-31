using System.ComponentModel.DataAnnotations;

namespace QualityBooks.Models.Validation
{
    public class RequireOnePhoneNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var suppler = (Supplier) validationContext.ObjectInstance;
            if (!string.IsNullOrEmpty(suppler.HomeNumber) || !string.IsNullOrEmpty(suppler.WorkNumber) || !string.IsNullOrEmpty(suppler.MobileNumber))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("You must supply one phone number");
            }
        }
    }
}