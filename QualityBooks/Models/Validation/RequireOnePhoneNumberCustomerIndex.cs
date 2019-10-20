using System.ComponentModel.DataAnnotations;
using QualityBooks.Models.ManageViewModels;

namespace QualityBooks.Models.Validation
{
    public class RequireOnePhoneNumberCustomerIndex : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (IndexViewModel) validationContext.ObjectInstance;
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