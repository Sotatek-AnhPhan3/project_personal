using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Usecases.Roles.Queries.GetRolesList;

public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, GetRolesListResponse>
{
    private readonly IRepository<Role, Guid> _roleRepository;
    public GetRolesListQueryHandler(IRepository<Role, Guid> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<GetRolesListResponse> Handle(GetRolesListQuery query, CancellationToken cancellationToken)
    {
        var allRoles = await AllRoles(query);

        int totalCount = allRoles.Count();
        var items = await allRoles
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(u => new RolesListDto
            {
                Id = u.Id,
                Name = u.Name,
                Description = u.Description,
                Slug = u.Slug,
                CreatedTime = u.CreatedTime,
                UpdatedTime = u.UpdatedTime
            }).ToListAsync();
        return new GetRolesListResponse
        {
            TotalCount = totalCount,
            Items = items,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            SortBy = query.SortBy,
            Sorting = query.Sorting,
            TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize),
        };
    }

    private async Task<IQueryable<Role>> AllRoles(GetRolesListQuery request)
    {
        var allRoles = _roleRepository.GetAll();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            if (request.Sorting.ToLower() == "asc")
            {
                allRoles = allRoles.OrderBy(u => EF.Property<object>(u, request.SortBy));
            }
            else
            {
                allRoles = allRoles.OrderByDescending(u => EF.Property<object>(u, request.SortBy));
            }
        }

        if (!string.IsNullOrEmpty(request.Search))
        {
            allRoles = allRoles.Where(u =>
                u.Name.ToLower().Contains(request.Search.ToLower()) ||
                u.Description.ToLower().Contains(request.Search.ToLower()) ||
                u.Slug.ToLower().Contains(request.Search.ToLower())
            );
        }

        return allRoles;
    }
}
