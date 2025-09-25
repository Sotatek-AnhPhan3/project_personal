using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Application.Usecases.Users.Commands.Update;
using InternSotatek.Personal.Application.Utils;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;

namespace InternSotatek.Personal.Application.Usecases.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleResponse>
{
    private readonly IRepository<Role, Guid> _roleRepository;
    public UpdateRoleCommandHandler(IRepository<Role, Guid> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<UpdateRoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await RoleExist(request, cancellationToken);

        await UpdateRole(existingUser, request, cancellationToken);

        return new UpdateRoleResponse
        {
            Code = 200,
            Message = "Update successfull"
        };
    }

    private async Task<Role> RoleExist(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var existingNameRole = await _roleRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (existingNameRole == null)
        {
            throw new KeyNotFoundException("Name doesn't exist");
        }

        var nameRoleConflict = await _roleRepository.FirstOrDefaultAsync(
            x => x.Name.ToLower() == request.Name.ToLower() && x.Id != request.Id, cancellationToken);

        if (nameRoleConflict != null)
        {
            throw new ArgumentException("Name already exists");
        }
        return existingNameRole;
    }

    private async Task UpdateRole(Role role, UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        role.Name = request.Name;
        role.Description = request.Description;
        role.Slug = SlugHelper.StringToSlug(request.Name);
        role.UpdatedTime = DateTime.UtcNow;

        await _roleRepository.UpdateAsync(role, cancellationToken);
    }
}
