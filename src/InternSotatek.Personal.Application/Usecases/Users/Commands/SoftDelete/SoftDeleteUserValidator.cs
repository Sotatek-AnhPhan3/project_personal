using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;

namespace InternSotatek.Personal.Application.Usecases.Users.Commands.SoftDelete;
public class SoftDeleteUserValidator : AbstractValidator<SoftDeleteUserCommand>
{
    private readonly IRepository<User, Guid> _userRepository;
    public SoftDeleteUserValidator(IRepository<User, Guid> userRepository)
    {
        _userRepository = userRepository;
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id unavailable");
    }
}
