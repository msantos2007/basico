using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FluentValidation;
using Basico.Web.Models;

namespace Basico.Web.Infrastructure.Validators
{
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress()
                .WithMessage("Email inválido");

            RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Usuário inválido");

            RuleFor(r => r.Password).NotEmpty()
                .WithMessage("Senha inválida");
        }
    }

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Usuário inválido");

            RuleFor(r => r.Password).NotEmpty()
                .WithMessage("Senha inválida");
        }
    }


    public class UserEditViewModelValidator : AbstractValidator<UserEditViewModel>
    {
        public UserEditViewModelValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress()
                .WithMessage("Email inválido");

            RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Usuário inválido");

            RuleFor(r => r.usuario_logado).NotEmpty()
                .WithMessage("Usuário Inexistente");

            //RuleFor(r => r.password).Empty();

            //RuleFor(r => r.newpassword_n).Empty();

            //RuleFor(r => r.newpassword_c).Empty();

        }
    }
}