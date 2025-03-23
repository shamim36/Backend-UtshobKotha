using System.ComponentModel.DataAnnotations;

namespace Backend_UtshobKotha.Validation
{
    public class CategoryValidationAttribute : ValidationAttribute
    {
        private readonly string[] _validCategories = new[]
        {
            "Academic", "Cultural", "Sports", "Technical", "Business", "Career"
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Category is required");
            }

            string category = value.ToString();
            if (!_validCategories.Contains(category))
            {
                return new ValidationResult($"Invalid category. Valid categories are: {string.Join(", ", _validCategories)}");
            }

            return ValidationResult.Success;
        }
    }
}
