using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Basico.Web.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;

namespace Basico.Web.Models
{
    public class UserEditViewModel : IValidatableObject
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string password { get; set; }
        public string newpassword_n { get; set; }
        public string newpassword_c { get; set; }
        public string usuario_logado { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateCreated { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new UserEditViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}