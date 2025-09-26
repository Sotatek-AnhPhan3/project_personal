using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;

namespace InternSotatek.Personal.Application.Usecases.UserRoles.Queries.GetAllRoleByUserId;

public class GetAllRoleByUserIdValidator : AbstractValidator<GetAllRoleByUserIdQuery>
{
    private readonly IRepository<UserRole, Guid> _userRoleRepository;

    public GetAllRoleByUserIdValidator(IRepository<UserRole, Guid> userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId unavailable")
            .MustAsync(ExistingUserId).WithMessage("User doesn't exists");
    }

    private async Task<bool> ExistingUserId(Guid userId, CancellationToken cancellationToken)
    {
        var existingUser = _userRoleRepository.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        return existingUser != null;
    }
   
}
