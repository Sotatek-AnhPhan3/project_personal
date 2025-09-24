using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace InternSotatek.Personal.Application.Usecases.Roles.Commands.CreateRole
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name Role unavailable")
                .Length(2, 250).WithMessage("Name role must be between 2 and 250 characters");
            RuleFor(x => x.Description)
                .Length(2, 250).When(x => !string.IsNullOrEmpty(x.Description)).WithMessage("Description must be between 2 and 250 characters");
        }
    }
}

