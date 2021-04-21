using Balto.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Balto.Web.Validation
{
    public class ObjectiveDateCompareAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var objective = (ObjectivePostView)validationContext.ObjectInstance;
            
            if(objective.StartingDate != null || value != null)
            {
                if (DateTime.Compare((DateTime)objective.StartingDate, (DateTime)value) < 0)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("The objective start and end dates require values and the end must not be before the start!");          
        }
    }
}
