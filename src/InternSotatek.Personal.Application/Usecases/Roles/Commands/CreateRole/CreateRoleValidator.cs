using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;

namespace InternSotatek.Personal.Application.Usecases.Roles.Commands.CreateRole;

public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IRepository<Role, Guid> _roleRepository;
    public CreateRoleValidator(IRepository<Role, Guid> roleRepository)
    {
        _roleRepository = roleRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name Role unavailable")
            .Length(2, 250).WithMessage("Name role must be between 2 and 250 characters")
            .MustAsync(RoleNameExist).WithMessage("Username already exists");
        RuleFor(x => x.Description)
            .Length(2, 250).When(x => !string.IsNullOrEmpty(x.Description)).WithMessage("Description must be between 2 and 250 characters");
    }
    private async Task<bool> RoleNameExist(string name, CancellationToken cancellationToken)
    {
        var roleNameExist = await _roleRepository.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
        return roleNameExist == null;
    }

}

