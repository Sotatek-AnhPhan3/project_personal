using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;

namespace InternSotatek.Personal.Application.Usecases.Users.Commands.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IRepository<User, Guid> _userRepository;

    public UpdateUserValidator(IRepository<User, Guid> userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Id)
            .MustAsync(UserExist).WithMessage("Username already exists");
        // Username
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username unavailable")
            .Length(2, 250).WithMessage("Username is invalid");
            //.MustAsync(UsernameNotTakenByOthers).WithMessage("Username already exists");
        // Email
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email unavailable")
            .EmailAddress().WithMessage("Email is invalid");
        // phone  number
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d+$").WithMessage("PhoneNumber must contain only digits")
            .Length(10, 11).WithMessage("PhoneNumber is invalid")
            .NotEmpty().WithMessage("PhoneNumber unavailable");
        // first name
        RuleFor(x => x.Firstname)
            .Length(2, 250).WithMessage("Firstname must be between 2 and 250 characters");
        // Last name
        RuleFor(x => x.Lastname)
            .Length(2, 250).WithMessage("Lastname must be between 2 and 250 characters");
    }
    private async Task<bool> UserExist(Guid id, CancellationToken cancellationToken)
    {
        var userExist = await _userRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return userExist != null;
    }

}
