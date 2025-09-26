using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;

namespace InternSotatek.Personal.Application.Usecases.Roles.Commands.DeleteRole;

public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
{
    private readonly IRepository<Role, Guid> _roleRepository;

    public DeleteRoleValidator(IRepository<Role, Guid> roleRepository)
    {
        _roleRepository = roleRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id unavailable")
            .MustAsync(RoleExist).WithMessage("Role doesn't exists");
    }

    private async Task<bool> RoleExist(Guid id, CancellationToken cancellationToken)
    {
        var existingRole = await _roleRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return existingRole != null;
    }
}
