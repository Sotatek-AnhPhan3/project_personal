using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;

namespace InternSotatek.Personal.Application.Usecases.Users.Commands.Create;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IRepository<User, Guid> _userRepository;
    public CreateUserValidator(IRepository<User, Guid> userRepository)
    {
        _userRepository = userRepository;
        // Username
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username unavailable")
            .Length(2, 250).WithMessage("Username is invalid")
            .MustAsync(UsernameExist).WithMessage("Username already exists");
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

    private async Task<bool> UsernameExist(string username, CancellationToken cancellationToken) {
        var userExist = await _userRepository.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
        return userExist == null;
    }
}
