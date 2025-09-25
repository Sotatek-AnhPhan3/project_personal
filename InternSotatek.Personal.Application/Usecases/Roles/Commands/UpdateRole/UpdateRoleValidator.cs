using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;

namespace InternSotatek.Personal.Application.Usecases.Roles.Commands.UpdateRole;

public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
{
    private readonly IRepository<Role, Guid> _roleRepository;

    public UpdateRoleValidator(IRepository<Role, Guid> roleRepository)
    {
        _roleRepository = roleRepository;
        RuleFor(x => x.Id)
            .MustAsync(RoleNameExist).WithMessage("Username already exists");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name Role unavailable")
            .Length(2, 250).WithMessage("Name role must be between 2 and 250 characters");
            
        RuleFor(x => x.Description)
            .Length(2, 250).When(x => !string.IsNullOrEmpty(x.Description)).WithMessage("Description must be between 2 and 250 characters");
    }
    private async Task<bool> RoleNameExist(Guid id, CancellationToken cancellationToken)
    {
        var roleNameExist = await _roleRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return roleNameExist != null;
    }

}
