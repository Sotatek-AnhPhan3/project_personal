using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;

namespace InternSotatek.Personal.Application.Usecases.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, DeleteRoleResponse>
{
    private readonly IRepository<Role, Guid> _roleRepository;

    public DeleteRoleCommandHandler(IRepository<Role, Guid> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<DeleteRoleResponse> Handle(DeleteRoleCommand command, CancellationToken cancellation)
    {
        await _roleRepository.DeleteByIdAsync(command.Id, cancellation);
        return new DeleteRoleResponse {};
    }
}
