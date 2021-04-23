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
            string inputValue = (string)value;
            var scriptTagRegex = new Regex(srciptTagPattern);

            if (scriptTagRegex.IsMatch(inputValue)) return new ValidationResult("Unsafe request content!");

            return ValidationResult.Success;
        }
    }
}
