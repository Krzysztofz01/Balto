using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Balto.Web.Validation
{
    public class AntiCrossSiteScriptingAttribute : ValidationAttribute
    {
        private readonly string srciptTagPattern = "<[/a-zA-Z]+>";
        //@"^[a-zA-Z''-'\s!@#$%^&*]{1,64}$"

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Ignore if value is null, let it handle by other attributes
            if (value == null) return ValidationResult.Success;

            var inputValue = (string)value;
            if (string.IsNullOrEmpty(inputValue)) return ValidationResult.Success;

            var scriptTagRegex = new Regex(srciptTagPattern);
            if (!scriptTagRegex.IsMatch(inputValue)) return ValidationResult.Success;

            return new ValidationResult("Unsafe request content!");
        }
    }
}
