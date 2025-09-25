using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;

namespace InternSotatek.Personal.Application.Usecases.Users.Commands.Delete;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    private readonly IRepository<User, Guid> _userRepository;
    public DeleteUserValidator(IRepository<User, Guid> userRepository)
    {
        _userRepository = userRepository;
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id unavailable")
            .MustAsync(UserExist).WithMessage("Username doesn't exists");
    }
    private async Task<bool> UserExist(Guid id, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return existingUser != null;
    }
}
