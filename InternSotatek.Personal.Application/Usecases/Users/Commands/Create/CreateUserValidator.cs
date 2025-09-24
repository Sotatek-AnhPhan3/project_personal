using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace InternSotatek.Personal.Application.Usecases.Users.Commands.Create
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            // Username
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username unavailable")
                .Length(2, 250).WithMessage("Username is invalid");
            // passsword
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password unavailable")
                .Length(6, 250).WithMessage("Password is invalid");
            // Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email unavailable")
                .EmailAddress().WithMessage("Email is invalid");
            // phone  number
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\d+$").WithMessage("PhoneNumber must contain only digits")
                .Length(10, 11).WithMessage("PhoneNumber is invalid")
                .NotEmpty().WithMessage("PhoneNumber unavailable");
        }
    }
}
