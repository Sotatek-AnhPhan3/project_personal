using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;

namespace InternSotatek.Personal.Application.Usecases.UserRoles.Commands.Create;

public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, CreateUserRoleResponse>
{
    private readonly IRepository<UserRole, Guid> _userRoleRepository;
    private readonly IRepository<Role, Guid> _roleRepository;
    private readonly IRepository<User, Guid> _userRepository;

    public CreateUserRoleCommandHandler(
        IRepository<UserRole, Guid> userRoleRepository
        , IRepository<Role, Guid> roleRepository
        , IRepository<User, Guid> userRepository
    )
    {
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async Task<CreateUserRoleResponse> Handle(CreateUserRoleCommand command, CancellationToken cancellationToken)
    {
        string userName = await GetUsername(command.UserId, cancellationToken);

        if (await ExistingUserId(command.UserId, cancellationToken))
        {
            await CreateUserRoleUpdate(command, userName, cancellationToken);
        }
        else
        {
            await CreateUserRoleFisrt(command, userName, cancellationToken);
        }
        
        return new CreateUserRoleResponse { 
            UserId = command.UserId,
            RoleIds = command.RoleIds,
        };
    }

    private async Task<string> GetUsername(Guid userId, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (existingUser == null)
        {
            throw new KeyNotFoundException("User doesn't exists");
        }
        return existingUser.Username;
    }
    private async Task<string> GetRoleName(Guid roleId, CancellationToken cancellationToken)
    {
        var existingRole = await _roleRepository.FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);
        if (existingRole == null)
        {
            throw new KeyNotFoundException("Role doesn't exists");
        }
        return existingRole.Name;
    }
    private async Task<bool> ExistingUserId(Guid userId, CancellationToken cancellationToken)
    {
        var existingUser = await _userRoleRepository.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        return existingUser != null;
    }
    private async Task CreateUserRoleFisrt(CreateUserRoleCommand command, string userName, CancellationToken cancellationToken)
    {
        foreach (var item in command.RoleIds)
        {
            string roleName = await GetRoleName(item, cancellationToken);
            var userRoles = new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                Username = userName,
                RoleId = item,
                RoleName = roleName
            };
            await _userRoleRepository.AddAsync(userRoles, cancellationToken);
        }
    }
    private async Task CreateUserRoleUpdate(CreateUserRoleCommand command, string userName, CancellationToken cancellationToken)
    {
        await _userRoleRepository.DeleteWhereAsync(x => x.UserId == command.UserId, cancellationToken);
        await CreateUserRoleFisrt(command, userName, cancellationToken);
    }
}
