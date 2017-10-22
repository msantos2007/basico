using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Basico.Web.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;

namespace Basico.Web.Models
{
    public class LoginViewModel : IValidatableObject
    { 
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }        

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new LoginViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}