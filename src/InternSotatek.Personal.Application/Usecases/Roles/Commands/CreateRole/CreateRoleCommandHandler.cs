using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Application.Utils;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Usecases.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateRoleResponse>
{
    private readonly IRepository<Role, Guid> _roleRepository;
    public CreateRoleCommandHandler(
        IRepository<Role, Guid> roleRepository
    )
    {
        _roleRepository = roleRepository;
    }

    public async Task<CreateRoleResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var newRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CreatedTime = DateTime.UtcNow,
            Slug = SlugHelper.StringToSlug(request.Name)
        };
        await _roleRepository.AddAsync(newRole, cancellationToken);

        return new CreateRoleResponse
        {
            Id = newRole.Id,
            Name = newRole.Name,
            Description = newRole.Description,
            Slug = newRole.Slug,
            CreatedTime = newRole.CreatedTime,
        };
    }
}
