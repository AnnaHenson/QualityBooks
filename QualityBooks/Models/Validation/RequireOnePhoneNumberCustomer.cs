using System.ComponentModel.DataAnnotations;
using QualityBooks.Models.AccountViewModels;

namespace QualityBooks.Models.Validation
{
    public class RequireOnePhoneNumberCustomer : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (RegisterViewModel) validationContext.ObjectInstance;
            if (!string.IsNullOrEmpty(customer.HomePhone) || !string.IsNullOrEmpty(customer.WorkPhone) || !string.IsNullOrEmpty(customer.MobilePhone))
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