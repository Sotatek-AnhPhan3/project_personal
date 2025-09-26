using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Usecases.UserRoles.Queries.GetAllRoleByUserId;

public class GetAllRoleByUserIdQueryHandler : IRequestHandler<GetAllRoleByUserIdQuery, GetAllRoleByUserIdResponse>
{
    private readonly IRepository<UserRole, Guid> _userRoleRepository;

    public GetAllRoleByUserIdQueryHandler(IRepository<UserRole, Guid> userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public async Task<GetAllRoleByUserIdResponse> Handle(GetAllRoleByUserIdQuery query, CancellationToken cancellationToken)
    {
        var allRoleByUserId = await _userRoleRepository
            .GetAll()
            .Where(x => x.UserId == query.UserId)
            .Include(x => x.Role)
            .ToListAsync();

        var allRoles = allRoleByUserId.Select(x => new Role
        {
            Id = x.Id,
            Name = x.Role.Name,
            Description = x.Role.Description,
            Slug = x.Role.Slug,
            CreatedTime = x.Role.CreatedTime,
            UpdatedTime = x.Role.UpdatedTime,
        }).ToList();

        return new GetAllRoleByUserIdResponse
        {
            UserId = query.UserId,
            Roles = allRoles
        };
    }
}
