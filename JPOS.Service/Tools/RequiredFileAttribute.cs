using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace JPOS.Service.Tools
{
    public class RequiredFileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file && file != null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "File is required");
        }
    }
}
